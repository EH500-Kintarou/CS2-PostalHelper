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

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMails)), "Get local mails" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_TriggerPercentage)), "Threshold of getting" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "Getting amount" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_DisposeOverflow)), "Dispose overflow" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "Overflow amount" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMails)), "Activate getting local mails when remaining is equal or less than the specified percentage of full capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_TriggerPercentage)), "When the remaining amaount percentage of full capacity is equal or less than this, the spcified amount of unsorted mails will be gotten." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)), "When getting local mails, the amount will be this of the full capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_DisposeOverflow)), "Activate disposing mails when amount is equal or more than the specified percentage of full capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)), "When mails is equal or more than this amount of full capacity, cut off to this amount." },

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMails)), "Get unsorted mails" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_TriggerPercentage)), "Threshold of getting" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingAmount)), "Getting amount" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_DisposeOverflow)), "Dispose overflow" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "Overflow amount" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMails)), "Activate getting unsorted mails when remaining is equal or less than the specified percentage of full capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_TriggerPercentage)), "When the remaining amaount percentage of full capacity is equal or less than this, the spcified amount of unsorted mails will be gotten." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingAmount)), "When getting unsorted mails, the amount will be this of the full capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_DisposeOverflow)), "Activate disposing mails when amount is equal or more than the specified percentage of full capacity." },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)), "When mails is equal or more than this amount of full capacity, cut off to this amount." },

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
