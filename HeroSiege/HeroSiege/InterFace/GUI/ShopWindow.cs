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

    class ShopWindow : IUI
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
        public void Update(float delta)
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
                    if (ButtonPress(PlayerInput.A))
                    {
                        selectedItem = new HealingPotion(healtText);
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w1:
                    if (ButtonPress(PlayerInput.A))
                    {
                        selectedItem = new ManaPotion(manaText);
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w2:
                    break;
                case ShopIndex.w3:
                    if (ButtonPress(PlayerInput.A))
                    {
                        selectedItem = new Cleave(CleaveText);
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;
                        if (player.Inventory.GetInventory[2].GetItemType != ItemType.NONE)
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w4:
                    if (ButtonPress(PlayerInput.A))
                    {
                        selectedItem = new MultiShot(multiShotText);
                        if (!player.HaveEnoughGold(selectedItem.GetItemCost))
                            return;
                        if (player.Inventory.GetInventory[2].GetItemType != ItemType.NONE)
                            return;

                        player.Inventory.AddItem(selectedItem);
                        player.DecreaseGold(selectedItem.GetItemCost);
                    }
                    break;
                case ShopIndex.w5:
                    break;
                case ShopIndex.w6:
                    break;
                case ShopIndex.w7:
                    break;
                case ShopIndex.w8:
                    break;
                case ShopIndex.w9:
                    break;
                case ShopIndex.w10:
                    break;
                case ShopIndex.w11:
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
        public void Draw(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("ShopWindow"), basePos + new Vector2(0,-50), null, Color.White, 0, new Vector2(windowSize.X / 2, 0), 2, SpriteEffects.None, 0);
            DrawItemIcon(SB);
            DrawSelectedWindow(SB);
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

        

       
        private bool ButtonPress(PlayerInput b)
        {
            return InputHandler.GetButtonState(playerIndex, b) == InputState.Released;
        }
    }
}
