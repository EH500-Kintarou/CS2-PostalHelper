using Unity.Collections;
using Unity.Entities;
using Colossal.Entities;
using Game;
using Game.Prefabs;
using Game.Buildings;
using Game.Economy;

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
        foreach (Entity postEntity in m_PostFacilitiesQuery.ToEntityArray(Allocator.Temp))
        {
            if (!EntityManager.TryGetComponent<PrefabRef>(postEntity, out PrefabRef prefab))
            {
                Mod.log.Warn($"Failed to retrieve PrefabRef for {postEntity}.");
                continue;
            }
            if (!EntityManager.TryGetComponent<PostFacilityData>(prefab, out PostFacilityData postFacilityData))
            {
                Mod.log.Warn($"Failed to retrieve PostFacilityData for {prefab}.");
                continue;
            }
            if (!EntityManager.TryGetBuffer<Resources>(postEntity, false, out DynamicBuffer<Resources> resourcesBuffer))
            {
                Mod.log.Warn($"Failed to retrieve Resources buffer for {postEntity}.");
                continue;
            }
#if DEBUG
            Mod.log.Info($"{postEntity}: SortingRate {postFacilityData.m_SortingRate} UnsortedMail {EconomyUtils.GetResources(Resource.UnsortedMail, resourcesBuffer)} LocalMail {EconomyUtils.GetResources(Resource.LocalMail, resourcesBuffer)}");
#endif
            if (postFacilityData.m_SortingRate == 0)
            {
                // PostOffice
                if (EconomyUtils.GetResources(Resource.LocalMail, resourcesBuffer) < 4000)
                {
                    EconomyUtils.AddResources(Resource.LocalMail, 4000, resourcesBuffer);
                    Mod.log.Info($"{postEntity}.LocalMail: {EconomyUtils.GetResources(Resource.LocalMail, resourcesBuffer)}");
                }
            }
            else
            {
                // PostSortingFacility
                if (EconomyUtils.GetResources(Resource.UnsortedMail, resourcesBuffer) < 25000)
                {
                    EconomyUtils.AddResources(Resource.UnsortedMail, 25000, resourcesBuffer);
                    Mod.log.Info($"{postEntity}.UnsortedMail: {EconomyUtils.GetResources(Resource.UnsortedMail, resourcesBuffer)}");
                }
            }
        }
    }

    public PostalHelperSystem()
    {
    }
}
