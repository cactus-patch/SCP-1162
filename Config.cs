using System.ComponentModel;
using MapGeneration;

namespace SCP1162
{
    public sealed class Config
    {
        [Description("The Vertical scale of the 1162 hole")]
        public float Vertical { get; set; } = 0.01f;

        [Description("The chance of rolling a loss")]
        public double LossChance { get; set; } = 0.15;

        [Description("Whether a player should take damage or die when gambling with nothing.")]
        public bool DamageOnHand { get; set; } = false;
        
        [Description("How much damage to deal when gambling with nothing and DamageOnHand is true.")]
        public float DamageAmount { get; set; } = 50f;

        [Description("Pool of all possible items to be awareded.")]
        public ItemType[]? Pool { get; set; } =
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
        public RoomName RoomName { get; set; } = RoomName.Lcz173; 
    }
}