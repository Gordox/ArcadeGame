using HeroSiege.FTexture2D;

namespace HeroSiege.FGameObject.Items.Armors
{
    abstract class Armor : Item
    {
        public ArmorType ArmorType { get; protected set; }

        protected int inteligence, agility, strength, armor;

        public Armor(TextureRegion region, ItemType itemType, ArmorType armorType)
            : base(region, itemType)
        {
            this.ArmorType = armorType;
        }



        public int GetItemInteligence
        {
            get { if (inteligence > 0) return inteligence; else return 0; }
        }

        public int GetItemAgility
        {
            get { if (agility > 0) return agility; else return 0; }
        }

        public int GetItemStrength
        {
            get { if (strength > 0) return strength; else return 0; }
        }

        public int GetItemArmor
        {
            get { if (armor > 0) return armor; else return 0; }
        }
    }
}
