using Colossal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalHelper;

public class LocaleEN : IDictionarySource
{
	private readonly Setting m_Setting;
	public LocaleEN(Setting setting)
	{
		m_Setting = setting;
	}
	public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
	{
		return new Dictionary<string, string>
		{
			{ m_Setting.GetSettingsLocaleID(), "Postal Helper" },
			{ m_Setting.GetOptionTabLocaleID(Setting.MainSection), "Main" },
			{ m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "Post Office" },
			{ m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "Post Sorting Facility" },
			{ m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "Reset" },

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "Get local mails" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Threshold of getting" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Getting amount" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_DisposeOverflow)), "Dispose overflow" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Overflow amount" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)), "Enable the feature to retrieve the specified amount of local mail when the remaining amount falls below the set value." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "Get local mail when its remaining falls below this percentage of the post office's capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)), "Specify the amount of local mail received as a percentage of the post office's capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_DisposeOverflow)), "Enable the feature to dispose overflow mail if it exceeds the set amount." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)), "When the amount of mail exceeds this percentage of the post office's capacity, the overflow mail will be disposed." },

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Get unsorted mails" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Threshold of getting" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "Getting amount" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_DisposeOverflow)), "Dispose overflow" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Overflow amount" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "Enable the feature to retrieve the specified amount of unsorted mail when the remaining amount falls below the set value." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "Get unsorted mail when its remaining falls below this percentage of the post sorting facility's capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)), "Specify the amount of unsorted mail received as a percentage of the post sorting facility's capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_DisposeOverflow)), "Enable the feature to dispose overflow mail if it exceeds the set amount." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)), "When the amount of mail exceeds this percentage of the post sorting facility's capacity, the overflow mail will be disposed." },

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "Reset to Vanilla" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "Reset to Recommendation" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "This configuration is same as vanilla behavior." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)), "Reset to Recommendation" },
		};
	}

	public void Unload()
	{
	}
}
