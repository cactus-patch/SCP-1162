using Exiled.API.Enums;
using Exiled.API.Features;
using ExiledR = Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LabAPI = LabApi.Features.Wrappers;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using ExiledIAPI= Exiled.API.Features.Items.Item;
using ServerEvents = Exiled.Events.Handlers.Server;
using YamlDotNet.Serialization;
using InventorySystem;
using Exiled.API.Extensions;
using Exiled.API.Features.Toys;
using AdminToys;
using Exiled.Events.Handlers;
using Exiled.API.Structs;

namespace SCP1162
{
    public class EventHandler(Plugin plugin)
    {
        private readonly Plugin _plugin = plugin;
        private readonly ItemType[] Pool = Plugin.Instance.Config.Pool;
        [YamlIgnore] private readonly System.Random _rng = new();

        public void OnRoundStarted()
        {
            ExiledR.Room room = ExiledR.Room.Get(_plugin.Config.RoomType);
            Vector3 position = Utils.GetGlobalCords(_plugin.Config.RoomType, new Vector3(16.68f, 11.43f, 8.11f));
            Quaternion rotation = room.Rotation;
            Vector3 scale = new(2f, _plugin.Config.Vertical, 2f);

            var Interactable = LabAPI.InteractableToy.Create(position, rotation, scale);
            
            var Visible = Primitive.Create(new PrimitiveSettings(PrimitiveType.Sphere,Color.black,position,Vector3.zero,scale,true));

            Interactable.InteractionDuration = 0.5f;

            Interactable.OnSearched += player => Gamble(player.CurrentItem, player);
        }

        private void Gamble(LabApi.Features.Wrappers.Item? item, Exiled.API.Features.Player player)
        {
            try
            {
                System.Random random = new();
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
                    else
                    {
                        player.RemoveHeldItem(true);
                        if (_rng.NextDouble() < _plugin.Config.LossChance) { player.ShowHint($"You insert {Utils.GetItemName(item.Type)} and get nothing in return"); return; }
                        else
                        {
                            int _temp = _rng.Next(0, Pool.Length);
                            var newItem = player.AddItem(Pool[_temp]);
                            player.AddItem(newItem);
                            player.ShowHint($"You got {Utils.GetItemName(Pool[_temp])} from SCP-1162", 7.5f);
                            player.CurrentItem = newItem;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log.Error($"Error in Gamble method: {ex.Message}\nStackTrace: {ex.StackTrace}\nInner: {ex.InnerException}\nSource: {ex.Source}");
                player.ShowHint("Something went wrong!\nYou should try again and if this continues: \nDM Noobest1001,\nmake an issue on GitHub,\nor make a ticket at https://discord.com/channels/1262120573148856341/1291640765805629543 and ping @noobest1001", 5f);
            }

            return;
        }
    }
}