using Colossal.Logging;
using Colossal.IO.AssetDatabase;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace PostalHelper;

public class Mod : IMod
{
    // mod's instance and asset
    public static Mod instance { get; private set; }
    public static ExecutableAsset modAsset { get; private set; }
    // logging
    public static ILog log = LogManager.GetLogger($"{nameof(PostalHelper)}").SetShowsErrorsInUI(false);

    public void OnLoad(UpdateSystem updateSystem)
    {
        log.Info(nameof(OnLoad));

        if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
        {
            log.Info($"{asset.name} v{asset.version} mod asset at {asset.path}");
            modAsset = asset;
        }

        // Run the system before simulation phase starts
        updateSystem.UpdateBefore<PostalHelperSystem>(SystemUpdatePhase.GameSimulation);
    }

    public void OnDispose()
    {
        log.Info(nameof(OnDispose));
    }
}
