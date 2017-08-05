using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Armors
{
    class AggramarsStride : Armor
    {

        const string ITEM_NAME = "Aggramar's Stride";
        const int ITEM_COST = 150;

        const int STRENGTH = 0;
        const int INTELIGENCE = 0;
        const int AGILITY = 0;
        const int ARMOR = 0;

        public AggramarsStride(TextureRegion region)
            : base(region, ItemType.Armor, ArmorType.AggramarsStride)
        {
        }
        public AggramarsStride()
            : base(ItemType.Armor, ArmorType.AggramarsStride)
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
