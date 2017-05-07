using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items
{
    public enum ItemType
    {
        NONE,
        Potion,
        Armor,
        Weapon
    }

    public enum PotionType
    {
        NONE,
        HealingPotion,
        GreaterHealingPotion,
        ManaPotion,
        GreaterManaPotion,
        RejuvenationPotion
    }

    public enum WeaponType
    {
        NONE,
        Cleave,
        MultiShot,
        Devastation,
        StaffOfHappiness
    }

    public enum ArmorType
    {
        NONE,
        AggramarsStride,
        CloakOfWisdom,
        DragonScaleChest,
        FirestoneWalkers,
        JusticeGaze,
        SaruansResolve
    }
}
