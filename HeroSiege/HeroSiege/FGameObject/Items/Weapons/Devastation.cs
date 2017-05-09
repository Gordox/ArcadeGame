using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Weapons
{
    class Devastation : Weapon
    {

        const string ITEM_NAME = "Devastation";
        const int ITEM_COST = 1800;

        const int STRENGTH = 50;
        const int INTELIGENCE = 5;
        const int AGILITY = 10;
        const int DAMAGE = 5;

        public Devastation(TextureRegion region)
            : base(region, ItemType.Weapon, WeaponType.Devastation)
        {
        }
        public Devastation()
            : base(ItemType.Weapon, WeaponType.Devastation)
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
