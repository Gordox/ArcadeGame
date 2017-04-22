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
            //Mana / Health / EXP
            DrawHealtManaStatus(SB);
            //Inventory
        }

        private void DrawHealtManaStatus(SpriteBatch SB)
        {
            //Health
            string text = player.Stats.Health + " / " + player.Stats.MaxHealth;
            Vector2 textposition = new Vector2(position.X - hudSize.X / 2 + 140, viewPort.Height - 40);
            Vector2 orgin = ResourceManager.GetFont("Arial_Font").MeasureString(text);
            SB.DrawString(ResourceManager.GetFont("Arial_Font"), text, textposition, Color.Green, 0, new Vector2(orgin.X / 2, orgin.Y / 2), 0.8f, SpriteEffects.None, 1);
            //Mana
            text = player.Stats.Mana + " / " + player.Stats.MaxMana;
            textposition = new Vector2(position.X - hudSize.X / 2 + 140, viewPort.Height - 10);
            orgin = ResourceManager.GetFont("Arial_Font").MeasureString(text);
            SB.DrawString(ResourceManager.GetFont("Arial_Font"), text, textposition, Color.Blue, 0, new Vector2(orgin.X / 2, orgin.Y / 2), 0.8f, SpriteEffects.None, 1);
        }

        private void DrawEXPBar(SpriteBatch SB) { }
    }
}
