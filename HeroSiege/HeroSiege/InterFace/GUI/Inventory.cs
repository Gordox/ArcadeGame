using HeroSiege.FGameObject.Items;
using HeroSiege.FGameObject.Items.Armors;
using HeroSiege.FGameObject.Items.Potions;
using HeroSiege.FGameObject.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.InterFace.GUI
{
    class Inventory
    {

        Item[] items;

        int bDamage = 0, bArmor = 0, bInt = 0, bAgli = 0, bStr = 0;

        public Inventory()
        {
            items = new Item[6];
            init();
        }

        private void init()
        {
            for (int i = 0; i < items.Length; i++)
                items[i] = new Item();
        }


        public void AddItem(Item item)
        {
            switch (item.ItemType)
            {
                case ItemType.Potion:
                    AddPotion((Potion)item);
                    break;
                case ItemType.Armor:

                    break;
                case ItemType.Weapon:
                    AddWeapon((Weapon)item);
                    break;
                default:
                    break;
            }
        }

        private void AddPotion(Potion p)
        {
            if (p.GetPotionType == PotionType.HealingPotion || p.PotionType == PotionType.GreaterHealingPotion)
            {
                if (items[0].GetItemType == ItemType.NONE)
                    items[0] = p;
                else if (((Potion)items[0]).PotionType == p.PotionType && ((Potion)items[0]).CanGetMore())
                    ((Potion)items[0]).Quantity++;
                else if (((Potion)items[0]).PotionType == p.PotionType && !((Potion)items[0]).CanGetMore())
                { }
                else
                    items[0] = p;

            }
            else
            {
                if (items[1].GetItemType == ItemType.NONE)
                    items[1] = p;
                else if (((Potion)items[1]).PotionType == p.PotionType && ((Potion)items[1]).CanGetMore())
                    ((Potion)items[1]).Quantity++;
                else if (((Potion)items[1]).PotionType == p.PotionType && !((Potion)items[1]).CanGetMore())
                { }
                else
                    items[1] = p;
            }
        }
        private void AddWeapon(Weapon w)
        {
            if(w.WeaponType == WeaponType.Cleave || w.WeaponType == WeaponType.MultiShot)
            {
                if (items[2].GetItemType == ItemType.NONE)
                    items[2] = w;
                IncreaseBunosStats(w);
                
            }
            else
            {
                items[3] = w;
                IncreaseBunosStats(w);
            }

        }


        public int UseHealingPotion()
        {
            if (items[0].ItemType != ItemType.NONE && items[0].Quantity > 0)
            {
                items[0].Quantity--;
                int healing = ((Potion)items[0]).Healing;
                if (items[0].Quantity == 0)
                    items[0] = new Item();
                return healing;
            }
            else
                return 0;
        }
        public int UseManaPotion()
        {
            if (items[1].ItemType != ItemType.NONE && items[1].Quantity > 0)
            {
                items[1].Quantity--;
                int mana = ((Potion)items[1]).ManaRestoring;

                if (items[1].Quantity == 0)
                    items[1] = new Item();

                return mana;
            }
            else
                return 0;
        }

        public Item[] GetInventory
        {
            get { return items; }
        }

        private void IncreaseBunosStats(Armor a)
        {
            bArmor += a.GetItemArmor;
            bAgli += a.GetItemAgility;
            bStr += a.GetItemStrength;
            bInt += a.GetItemInteligence;
        }

        private void IncreaseBunosStats(Weapon w)
        {
            bDamage += w.GetItemDamage;
            bAgli += w.GetItemAgility;
            bStr += w.GetItemStrength;
            bInt += w.GetItemInteligence;
        }

        private void DecreaseBunosStats(Armor a)
        {
            bArmor -= a.GetItemArmor;
            bAgli -= a.GetItemAgility;
            bStr -= a.GetItemStrength;
            bInt -= a.GetItemInteligence;
        }

        private void DecreaseBunosStats(Weapon w)
        {
            bDamage -= w.GetItemDamage;
            bAgli -= w.GetItemAgility;
            bStr -= w.GetItemStrength;
            bInt -= w.GetItemInteligence;
        }

        public int GetArmor()
        {
            return bArmor;
        }
        public int GetDamage()
        {
            return bDamage;
        }

        public int GetStrenght()
        {
            return bStr;
        }
        public int GetAgility()
        {
            return bAgli;
        }
        public int GetInteligence()
        {
            return bInt;
        }

    }
}
/*
 *   const int STRENGTH = 0;
        const int INTELIGENCE = 0;
        const int AGILITY = 0;
        const int DAMAGE = 0;
*/