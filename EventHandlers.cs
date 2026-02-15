using AdminToys;
using CustomPlayerEffects;
using InventorySystem;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using PlayerRoles;
using UnityEngine;
using YamlDotNet.Serialization;
using Interact = LabApi.Events.Handlers.PlayerEvents;
using Logger = LabApi.Features.Console.Logger;
using Player = LabApi.Features.Wrappers.Player;
using PrimitiveObjectToy = LabApi.Features.Wrappers.PrimitiveObjectToy;
using Random = System.Random;
using Room = LabApi.Features.Wrappers.Room;
using Server = LabApi.Events.Handlers.ServerEvents;

namespace SCP1162
{
    public class EventHandler
    {
        private static Config _config => Scp1162.Instance.Config;

        internal EventHandler()
        {
            Server.RoundStarted += OnRoundStarted;
            Interact.InteractedToy += OnPlayerUsedToy;
        }

        ~EventHandler()
        {
            Server.RoundStarted -= OnRoundStarted;
            Interact.InteractedToy -= OnPlayerUsedToy;

            _interactable?.Destroy();
            _visible.Destroy();
        }
        
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
        
        private readonly ItemType[] _pool = _config.Pool ?? Fallback;
        [YamlIgnore] private readonly Random _rng = new();

        private InteractableToy? _interactable;
        private PrimitiveObjectToy _visible;

        private void OnRoundStarted()
        {
            Room room = Room.Get(_config.RoomName).First();
            Vector3 position = Utils.GetGlobalCords(_config.RoomName, new Vector3(16.68f, 11.43f, 8.11f));
            Quaternion rotation = room.Rotation;
            Vector3 scale = new(2f, _config.Vertical, 2f);
            
            _interactable = InteractableToy.Create(room.Transform);
            _interactable.Position = position;
            _interactable.Rotation = rotation;
            _interactable.Scale = scale;
            _interactable.IsStatic = true;
            _interactable.InteractionDuration = 0.5f;

            if (_interactable is null)
            {
                Logger.Error("Interactable is null");
                return;
            }

            _visible = PrimitiveObjectToy.Create(room.Transform);
            _visible.Position = position + new Vector3(0, .2f ,0);
            _visible.Rotation = rotation;
            _visible.Scale = scale;
            _visible.Parent = room.Transform;
            _visible.Color = new Color(0, 0, 0);
            _visible.Flags = PrimitiveFlags.Visible;
            _visible.Type = PrimitiveType.Sphere;
            _visible.IsStatic = true;
            
            _interactable.Spawn();
            _visible.Spawn();
        }

        private void OnPlayerUsedToy(PlayerInteractedToyEventArgs ev)
        {
            if (ev.Interactable == _interactable)
            {
                GambleDec(ev.Player.CurrentItem, ev.Player);
            }
        }
        
        private void GambleDec(Item? item, Player player)
        {
            if (player.Team != Team.SCPs && !player.IsDisarmed)
            {
                try
                {
                    if (item == null)
                    {
                        if (_config.DamageOnHand)
                        {
                            player.Damage(_config.DamageAmount, null, armorPenetration: 100);
                        }
                        else
                        {
                            player.EnableEffect<SeveredHands>(duration: 100f);
                            player.SendHint("You insert your hands into SCP-1162 and lose feeling in them.", 10f);
                            player.SendHitMarker(2f);  
                        }
                    }
                    
                    else if (item.Category is ItemCategory.SCPItem or ItemCategory.SpecialWeapon)
                    {
                        player.SendHint("You put " + Utils.GetItemName(item.Type) + " in and SCP-1162 just spits it back out", 7.5f);
                    }
                    else
                    {
                        if (item.Type is ItemType.SCP330)
                        {
                            player.SendHint("You put an SCP-330-1 instance in and SCP-1162 just spits it back out", 7.5f);
                            return;
                        }
                        Gamble(player);
                        
                    }
                }
                catch (Exception ex)
                {
                    player.CurrentItem = null;
                    Logger.Error(ex.Message);
                    Logger.Error(ex.Source);
                }
            }
        }

        private void Gamble(Player player)
        {
            Inventory _inv = player.Inventory;
            ItemType item = _inv.CurInstance.ItemTypeId;
            player.RemoveItem(item);
            
            if (_rng.NextDouble() < _config.LossChance)
            {
                player.SendHint($"You insert {Utils.GetItemName(item)} and get nothing in return");
                return;
            }
            // ReSharper disable once InconsistentNaming
            var _temp = _rng.Next(0, _pool.Length);
            Item newItem = player.AddItem(_pool[_temp]);
            
            player.AddItem(newItem.Type);
            player.SendHint($"You got {Utils.GetItemName(_pool[_temp])} from SCP-1162", 7.5f);
            player.CurrentItem = newItem;
        }
    }
}