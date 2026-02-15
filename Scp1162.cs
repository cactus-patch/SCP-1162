using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;

namespace SCP1162
{

    public class Scp1162 : Plugin
    {
        public static Scp1162? Instance { get; private set; }
        
        public override string Name => "SCP-1162";
        public override string Description => "Adds a hole that can be used to gamble items for a chance to win better items. Based on SCP-1162.";
        public override string Author => "Noobest1001";
        public override Version Version => new(2, 0, 0);
        public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

        public Config Config { get; private set; } = null!;
        private EventHandler _eventHandler = null!;


        public override void Enable()
        {
            Instance = this;
            _eventHandler = new EventHandler();
        }

        public override void LoadConfigs()
        {
            Config = this.TryLoadConfig("config.yml", out Config? _) ? Config : new Config();

            base.LoadConfigs();
        }

        public override void Disable()
        {
            _eventHandler = null;
            Instance = null;
        }
    }
}