using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Armors
{
    class DragonScaleChest : Armor
    {

        const string ITEM_NAME = "Dragonscale Chest";
        const int ITEM_COST = 150;

        const int STRENGTH = 10;
        const int INTELIGENCE = 0;
        const int AGILITY = 0;
        const int ARMOR = 0;

        public DragonScaleChest(TextureRegion region)
            : base(region, ItemType.Armor, ArmorType.DragonScaleChest)
        {
        }
        public DragonScaleChest()
            : base(ItemType.Armor, ArmorType.DragonScaleChest)
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
