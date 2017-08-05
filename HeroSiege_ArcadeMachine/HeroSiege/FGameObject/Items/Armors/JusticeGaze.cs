using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Armors
{
    class JusticeGaze : Armor
    {

        const string ITEM_NAME = "Justice Gaze";
        const int ITEM_COST = 1500;

        const int STRENGTH = 40;
        const int INTELIGENCE = 10;
        const int AGILITY = 5;
        const int ARMOR = 30;

        public JusticeGaze(TextureRegion region)
            : base(region, ItemType.Armor, ArmorType.JusticeGaze)
        {
        }
        public JusticeGaze()
            : base(ItemType.Armor, ArmorType.JusticeGaze)
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
