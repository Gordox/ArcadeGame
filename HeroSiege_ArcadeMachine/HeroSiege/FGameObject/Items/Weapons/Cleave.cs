using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;

namespace HeroSiege.FGameObject.Items.Weapons
{
    class Cleave : Weapon
    {
        
        const string ITEM_NAME = "Cleave";
        const int ITEM_COST = 500;

        const int STRENGTH = 20;
        const int INTELIGENCE = 2;
        const int AGILITY = 10;
        const int DAMAGE = 5;

        public Cleave(TextureRegion region)
            : base(region, ItemType.Weapon, WeaponType.Cleave)
        {
        }
        public Cleave()
            : base(ItemType.Weapon, WeaponType.Cleave)
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
