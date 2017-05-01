using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;

namespace HeroSiege.FGameObject.Items.Potions
{
    class ManaPotion : Potion
    {
        const string ITEM_NAME = "SIMONS MAMMA";
        const int MANA_RESTORING = 200;
        const int ITEM_COST = 150;

        public ManaPotion(TextureRegion region)
            : base(region, ItemType.Potion, PotionType.ManaPotion)
        {
        }
        public ManaPotion()
            : base(ItemType.Potion, PotionType.ManaPotion)
        {
        }
        protected override void InitAtributes()
        {
            base.InitAtributes();
            ItemName = ITEM_NAME;
            mana = MANA_RESTORING;
            cost = ITEM_COST;
            Quantity = 1;
        }
    }
}
