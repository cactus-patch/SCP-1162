using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.Features;
using Server = Exiled.Events.Handlers.Server;
using Interact = LabApi.Events.Handlers.PlayerEvents;

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
            
            Server.RoundStarted += _eventHandler.OnRoundStarted;
            Server.RoundEnded += _eventHandler.OnRoundEnded;
            Interact.InteractedToy += _eventHandler.OnPlayerUsedToy;


            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= _eventHandler!.OnRoundStarted;
            Server.RoundEnded -= _eventHandler!.OnRoundEnded;
            Interact.InteractedToy -= _eventHandler.OnPlayerUsedToy;
            
            
            _eventHandler = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}