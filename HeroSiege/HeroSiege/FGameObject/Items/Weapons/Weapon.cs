using HeroSiege.FTexture2D;

namespace HeroSiege.FGameObject.Items.Weapons
{
    abstract class Weapon : Item
    {
        public WeaponType WeaponType { get; protected set; }

        protected int inteligence, agility, strength, damage;

        public Weapon(TextureRegion region, ItemType itemType, WeaponType weaponType)
            : base(region, itemType)
        {
            this.WeaponType = weaponType;
        }
        public Weapon(ItemType itemType, WeaponType weaponType)
           : base(itemType)
        {
            this.WeaponType = weaponType;
        }

        protected override void InitAtributes()
        {
            base.InitAtributes();
            maxQuantity = 1;
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

        public int GetItemDamage
        {
            get { if (damage > 0) return damage; else return 0; }
        }
    }
}
