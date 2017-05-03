using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FEntity.Players;
using Microsoft.Xna.Framework;
using HeroSiege.Tools;
using HeroSiege.Manager;
using Microsoft.Xna.Framework.Input;
using HeroSiege.FGameObject.Items.Potions;
using HeroSiege.FTexture2D;
using HeroSiege.FGameObject.Items;
using HeroSiege.FGameObject.Items.Weapons;
using HeroSiege.InterFace.UIs;
using HeroSiege.FGameObject.Items.Armors;

namespace HeroSiege.InterFace.GUI
{
    enum ShopIndex
    {
        w0,
        w1,
        w2,
        w3,
        w4,
        w5,
        w6,
        w7,
        w8,
        w9,
        w10,
        w11
    }

    class ShopWindow : UI
    {
        Viewport viewPort;
        Hero player;
        Vector2 basePos;
        PlayerIndex playerIndex;
        Point windowSize;
        Item selectedItem;
        TextureRegion selectTexture, healtText, manaText, revieText, CleaveText, multiShotText;
        Vector2[] drawOffsets;

        ShopIndex sIndex;
        int index;

        public ShopWindow(GameSettings setting, Viewport viewPort, Hero player, PlayerIndex index)
        {
            this.viewPort = viewPort;
            this.player = player;
            this.playerIndex = index;
            this.sIndex = ShopIndex.w0;
            this.index = 0;

            windowSize = new Point(ResourceManager.GetTexture("ShopWindow").region.Width, ResourceManager.GetTexture("ShopWindow").region.Height);
            InitTexture();
            InitDrawOffset();

            if (setting.GameMode == GameMode.Multiplayer && index == PlayerIndex.Two)
                basePos = new Vector2(viewPort.Width + viewPort.Width - windowSize.X, viewPort.Height / 2 - windowSize.Y / 2);
            else
                basePos = new Vector2(viewPort.Width - windowSize.X, viewPort.Height / 2 - windowSize.Y /2);
        }
        private void InitTexture()
        {
            selectTexture = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 0, 0, 78, 78);
            healtText =     new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);
            manaText =      new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78 * 2, 0, 78, 78);
            revieText =     new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78 * 3, 0, 78, 78);

            CleaveText =    new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 0, 78, 78, 78);
            multiShotText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78 * 3, 78, 78, 78);

            //healtText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);
            //healtText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);
            //healtText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);
            //healtText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);
            //healtText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);
            //healtText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);
            //healtText = new TextureRegion(ResourceManager.GetTexture("ItemIcons"), 78, 0, 78, 78);

        }
        private void InitDrawOffset()
        {
            float width = selectTexture.region.Width + 4;
            float height = selectTexture.region.Height + 4;
            drawOffsets = new Vector2[12];

            drawOffsets[0]  = new Vector2(0, 0);
            drawOffsets[1]  = new Vector2(width, 0);
            drawOffsets[2]  = new Vector2(width * 2, 0);
            drawOffsets[3]  = new Vector2(width * 3, 0);

            drawOffsets[4]  = new Vector2(0, height);
            drawOffsets[5]  = new Vector2(width, height);
            drawOffsets[6]  = new Vector2(width * 2, height);
            drawOffsets[7]  = new Vector2(width * 3, height);

            drawOffsets[8]  = new Vector2(0, height * 2);
            drawOffsets[9]  = new Vector2(width, height * 2);
            drawOffsets[10] = new Vector2(width * 2, height * 2);
            drawOffsets[11] = new Vector2(width * 3, height * 2);
        }

        //----- Updates -----//
        public override void Update(float delta)
        {
            if (player.isBuying)
                UpdateShopWindow();
            else
            {
                index = 0;
                sIndex = ShopIndex.w0;
            }
        }
        private void UpdateShopWindow()
        {
            UpdateJoystick();

            
            switch (sIndex)
            {
                case ShopIndex.w0:

                    selectedItem = new HealingPotion(healtText);

                    if (ButtonPress(PlayerInput.A))
                    {
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w1:
                    selectedItem = new ManaPotion(manaText);
                    if (ButtonPress(PlayerInput.A))
                    {
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w2:
                    selectedItem = new Reincarnation(revieText);
                    if (ButtonPress(PlayerInput.A))
                    {
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w3:
                    selectedItem = new Cleave(CleaveText);
                    if (ButtonPress(PlayerInput.A))
                    {
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w4:
                    selectedItem = new MultiShot(multiShotText);
                    if (ButtonPress(PlayerInput.A))
                    {                     
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w5:
                    selectedItem = new Item();
                    break;
                case ShopIndex.w6:
                    selectedItem = new Item();
                    break;
                case ShopIndex.w7:
                    selectedItem = new Item();
                    break;
                case ShopIndex.w8:
                    selectedItem = new Item();
                    break;
                case ShopIndex.w9:
                    selectedItem = new Item();
                    break;
                case ShopIndex.w10:
                    selectedItem = new Item();
                    break;
                case ShopIndex.w11:
                    selectedItem = new Item();
                    break;
                default:
                    break;
            }
        }
        private void UpdateSelected(int i)
        {
            index += i;

            if (index > 11)
                index -= 12;
            else if (index < 0)
                index += 12;

            sIndex = (ShopIndex)index;     
        }
        private void UpdateJoystick()
        {
            if (ButtonPress(PlayerInput.Right))
                UpdateSelected(1);
            else if (ButtonPress(PlayerInput.Left))
                UpdateSelected(-1);
            if (ButtonPress(PlayerInput.Up))
                UpdateSelected(-4);
            if (ButtonPress(PlayerInput.Down))
                UpdateSelected(4);
        }

        //----- Draws -----//
        public override void Draw(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("ShopWindow"), basePos + new Vector2(0,-50), null, Color.White, 0, new Vector2(windowSize.X / 2, 0), 2, SpriteEffects.None, 0);
            DrawItemIcon(SB);
            DrawSelectedWindow(SB);
            DrawItemInfo(SB);
        }
        private void DrawItemIcon(SpriteBatch SB)
        {
            Vector2 offset = new Vector2(-135, 20);
            SB.Draw(healtText, basePos + offset + drawOffsets[0], healtText, Color.White);
            SB.Draw(manaText, basePos + offset + drawOffsets[1], manaText, Color.White);
            SB.Draw(revieText, basePos + offset + drawOffsets[2], revieText, Color.White);
            SB.Draw(CleaveText, basePos + offset + drawOffsets[3], CleaveText, Color.White);
            SB.Draw(multiShotText, basePos + offset + drawOffsets[4], multiShotText, Color.White);

        }
        private void DrawSelectedWindow(SpriteBatch SB)
        {
            Vector2 offset = new Vector2(-135, 20);

            SB.Draw(selectTexture, basePos + offset + drawOffsets[index], selectTexture, Color.White);
        }
        private void DrawItemInfo(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("ItemInfoWindow"), basePos + new Vector2(0, -200), null, Color.White, 0, new Vector2(windowSize.X / 2, 0), new Vector2(1,1), SpriteEffects.None, 0);

            if (selectedItem == null || (selectedItem != null && selectedItem.ItemType == ItemType.NONE))
                return;
            if(selectedItem.ItemType == ItemType.Potion)
                DrawPotionInfo(SB, basePos + new Vector2(-100, -195), (Potion)selectedItem);
            else if (selectedItem.ItemType == ItemType.Weapon)
                DrawWeaponInfo(SB, basePos + new Vector2(-100, -195), (Weapon)selectedItem);
            else if (selectedItem.ItemType == ItemType.Armor)
                DrawArmorInfo(SB, basePos + new Vector2(-100, -195), (Armor)selectedItem);

            //
        }
        
        private void DrawPotionInfo(SpriteBatch SB, Vector2 pos, Potion p)
        {
            DrawString(SB, ResourceManager.GetFont("Arial_Font"),
                "Name: " +p.ItemName
                +"\nCost: "+p.GetItemCost+ " gold", pos, Color.White, 1);

            if(p.PotionType == PotionType.HealingPotion || p.PotionType == PotionType.GreaterHealingPotion)
                DrawString(SB, ResourceManager.GetFont("Arial_Font"), "\n\nHeals: " + p.Healing, pos, Color.White, 1);
            else if (p.PotionType == PotionType.ManaPotion || p.PotionType == PotionType.GreaterManaPotion)
                DrawString(SB, ResourceManager.GetFont("Arial_Font"), "\n\nMana restoring: " + p.ManaRestoring, pos, Color.White, 1);
            else if (p.PotionType == PotionType.RejuvenationPotion)
                DrawString(SB, ResourceManager.GetFont("Arial_Font"), "\n\nRevive on death" +
                                                                      "\nHeals: " + p.Healing +
                                                                      "\nMana restoring: " + p.ManaRestoring, pos, Color.White, 1);
        }
        private void DrawWeaponInfo(SpriteBatch SB, Vector2 pos, Weapon w)
        {
            DrawString(SB, ResourceManager.GetFont("Arial_Font"),
                "Name: " + w.ItemName
                + "\nCost: " + w.GetItemCost + " gold", pos, Color.White, 1);

            string extraInfo ="\n";
            if (w.WeaponType == WeaponType.Cleave)
                extraInfo += "\nFor Melee type";
            else if (w.WeaponType == WeaponType.MultiShot)
                extraInfo += "\nFor Range type";

            DrawString(SB, ResourceManager.GetFont("Arial_Font"), extraInfo +
                                                                  "\nDamage: " + w.GetItemDamage +
                                                                  "\nStrength: " + w.GetItemStrength +
                                                                  "\nAgility: " + w.GetItemAgility +
                                                                  "\nInteligence: " + w.GetItemInteligence,
                                                                  
                                                                  pos, Color.White, 1);
        }
        private void DrawArmorInfo(SpriteBatch SB, Vector2 pos, Armor a)
        {
            DrawString(SB, ResourceManager.GetFont("Arial_Font"),
                "Name: " + a.ItemName
                + "\nCost: " + a.GetItemCost + " gold", pos, Color.White, 1);

            DrawString(SB, ResourceManager.GetFont("Arial_Font"),"\n\nArmor: " + a.GetItemArmor +
                                                                  "\nStrength: " + a.GetItemStrength +
                                                                  "\nAgility: " + a.GetItemAgility +
                                                                  "\nInteligence: " + a.GetItemInteligence,
                                                                  pos, Color.White, 1);
        }

        private bool ButtonPress(PlayerInput b)
        {
            return InputHandler.GetButtonState(playerIndex, b) == InputState.Released;
        }
    }
}
