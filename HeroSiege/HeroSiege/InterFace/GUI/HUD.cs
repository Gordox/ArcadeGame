using HeroSiege.FEntity.Players;
using HeroSiege.Manager;
using HeroSiege.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.InterFace.GUI
{
    class HUD : IUI
    {
        GameSettings setting;
        Viewport viewPort;
        Hero player;
        Vector2 position;
        Texture2D heroPortrait;
        PlayerIndex index;
        Point hudSize;

        public HUD(GameSettings setting, Viewport viewPort, Hero player, PlayerIndex index)
        {
            this.setting = setting;
            this.viewPort = viewPort;
            this.player = player;
            this.index = index;
            hudSize = new Point(ResourceManager.GetTexture("HUDTexture").region.Width, ResourceManager.GetTexture("HUDTexture").region.Height);

            if(setting.GameMode == GameMode.Multiplayer && index == PlayerIndex.Two)
                position = new Vector2(viewPort.Width + viewPort.Width / 2, viewPort.Height - hudSize.Y);
            else
                position = new Vector2(viewPort.Width / 2, viewPort.Height - hudSize.Y);

            switch (index)
            {
                case PlayerIndex.One:
                    InitPortrait(setting.playerOne);
                    break;
                case PlayerIndex.Two:
                    InitPortrait(setting.playerTwo);
                    break;
                default:
                    break;
            }
        }

        private void InitPortrait(CharacterType heroType)
        {
            switch (heroType)
            {
                case CharacterType.ElvenArcher:
                    break;
                case CharacterType.Mage:
                    break;
                case CharacterType.Gryphon_Rider:
                    break;
                case CharacterType.FootMan:
                    break;
                case CharacterType.Dwarven:
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    break;
                case CharacterType.Knight:
                    break;
                case CharacterType.None:
                    break;
                default:
                    break;
            }
        }

        public void Update(float delta)
        {
            
        }


        public void Draw(SpriteBatch SB)
        {
            //Potrait

            //HUD
            SB.Draw(ResourceManager.GetTexture("HUDTexture"), position, null, Color.White, 0, new Vector2(hudSize.X / 2, 0), 1, SpriteEffects.None, 0);
            // Middle Mana / Health / EXP / Name
            DrawHealtManaStatus(SB);
            DrawEXPBar(SB);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), "Gordox", position + new Vector2(0, 90), Color.White, 2);
            //Inventory



        }


        private void DrawHealtManaStatus(SpriteBatch SB)
        {
            //Health
            string text = player.Stats.Health + " / " + player.Stats.MaxHealth;
            Vector2 textposition = new Vector2(position.X - hudSize.X / 2 + 140, viewPort.Height - 38);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), text, textposition, Color.Green, 0.8f);

            //Mana
            text = player.Stats.Mana + " / " + player.Stats.MaxMana;
            textposition = new Vector2(position.X - hudSize.X / 2 + 140, viewPort.Height - 13);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), text, textposition, Color.Blue, 0.8f);


        }
        private void DrawEXPBar(SpriteBatch SB)
        {
            int offsetX = ResourceManager.GetTexture("XpBarLayer_1").region.Width / 2;
            int offsetY = ResourceManager.GetTexture("XpBarLayer_1").region.Height / 2;
            Vector2 xpBarPos = new Vector2(position.X - offsetX + 5 , viewPort.Height - 150);
            SB.Draw(ResourceManager.GetTexture("XpBarLayer_1"), xpBarPos, Color.White);
            SB.Draw(ResourceManager.GetTexture("XpBarLayer_2"), xpBarPos, GenerateBar(player.Stats.XP, player.Stats.MaxXP, ResourceManager.GetTexture("XpBarLayer_2").region), Color.Purple);
            string text = "Level: " + player.Stats.Level;

            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), text, xpBarPos + new Vector2(offsetX, offsetY), Color.White);
        }


        public Rectangle GenerateBar(float Current, float Max, int width, int height)
        {
            float Percent = Current / Max;
            return new Rectangle(0, 0, (int)(Percent * width), height);
        }
        public Rectangle GenerateBar(float Current, float Max, Rectangle region)
        {
            float Percent = Current / Max;
            return new Rectangle(0, 0, (int)(Percent * region.Width), region.Height);
        }

        private void DrawCenterString(SpriteBatch SB, SpriteFont font, string text, Vector2 pos, Color color)
        {
            Vector2 orgin = font.MeasureString(text);
            SB.DrawString(ResourceManager.GetFont("Arial_Font"), text, pos, color, 0, new Vector2(orgin.X / 2, orgin.Y / 2), 1, SpriteEffects.None, 1);
        }
        private void DrawCenterString(SpriteBatch SB, SpriteFont font, string text, Vector2 pos, Color color, float size)
        {
            Vector2 orgin = font.MeasureString(text);
            SB.DrawString(ResourceManager.GetFont("Arial_Font"), text, pos, color, 0, new Vector2(orgin.X / 2, orgin.Y / 2), size, SpriteEffects.None, 1);
        }
    }
}
