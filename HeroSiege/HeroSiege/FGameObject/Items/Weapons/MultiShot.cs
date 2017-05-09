using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Weapons
{
    class MultiShot : Weapon
    {

        const string ITEM_NAME = "Multi Shot";
        const int ITEM_COST = 500;

        const int STRENGTH = 2;
        const int INTELIGENCE = 50;
        const int AGILITY = 50;
        const int DAMAGE = 5;

        public MultiShot(TextureRegion region)
            : base(region, ItemType.Weapon, WeaponType.MultiShot)
        {
        }
        public MultiShot()
            : base(ItemType.Weapon, WeaponType.MultiShot)
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
