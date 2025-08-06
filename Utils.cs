using Exiled.API.Enums;
using Exiled.API.Features;
using EP = Exiled.API.Features.Player;
using ExItem = Exiled.API.Features.Items; // Exiled.API.Features.Items not ExtendedItems
using InventorySystem.Items.Usables.Scp330;
using UnityEngine;

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
        public static Vector3 GetGlobalCords(RoomType roomType, Vector3 localPos)
        {
            Room room = Room.Get(roomType);

            Quaternion rotation = room.Rotation;
            Vector3 roomPos = room.Position;

            double offsetY = Math.Round(Math.Abs(rotation.eulerAngles.y / 90f));

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
        /// Maps an integer value to a corresponding <see cref="CandyKindID"/> enumeration.
        /// </summary>
        /// <param name="candy">An integer representing the type of candy to map.</param>
        /// <returns>A <see cref="CandyKindID"/> value corresponding to the provided integer.  Returns <see
        /// cref="CandyKindID.Pink"/> for 0, <see cref="CandyKindID.Blue"/> for 1,  <see cref="CandyKindID.Green"/> for
        /// 2, <see cref="CandyKindID.Yellow"/> for 3,  <see cref="CandyKindID.Purple"/> for 4, <see
        /// cref="CandyKindID.Rainbow"/> for 5,  and <see cref="CandyKindID.Red"/> for any other value.</returns>
        [Obsolete("Used for adding candy to 1162")]
        public static CandyKindID AddCandy(int candy)
        {
            return candy switch
            {
                0 => CandyKindID.Pink,
                1 => CandyKindID.Blue,
                2 => CandyKindID.Green,
                3 => CandyKindID.Yellow,
                4 => CandyKindID.Purple,
                5 => CandyKindID.Rainbow,
                _ => CandyKindID.Red,
            };
        }

        /// <summary>
        /// Converts a candy identifier to its corresponding color representation as a formatted string.
        /// </summary>
        /// <remarks>The returned string uses HTML-like tags to specify the color, which can be useful for
        /// rendering in environments that support such formatting. For example, a value of 0 returns a pink color,
        /// while a value of 5 returns a rainbow-colored string.</remarks>
        /// <param name="candy">An integer representing the candy type. Valid values range from 0 to 5, where each value corresponds to a
        /// specific color. Any other value defaults to red.</param>
        /// <returns>A string containing the color representation of the candy, formatted with HTML-like color tags.</returns>
        [Obsolete("Used for adding candy to 1162")]
        public static string CandytoString(int candy)
        {
            return candy switch
            {
                0 => "<color=#FFC0CB>Pink</color>",
                1 => "<color=#0000FF>Blue</color>",
                2 => "<color=#008000>Green</color>",
                3 => "<color=#FFFF00>Yellow</color>",
                4 => "<color=#800080>Purple</color>",
                5 => "<color=#FF0000>R</color><color=#FF7F00>a</color><color=#FFFF00>i</color><color=#00FF00>n</color><color=#0000FF>b</color><color=#4B0082>o</color><color=#8A2BE2>w</color>",
                _ => "<color=#FF0000>Red</color>",
            };
        }

        /// <summary>
        /// Gives and equips an item to a player
        /// </summary>
        /// <param name="item"></param>
        /// <param name="player"></param>
        private static void GiveEquip(ExItem.Item item, EP player)
        {
            player.AddItem(item);
            player.CurrentItem = item;
        }

        /// <summary>
        /// Gives and equips <see cref="ExItem.Item"/> the specified equipment item to the player.
        /// </summary>
        /// <remarks>This method creates an item of the specified type and assigns it to the given
        /// player.</remarks>
        /// <param name="itemType">The type of item to be given to the player.</param>
        /// <param name="player">The player who will receive the item.</param>
        public static void GiveEquip(ItemType itemType, EP player)
        {
            ExItem.Item item = ExItem.Item.Create(itemType);
            GiveEquip(item, player);
        }

        /// <summary>
        /// Retrieves the display name of the specified item based on its type.
        /// </summary>
        /// <remarks>This method maps item types to their corresponding display names. If the item type is
        /// not recognized, an exception is thrown.</remarks>
        /// <param name="item">The item for which to retrieve the display name. The item's <see cref="ExItem.Item.Type"/> property
        /// determines the name returned.</param>
        /// <returns>A string representing the display name of the item. For example, "Janitor Keycard" for <see
        /// cref="ItemType.KeycardJanitor"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="item"/> has an unknown or unsupported <see cref="ExItem.Item.Type"/>.</exception>
        public static string GetItemName(ExItem.Item item)
        {
            return item.Type switch
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
                ItemType.GunA7 => "AK74SU",
                ItemType.Lantern => "Lantern",
                ItemType.SCP1344 => "SCP-1344-6",
                ItemType.SurfaceAccessPass => "Surface Access Pass",
                ItemType.GunSCP127 => "SCP-127",
                _ => throw new ArgumentException($"Unknown item type: {item.Type}"),
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
                ItemType.GunA7 => "AK74SU",
                ItemType.Lantern => "Lantern",
                ItemType.SCP1344 => "SCP-1344-6",
                ItemType.SurfaceAccessPass => "Surface Access Pass",
                ItemType.GunSCP127 => "SCP-127",
                _ => throw new ArgumentException($"Unknown item type: {item}"),
            };
        }
    }
}
