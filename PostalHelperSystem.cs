using Unity.Collections;
using Unity.Entities;
using Colossal.Entities;
using Game;
using Game.Prefabs;
using Game.Buildings;
using Game.Economy;
using System;

namespace PostalHelper;

// The system will run before Game.Simulation.PostFacilityAISystem.PostFacilityTickJob
// updateSystem.UpdateAt<PostFacilityAISystem>(SystemUpdatePhase.GameSimulation);

public partial class PostalHelperSystem : GameSystemBase
{
    private EntityQuery m_PostFacilitiesQuery;

    public override int GetUpdateInterval(SystemUpdatePhase phase)
    {
#if DEBUG
        return 256; // PostFacilityAISystem has 256
#else
        return 262144 / 32; // 32 updates per day is once per 45 in-game minutes
#endif
    }
    public override int GetUpdateOffset(SystemUpdatePhase phase)
    {
        return 48; // PostFacilityAISystem has 176
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        m_PostFacilitiesQuery = GetEntityQuery(new EntityQueryDesc
        {
            All = new ComponentType[3]
            {
            ComponentType.ReadOnly<Game.Prefabs.PrefabRef>(),
            ComponentType.ReadOnly<Game.Buildings.PostFacility>(),
            ComponentType.ReadWrite<Game.Economy.Resources>(),
            },
            None = new ComponentType[3]
            {
                ComponentType.ReadOnly<Game.Common.Destroyed>(),
                ComponentType.ReadOnly<Game.Common.Deleted>(),
                ComponentType.ReadOnly<Game.Tools.Temp>(),
            },
        });
        RequireForUpdate(m_PostFacilitiesQuery);
        Mod.log.Info("PostalHelperSystem created.");
    }

    protected override void OnUpdate()
    {
        //Mod.log.Info($"OnUpdate: {m_PostFacilitiesQuery.CalculateEntityCount()} entities");
        foreach(Entity postEntity in m_PostFacilitiesQuery.ToEntityArray(Allocator.Temp)) {
            if(!EntityManager.TryGetComponent<PrefabRef>(postEntity, out PrefabRef prefab)) {
                Mod.log.Warn($"Failed to retrieve PrefabRef for {postEntity}.");
                continue;
            }
            if(!EntityManager.TryGetComponent<PostFacilityData>(prefab, out PostFacilityData postFacilityData)) {
                Mod.log.Warn($"Failed to retrieve PostFacilityData for {prefab}.");
                continue;
            }
            if(!EntityManager.TryGetBuffer<Resources>(postEntity, false, out DynamicBuffer<Resources> resourcesBuffer)) {
                Mod.log.Warn($"Failed to retrieve Resources buffer for {postEntity}.");
                continue;
            }

            var sortingRate = postFacilityData.m_SortingRate;
            var mailCapacity = postFacilityData.m_MailCapacity;
            var localMailCount = EconomyUtils.GetResources(Resource.LocalMail, resourcesBuffer);
			var outgoingMailCount = EconomyUtils.GetResources(Resource.OutgoingMail, resourcesBuffer);
			var unsortedMailCount = EconomyUtils.GetResources(Resource.UnsortedMail, resourcesBuffer);
			var allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

			if(mailCapacity <= 0) {
                Mod.log.Warn($"Mail capacity is zero or less: {mailCapacity}");
				continue;
			}

#if DEBUG
			Mod.log.Info($"{postEntity}: SortingRate {sortingRate}, Capacity {mailCapacity}, TotalMail: {allMailCount}, UnsortedMail {unsortedMailCount}, LocalMail {localMailCount}, OutgoingMail {outgoingMailCount}");
#endif

            if(sortingRate == 0) {  // 郵便局
				if(Mod.m_Setting.PO_GetLocalMails && localMailCount * 100 / mailCapacity <= Mod.m_Setting.PO_TriggerPercentage) {
					EconomyUtils.AddResources(Resource.LocalMail, mailCapacity * Mod.m_Setting.PO_GettingPercentage / 100, resourcesBuffer);

					var old = localMailCount;
					localMailCount = EconomyUtils.GetResources(Resource.LocalMail, resourcesBuffer);
					allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

					Mod.log.Info($"Get: {postEntity}.LocalMail: {old} -> {localMailCount}");
				}
				if(Mod.m_Setting.PO_DisposeOverflow && allMailCount * 100 / mailCapacity >= Mod.m_Setting.PO_OverflowPercentage) {
                    var removalRate = (double)allMailCount / mailCapacity - Mod.m_Setting.PO_OverflowPercentage / 100.0;

					EconomyUtils.AddResources(Resource.LocalMail, (int)(-localMailCount * removalRate), resourcesBuffer);
					EconomyUtils.AddResources(Resource.OutgoingMail, (int)(-outgoingMailCount * removalRate), resourcesBuffer);
					EconomyUtils.AddResources(Resource.UnsortedMail, (int)(-unsortedMailCount * removalRate), resourcesBuffer);

					var old = allMailCount;
					EconomyUtils.AddResources(Resource.LocalMail, -1 * (int)Math.Ceiling(localMailCount * removalRate), resourcesBuffer);
					EconomyUtils.AddResources(Resource.OutgoingMail, -1 * (int)Math.Ceiling(outgoingMailCount * removalRate), resourcesBuffer);
					EconomyUtils.AddResources(Resource.UnsortedMail, -1 * (int)Math.Ceiling(unsortedMailCount * removalRate), resourcesBuffer);
					allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

					Mod.log.Info($"Overflow: {postEntity}.All: {old} -> {allMailCount}");
				}
			} else {    // 郵便物仕分け施設
				if(Mod.m_Setting.PSF_GetUnsortedMails && unsortedMailCount * 100 / mailCapacity <= Mod.m_Setting.PSF_TriggerPercentage) {
					EconomyUtils.AddResources(Resource.UnsortedMail, mailCapacity * Mod.m_Setting.PSF_GettingPercentage / 100, resourcesBuffer);

					var old = unsortedMailCount;
					unsortedMailCount = EconomyUtils.GetResources(Resource.UnsortedMail, resourcesBuffer);
					allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

					Mod.log.Info($"Get: {postEntity}.UnsortedMail: {old} -> {unsortedMailCount}");
				}
				if(Mod.m_Setting.PSF_DisposeOverflow && allMailCount * 100 / mailCapacity >= Mod.m_Setting.PSF_OverflowPercentage) {
					var removalRate = (double)allMailCount / mailCapacity - Mod.m_Setting.PSF_OverflowPercentage / 100.0;

					EconomyUtils.AddResources(Resource.LocalMail, -1 * (int)Math.Ceiling(localMailCount * removalRate), resourcesBuffer);
					EconomyUtils.AddResources(Resource.OutgoingMail, -1 * (int)Math.Ceiling(outgoingMailCount * removalRate), resourcesBuffer);
					EconomyUtils.AddResources(Resource.UnsortedMail, -1 * (int)Math.Ceiling(unsortedMailCount * removalRate), resourcesBuffer);

					var old = allMailCount;
					localMailCount = EconomyUtils.GetResources(Resource.LocalMail, resourcesBuffer);
					outgoingMailCount = EconomyUtils.GetResources(Resource.OutgoingMail, resourcesBuffer);
					unsortedMailCount = EconomyUtils.GetResources(Resource.UnsortedMail, resourcesBuffer);
					allMailCount = localMailCount + outgoingMailCount + unsortedMailCount;

					Mod.log.Info($"Overflow: {postEntity}.All: {old} -> {allMailCount}");
				}
			}
		}
    }

    public PostalHelperSystem()
    {
    }
}
