using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Potions
{
    class HealingPotion : Potion
    {
        const string ITEM_NAME = "TIMS MAMMA";
        const int HEALTH_RESTORING = 200;
        const int ITEM_COST = 150;

        public HealingPotion(TextureRegion region)
            : base(region, ItemType.Potion, PotionType.HealingPotion)
        {
        }

        public HealingPotion()
            : base(ItemType.Potion, PotionType.HealingPotion)
        {
        }

        protected override void InitAtributes()
        {
            base.InitAtributes();
            ItemName = ITEM_NAME;
            healing = HEALTH_RESTORING;
            cost = ITEM_COST;
            Quantity = 1;
        }
    }
}
