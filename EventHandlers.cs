using Discord;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.API.Structs;
using Exiled.Events.EventArgs.Server;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using UnityEngine;
using YamlDotNet.Serialization;
using AdminToy = Exiled.API.Features.Toys.AdminToy;
using ExiledR = Exiled.API.Features;

namespace SCP1162
{
    public class EventHandler(Plugin plugin)
    {

        private static readonly ItemType[] Fallback = 
        [
            ItemType.KeycardJanitor,
            ItemType.KeycardZoneManager,
            ItemType.KeycardScientist,
            ItemType.KeycardContainmentEngineer,
            ItemType.KeycardResearchCoordinator,
            ItemType.KeycardMTFPrivate,
            ItemType.KeycardMTFOperative,
            ItemType.KeycardMTFCaptain,
            ItemType.KeycardFacilityManager,
            ItemType.KeycardChaosInsurgency,
            ItemType.KeycardO5,
            ItemType.SurfaceAccessPass,
            ItemType.Painkillers,
            ItemType.Medkit,
            ItemType.Adrenaline,
            ItemType.SCP500,
            ItemType.SCP207,
            ItemType.AntiSCP207,
            ItemType.GrenadeHE,
            ItemType.GrenadeFlash,
            ItemType.Coin,
            ItemType.Flashlight,
            ItemType.Radio,
        ];  
         
        private readonly ItemType[] _pool = Plugin.Instance?.Config.Pool ?? Fallback;
        [YamlIgnore] private readonly System.Random _rng = new();

        private InteractableToy? _interactable;

        public void OnRoundStarted()
        {
            ExiledR.Room room = ExiledR.Room.Get(plugin.Config.RoomType);
            Vector3 position = Utils.GetGlobalCords(plugin.Config.RoomType, new Vector3(16.68f, 11.43f, 8.11f));
            Quaternion rotation = room.Rotation;
            Vector3 scale = new(2f, plugin.Config.Vertical, 2f);

            _interactable = InteractableToy.Create(position, rotation, scale);
            
            // ReSharper disable once InconsistentNaming
            var _visible = Primitive.Create(new PrimitiveSettings(PrimitiveType.Sphere,Color.black,position,Vector3.zero,scale,true));

            
            _interactable.InteractionDuration = 0.5f;
            
        }

        public void OnPlayerUsedToy(PlayerInteractedToyEventArgs ev)
        {
            var player = ExiledR.Player.Get(ev.Player);
            if (ev.Interactable == _interactable)
            {
                Gamble(ev.Player.CurrentItem, player);
            }
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            _interactable = null;
            foreach (var toy in InteractableToy.List)
            {
                toy.Destroy();
            }
            foreach (var primitive in AdminToy.List)
            {
                primitive.Destroy();
            }
        }
        
        private void Gamble(Item? item, ExiledR.Player player)
        {
            if (player is { IsScp: false, IsAlive: true, })
            {
                try
                {
                    if (item == null)
                    {
                        player.EnableEffect(EffectType.SeveredHands);
                        player.ShowHint("You insert your hands into SCP-1162 and lose feeling in them.", 10f);
                        player.ShowHitMarker(2f);
                    }
                    else
                    {
                        if (item.Type is ItemType.SCP330)
                        {
                            player.ShowHint("You put an SCP-330-1 instance in and SCP-1162 just spits it back out", 7.5f);
                            return;
                        }

                        player.RemoveHeldItem();
                        if (_rng.NextDouble() < plugin.Config.LossChance)
                        {
                            player.ShowHint($"You insert {Utils.GetItemName(item.Type)} and get nothing in return");
                            return;
                        }

                        // ReSharper disable once InconsistentNaming
                        int _temp = _rng.Next(0, _pool.Length);
                        var newItem = player.AddItem(_pool[_temp]);
                        player.AddItem(newItem);
                        player.ShowHint($"You got {Utils.GetItemName(_pool[_temp])} from SCP-1162", 7.5f);
                        player.CurrentItem = newItem;
                    }
                }
                catch (Exception ex)
                {
                    Log.Send($"Error in Gamble method: {ex.Message}\nStackTrace: {ex.StackTrace}\nInner: {ex.InnerException}\nSource: {ex.Source}", LogLevel.Error, ConsoleColor.Cyan);
                    player.ShowHint("Something went wrong!\nYou should try again and if this continues: \nDM Noobest1001,\nmake an issue on GitHub,\nor make a ticket at https://discord.com/channels/1262120573148856341/1291640765805629543 and ping @noobest1001", 5f);
                }
            }
        }
    }
}