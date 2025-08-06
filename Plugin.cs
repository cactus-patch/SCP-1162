using Exiled.API.Features;
using ServerEvents = Exiled.Events.Handlers.Server;

namespace SCP1162
{

    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "SCP_1162";
        public override string Name => "SCP-1162";
        public override string Author => "Noobest1001";
        public override Version Version => new(1, 0, 0);
        public override Version RequiredExiledVersion => new(9, 7, 0);
        private EventHandler? _eventHandler;
        internal static Plugin? Instance;

        public override void OnEnabled()
        {
            Instance = this;
            _eventHandler = new EventHandler(this);
            ServerEvents.RoundStarted += _eventHandler.OnRoundStarted;


            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerEvents.RoundStarted -= _eventHandler!.OnRoundStarted;
            _eventHandler = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}