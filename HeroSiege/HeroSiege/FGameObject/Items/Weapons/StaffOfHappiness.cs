using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Weapons
{
    class StaffOfHappiness : Weapon
    {

        const string ITEM_NAME = "Staff of Happiness";
        const int ITEM_COST = 1800;

        const int STRENGTH = 5;
        const int INTELIGENCE = 30;
        const int AGILITY = 30;
        const int DAMAGE = 2;

        public StaffOfHappiness(TextureRegion region)
            : base(region, ItemType.Weapon, WeaponType.StaffOfHappiness)
        {
        }
        public StaffOfHappiness()
            : base(ItemType.Weapon, WeaponType.StaffOfHappiness)
        {
        }

        protected override void InitAtributes()
        {
            base.InitAtributes();
            ItemName = ITEM_NAME;
            cost = ITEM_COST;
            strength = STRENGTH;
            inteligence = INTELIGENCE;
            agility = AGILITY;
            damage = DAMAGE;
        }
    }
}
