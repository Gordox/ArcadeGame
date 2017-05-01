using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Items.Potions
{
    class Reincarnation : Potion
    {
        const string ITEM_NAME = "Reincarnation";
        const int HEALTH_RESTORING = 200;
        const int Mana_RESTORING = 200;
        const int ITEM_COST = 1500;

        public Reincarnation(TextureRegion region)
            : base(region, ItemType.Potion, PotionType.RejuvenationPotion)
        {
        }

        public Reincarnation()
            : base(ItemType.Potion, PotionType.RejuvenationPotion)
        {
        }

        protected override void InitAtributes()
        {
            base.InitAtributes();
            ItemName = ITEM_NAME;
            healing = HEALTH_RESTORING;
            mana = Mana_RESTORING;
            cost = ITEM_COST;
            Quantity = 1;
            maxQuantity = 1;
        }
    }
}
