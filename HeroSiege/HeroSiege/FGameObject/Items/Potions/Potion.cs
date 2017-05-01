using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;

namespace HeroSiege.FGameObject.Items.Potions
{
    abstract class Potion : Item
    {
        public PotionType PotionType { get; protected set; }

        protected int healing, mana;

        public Potion(TextureRegion region, ItemType itemType, PotionType potionType)
            : base(region, itemType)
        {
            this.PotionType = potionType;
        }
        public Potion(ItemType itemType, PotionType potionType)
            : base(itemType)
        {
            this.PotionType = potionType;
        }

        protected override void InitAtributes()
        {
            base.InitAtributes();
            maxQuantity = 99;
        }

        public PotionType GetPotionType
        {
            get { return PotionType; }
        }
        public int Healing
        {
            get { if (healing > 0) return healing; else return 0; }
        }
        public int ManaRestoring
        {
            get { if (mana > 0) return mana; else return 0; }
        }
    }
}
