using MapGeneration;
using UnityEngine;

using Room = LabApi.Features.Wrappers.Room;

namespace SCP1162
{
    
    internal static class Utils
    {
        
        /// <summary>
        /// Calculates the global coords of a point inside a room based on the room type and the location
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="localPos"></param>
        /// <returns>Vector3</returns>
        public static Vector3 GetGlobalCords(RoomName roomType, Vector3 localPos)
        {
            Room room = Room.Get(roomType).First();

            Quaternion rotation = room.Rotation;
            Vector3 roomPos = room.Position;

            var offsetY = Math.Round(Math.Abs(rotation.eulerAngles.y / 90f));

            return offsetY switch
            {
                0 => new Vector3(roomPos.x + localPos.x, roomPos.y + localPos.y, roomPos.z + localPos.z),
                1 => new Vector3(roomPos.x + localPos.z, roomPos.y + localPos.y, roomPos.z - localPos.x),
                2 => new Vector3(roomPos.x - localPos.x, roomPos.y + localPos.y, roomPos.z - localPos.z),
                3 => new Vector3(roomPos.x - localPos.z, roomPos.y + localPos.y, roomPos.z + localPos.x),
                _ => Vector3.zero
            };
        }
        
        /// <summary>
        /// Retrieves the display name of the specified item type.
        /// </summary>
        /// <param name="item">The <see cref="ItemType"/> for which to retrieve the display name.</param>
        /// <returns>A <see cref="string"/> representing the display name of the specified item type.</returns>
        /// <exception cref="ArgumentException">Thrown if the specified <paramref name="item"/> is not a recognized <see cref="ItemType"/>.</exception>
        public static string GetItemName(ItemType item)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            // ^ Because all the "missing" arms are event items
            return item switch
            {
                ItemType.KeycardJanitor => "Janitor Keycard",
                ItemType.KeycardScientist => "Scientist Keycard",
                ItemType.KeycardResearchCoordinator => "Research Coordinator Keycard",
                ItemType.KeycardZoneManager => "Zone Manager Keycard",
                ItemType.KeycardGuard => "Guard Keycard",
                ItemType.KeycardMTFPrivate => "MTF Private Keycard",
                ItemType.KeycardContainmentEngineer => "Containment Engineer Keycard",
                ItemType.KeycardMTFOperative => "MTF Operative Keycard",
                ItemType.KeycardMTFCaptain => "MTF Captain Keycard",
                ItemType.KeycardFacilityManager => "Facility Manager Keycard",
                ItemType.KeycardChaosInsurgency => "Chaos Insurgency Keycard",
                ItemType.KeycardO5 => "O5 Keycard",
                ItemType.Radio => "Radio",
                ItemType.GunCOM15 => "COM-15",
                ItemType.Medkit => "Medkit",
                ItemType.Flashlight => "Flashlight",
                ItemType.MicroHID => "Micro HID",
                ItemType.SCP500 => "SCP-500",
                ItemType.SCP207 => "SCP-207",
                ItemType.Ammo12gauge => "12-Gauge",
                ItemType.GunE11SR => "E-11",
                ItemType.GunCrossvec => "Crossvec",
                ItemType.Ammo556x45 => "5.56mm",
                ItemType.GunFSP9 => "FSP-9",
                ItemType.GunLogicer => "Logicer",
                ItemType.GrenadeHE => "Grenade",
                ItemType.GrenadeFlash => "Flashbang",
                ItemType.Ammo44cal => ".44mm",
                ItemType.Ammo762x39 => "7.62mm",
                ItemType.Ammo9x19 => "9mm Parabellum",
                ItemType.GunCOM18 => "COM-18",
                ItemType.SCP018 => "SCP-018",
                ItemType.SCP268 => "SCP-268",
                ItemType.Adrenaline => "Adrenaline",
                ItemType.Painkillers => "Painkillers",
                ItemType.Coin => "American Quarter",
                ItemType.ArmorLight => "Light Armor",
                ItemType.ArmorCombat => "Combat Armor",
                ItemType.ArmorHeavy => "Heavy Armor",
                ItemType.GunRevolver => "Revolver",
                ItemType.GunAK => "AK-15",
                ItemType.GunShotgun => "Shotgun",
                ItemType.SCP2176 => "SCP-2176",
                ItemType.SCP244a => "SCP-244-A",
                ItemType.SCP244b => "SCP-244-B",
                ItemType.SCP1853 => "SCP-1853",
                ItemType.ParticleDisruptor => "Particle Disruptor",
                ItemType.GunCom45 => "Glockinator",
                ItemType.SCP1576 => "SCP-1576",
                ItemType.Jailbird => "Jailbird",
                ItemType.AntiSCP207 => "SCP-207?",
                ItemType.GunFRMG0 => "Captains' gun",
                ItemType.GunA7 => "A7",
                ItemType.Lantern => "Lantern",
                ItemType.SCP1344 => "SCP-1344-6",
                ItemType.SurfaceAccessPass => "Surface Access Pass",
                ItemType.GunSCP127 => "SCP-127",
                _ => throw new ArgumentException($"Unknown item type: {item}"),
            };
        }
    }
}
