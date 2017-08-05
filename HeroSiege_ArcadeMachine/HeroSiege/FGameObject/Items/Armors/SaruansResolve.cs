using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Armors
{
    class SaruansResolve : Armor
    {

        const string ITEM_NAME = "Saruan's Resolve";
        const int ITEM_COST = 1500;

        const int STRENGTH = 10;
        const int INTELIGENCE = 50;
        const int AGILITY = 20;
        const int ARMOR = 30;

        public SaruansResolve(TextureRegion region)
            : base(region, ItemType.Armor, ArmorType.SaruansResolve)
        {
        }
        public SaruansResolve()
            : base(ItemType.Armor, ArmorType.SaruansResolve)
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
            armor = ARMOR;
        }
    }
}
