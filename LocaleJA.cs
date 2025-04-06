using Colossal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalHelper;

public class LocaleJA : IDictionarySource
{
	private readonly Setting m_Setting;
	public LocaleJA(Setting setting)
	{
		m_Setting = setting;
	}
	public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
	{
		return new Dictionary<string, string>
		{
			{ m_Setting.GetSettingsLocaleID(), "Postal Helper" },
			{ m_Setting.GetOptionTabLocaleID(Setting.MainSection), "メイン" },
			{ m_Setting.GetOptionGroupLocaleID(Setting.PostOfficeGroup), "郵便局" },
			{ m_Setting.GetOptionGroupLocaleID(Setting.PostSortingFacilityGroup), "郵便物仕分け施設" },
			{ m_Setting.GetOptionGroupLocaleID(Setting.ResetGroup), "リセット" },

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GetLocalMail)), "市内郵便を取得する" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "取得しきい値" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_GettingPercentage)), "取得量" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_DisposeOverflow)), "溢れた郵便物を廃棄" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PO_OverflowPercentage)), "溢れしきい値" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GetLocalMail)), "市内郵便の残量が設定値以下になったときに指定した量の市内郵便を取得する機能を有効にします。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingThresholdPercentage)), "市内郵便の残量が郵便局の容量のこのパーセンテージ以下になったときに市内郵便を取得します。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_GettingPercentage)), "市内郵便を受け取る量を郵便局の容量のパーセンテージで指定します。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_DisposeOverflow)), "郵便物の残量が設定値以上になったときにそれ以上を廃棄する機能を有効にします。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PO_OverflowPercentage)), "郵便物の残量が郵便局の容量のこのパーセンテージ以上になったときに溢れた分の郵便物を廃棄します。" },

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "未分類郵便を取得する" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "取得しきい値" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_GettingPercentage)), "取得量" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_DisposeOverflow)), "溢れた郵便物を廃棄" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.PSF_OverflowPercentage)), "溢れしきい値" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GetUnsortedMail)), "未分類郵便の残量が設定値以下になったときに指定した量の未分類郵便を取得する機能を有効にします。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingThresholdPercentage)), "未分類郵便の残量が郵便物仕分け施設の容量のこのパーセンテージ以下になったときに未分類郵便を取得します。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_GettingPercentage)), "未分類郵便を受け取る量を郵便物仕分け施設の容量のパーセンテージで指定します。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_DisposeOverflow)), "郵便物の残量が設定値以上になったときにそれ以上を廃棄する機能を有効にします。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.PSF_OverflowPercentage)), "郵便物の残量が郵便物仕分け施設の容量のこのパーセンテージ以上になったときに溢れた分の郵便物を廃棄します。" },

			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToVanilla)), "バニラの設定値にする" },
			{ m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToRecommend)), "推奨設定値にする" },

			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToVanilla)), "この設定にするとバニラの挙動と変わらなくなります。" },
			{ m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToRecommend)), "推奨値に設定します。" },
		};
	}

	public void Unload()
	{
	}
}
