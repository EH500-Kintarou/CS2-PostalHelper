using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace PostalHelper;

[FileLocation(nameof(PostalHelper))]
[SettingsUIGroupOrder(PostOfficeGroup, PostSortingFacilityGroup, ResetGroup)]
[SettingsUIShowGroupName(PostOfficeGroup, PostSortingFacilityGroup, ResetGroup)]
public class Setting : ModSetting
{
	public const string MainSection = "Main";
	public const string PostOfficeGroup = "Post Office";
	public const string PostSortingFacilityGroup = "Post Sorting Facility";
	public const string ResetGroup = "Reset";

	public Setting(IMod mod) : base(mod)
	{
		if(NotFirstTime) {
			SetDefaults();
			NotFirstTime = true;
		}
	}

	public override void Apply()
	{
		base.Apply();
		var system = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PostalHelperSystem>();
		system.Enabled = true;
	}

	#region Post Office

	[SettingsUISection(MainSection, PostOfficeGroup)]
	public bool PO_GetLocalMails { get; set; }

	[SettingsUISection(MainSection, PostOfficeGroup)]
	[SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
	[SettingsUIDisableByCondition(typeof(Setting), nameof(PO_GetLocalMails), true)]
	public int PO_TriggerPercentage { get; set; }

	[SettingsUISection(MainSection, PostOfficeGroup)]
	[SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
	[SettingsUIDisableByCondition(typeof(Setting), nameof(PO_GetLocalMails), true)]
	public int PO_GettingPercentage { get; set; }


	[SettingsUISection(MainSection, PostOfficeGroup)]
	public bool PO_DisposeOverflow { get; set; }

	[SettingsUISection(MainSection, PostOfficeGroup)]
	[SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
	[SettingsUIDisableByCondition(typeof(Setting), nameof(PO_DisposeOverflow), true)]
	public int PO_OverflowPercentage { get; set; }

	#endregion

	#region Post Sorting Facility

	[SettingsUISection(MainSection, PostSortingFacilityGroup)]
	public bool PSF_GetUnsortedMails { get; set; }

	[SettingsUISection(MainSection, PostSortingFacilityGroup)]
	[SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
	[SettingsUIDisableByCondition(typeof(Setting), nameof(PSF_GetUnsortedMails), true)]
	public int PSF_TriggerPercentage { get; set; }

	[SettingsUISection(MainSection, PostSortingFacilityGroup)]
	[SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
	[SettingsUIDisableByCondition(typeof(Setting), nameof(PSF_GetUnsortedMails), true)]
	public int PSF_GettingAmount { get; set; }


	[SettingsUISection(MainSection, PostSortingFacilityGroup)]
	public bool PSF_DisposeOverflow { get; set; }

	[SettingsUISection(MainSection, PostSortingFacilityGroup)]
	[SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
	[SettingsUIDisableByCondition(typeof(Setting), nameof(PSF_DisposeOverflow), true)]
	public int PSF_OverflowPercentage { get; set; }

	#endregion

	#region Reset

	[SettingsUISection(MainSection, ResetGroup)]
	[SettingsUIButton]
	public bool ResetToVanilla
	{
		set
		{
			SetToVanilla();
			Apply();
		}
	}

	[SettingsUISection(MainSection, ResetGroup)]
	[SettingsUIButton]
	public bool ResetToRecommend
	{
		set
		{
			SetDefaults();
			Apply();
		}
	}

	#endregion

	#region Others

	[SettingsUIHidden]
	public bool NotFirstTime { get; set; }

	#endregion

	public override void SetDefaults()
	{
		PO_GetLocalMails = true;
		PO_TriggerPercentage = 2;
		PO_GettingPercentage = 20;
		PO_DisposeOverflow = true;
		PO_OverflowPercentage = 80;

		PSF_GetUnsortedMails = true;
		PSF_TriggerPercentage = 2;
		PSF_GettingAmount = 20;
		PSF_DisposeOverflow = true;
		PSF_OverflowPercentage = 80;
	}

	public void SetToVanilla()
	{
		PO_GetLocalMails = false;
		PO_TriggerPercentage = 2;
		PO_GettingPercentage = 20;
		PO_DisposeOverflow = false;
		PO_OverflowPercentage = 80;

		PSF_GetUnsortedMails = false;
		PSF_TriggerPercentage = 2;
		PSF_GettingAmount = 20;
		PSF_DisposeOverflow = false;
		PSF_OverflowPercentage = 80;
	}
}
