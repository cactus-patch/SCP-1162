using System.ComponentModel;
using System.Drawing;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace SCP1162
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should debug messages be displayed?")]
        public bool Debug { get; set; } = false;

        [Description("Use Hints instead of Broadcast?")]
        public bool UseHints { get; set; } = true;

        [Description("The Vertical scale of the 1162 hole")]
        public float Vertical { get; set; } = 0.01f;

        [Description("The chance of rolling a loss")]
        public double LossChance { get; set; } = 0.15;

        [Description("Pool of all possible items to be awareded.")]
        public ItemType[] Pool { get; set; } = 
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
            ItemType.GunCOM15,
            ItemType.GunCOM18,
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


        [Description("Room used for SCP-1162.")]
        public RoomType RoomType { get; set; } = RoomType.Lcz173; 
    }
}