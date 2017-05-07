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
        const int ITEM_COST = 150;

        const int STRENGTH = 0;
        const int INTELIGENCE = 0;
        const int AGILITY = 0;
        const int DAMAGE = 0;

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
