using Colossal.Logging;
using Colossal.IO.AssetDatabase;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;

namespace PostalHelper;

public class Mod : IMod
{
    // mod's instance and asset
    public static Mod instance { get; private set; }
    public static ExecutableAsset modAsset { get; private set; }
    // logging
    public static ILog log = LogManager.GetLogger($"{nameof(PostalHelper)}").SetShowsErrorsInUI(false);
	// settings
	public static Setting m_Setting;

	public void OnLoad(UpdateSystem updateSystem)
    {
        log.Info(nameof(OnLoad));

        if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
        {
            log.Info($"{asset.name} v{asset.version} mod asset at {asset.path}");
            modAsset = asset;
        }

		m_Setting = new Setting(this);
		m_Setting.RegisterInOptionsUI();
		GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
		GameManager.instance.localizationManager.AddSource("ja-JP", new LocaleJA(m_Setting));

		AssetDatabase.global.LoadSettings(nameof(PostalHelper), m_Setting, new Setting(this));

		// Run the system before simulation phase starts
		updateSystem.UpdateBefore<PostalHelperSystem>(SystemUpdatePhase.GameSimulation);
    }

    public void OnDispose()
    {
        log.Info(nameof(OnDispose));
    }
}
