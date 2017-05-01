using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;

namespace HeroSiege.FGameObject.Items
{
    class Item : GameObject
    {
        public ItemType ItemType { get; protected set; }
        public string ItemName { get; protected set; }

        public int Quantity { get; set; }
        protected int maxQuantity;
        protected int cost;

        public Item() : base(null, 0, 0, 0, 0) { }
        public Item(ItemType itemType) : base(null, 0, 0, 0, 0) { this.ItemType = itemType; InitAtributes(); }
        public Item(TextureRegion region, ItemType itemType)
            : base(region, 0, 0, region.region.Width, region.region.Height)
        {
            this.ItemType = itemType;
            InitAtributes();
        }

        protected virtual void InitAtributes() { }

        public bool CanGetMore()
        {
            if (Quantity < maxQuantity)
                return true;
            else
                return false;
        }
        public ItemType GetItemType
        {
            get { return ItemType; }
        }
        public int GetItemCost
        {
            get { return cost; }
        }

        public TextureRegion GetTexture
        {
            get
            {
                if (sprite.Region != null)
                    return sprite.Region;
                else
                    return null;
            }
        }
    }
}
