using HeroSiege.FEntity.Players;
using HeroSiege.FGameObject.Items;
using HeroSiege.FTexture2D;
using HeroSiege.InterFace.UIs;
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
    class HUD : UI
    {
        GameSettings setting;
        Viewport viewPort;
        Hero player;
        Vector2 position;
        TextureRegion heroPortrait, mainStatsIcon, attackIcon, armorIcon;
        PlayerIndex index;
        Point hudSize;

        public HUD(GameSettings setting, Viewport viewPort, Hero player, PlayerIndex index)
        {
            this.setting = setting;
            this.viewPort = viewPort;
            this.player = player;
            this.index = index;
            hudSize = new Point(ResourceManager.GetTexture("HUDTexture").region.Width, ResourceManager.GetTexture("HUDTexture").region.Height);

            attackIcon = new TextureRegion(ResourceManager.GetTexture("StatsIcons"), 0, 0, 112, 111);
            armorIcon = new TextureRegion(ResourceManager.GetTexture("StatsIcons"), 224, 0, 112, 111);

            if (setting.GameMode == GameMode.Multiplayer && index == PlayerIndex.Two)
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
                    //StatsIcons
                    heroPortrait = ResourceManager.GetTexture("ArcherPortraits");
                    mainStatsIcon = new TextureRegion(ResourceManager.GetTexture("StatsIcons"), 112, 111, 112, 111); //Agi
                    break;
                case CharacterType.Mage:
                    heroPortrait = ResourceManager.GetTexture("MagePortraits");
                    mainStatsIcon = new TextureRegion(ResourceManager.GetTexture("StatsIcons"), 0, 111, 112, 111); //int
                    break;
                case CharacterType.Gryphon_Rider:

                    mainStatsIcon = new TextureRegion(ResourceManager.GetTexture("StatsIcons"), 0, 111, 112, 111); //int
                    break;
                case CharacterType.FootMan:
                    heroPortrait = ResourceManager.GetTexture("SoldierPortraits");
                    mainStatsIcon = new TextureRegion(ResourceManager.GetTexture("StatsIcons"), 224, 111, 112, 111); //str
                    break;
                case CharacterType.Dwarven:
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    break;
                case CharacterType.Knight:
                    heroPortrait = ResourceManager.GetTexture("KnightPortraits");
                    mainStatsIcon = new TextureRegion(ResourceManager.GetTexture("StatsIcons"), 224, 111, 112, 111); //str
                    break;
                case CharacterType.None:
                    break;
                default:
                    break;
            }
        }

        public override void Update(float delta)
        {
            
        }


        public override void Draw(SpriteBatch SB)
        {
            //Potrait
            SB.Draw(heroPortrait, new Vector2(position.X - 355, position.Y + 50), heroPortrait, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
            //HUD
            SB.Draw(ResourceManager.GetTexture("HUDTexture"), position, null, Color.White, 0, new Vector2(hudSize.X / 2, 0), 1, SpriteEffects.None, 0);
            // Middle Mana / Health / EXP / Name / stats
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), player.HeroName, position + new Vector2(0, 80), Color.White, 1.5f);
            DrawHealtManaStatus(SB);
            int offsetX = ResourceManager.GetTexture("XpBarLayer_1").region.Width / 2;
            int offsetY = ResourceManager.GetTexture("XpBarLayer_1").region.Height / 2;
            Vector2 xpBarPos = new Vector2(position.X - offsetX - 20, viewPort.Height - 170);
            DrawEXPBar(SB, xpBarPos, new Vector2(offsetX, offsetY));
                //Attack
            DrawStatsInfo(SB, attackIcon, ResourceManager.GetFont("Arial_Font"), position + new Vector2(-offsetX - 20, 140), "Damage:", player.Stats.Damage, player.GetDmgOnStats());
                //Armor
            DrawStatsInfo(SB, armorIcon, ResourceManager.GetFont("Arial_Font"), position + new Vector2(-offsetX - 20, 150 + armorIcon.region.Height/2), "Armor:", player.GetArmor());
                //Stats
            DrawStatsInfo(SB, mainStatsIcon, ResourceManager.GetFont("Arial_Font"), position + new Vector2(-offsetX + 140, 130 + armorIcon.region.Height / 3), "Strength:", player.GetStrenght().ToString(), "Agility:", player.GetAgility().ToString(), "Intelligence:", player.GetInteligence().ToString());
            //Inventory
            DrawInventory(SB, position);
        }


        private void DrawHealtManaStatus(SpriteBatch SB)
        {
            //Health
            string text = (int)player.Stats.Health + " / " + player.Stats.MaxHealth;
            Vector2 textposition = new Vector2(position.X - hudSize.X / 2 + 140, viewPort.Height - 38);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), text, textposition, Color.Green, 0.8f);

            //Mana
            text = (int)player.Stats.Mana + " / " + player.Stats.MaxMana;
            textposition = new Vector2(position.X - hudSize.X / 2 + 140, viewPort.Height - 13);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), text, textposition, Color.Blue, 0.8f);


        }
        private void DrawEXPBar(SpriteBatch SB, Vector2 pos, Vector2 offset)
        {
            Rectangle rec = ResourceManager.GetTexture("XpBarLayer_1").region;
            SB.Draw(ResourceManager.GetTexture("XpBarLayer_1"), pos, rec, Color.White, 0, new Vector2(0, 0), new Vector2(1.2f,1), SpriteEffects.None, 0);
            SB.Draw(ResourceManager.GetTexture("XpBarLayer_2"), pos, GenerateBar(player.Stats.XP, player.Stats.MaxXP, ResourceManager.GetTexture("XpBarLayer_2").region), Color.Purple, 0, new Vector2(0, 0), new Vector2(1.2f, 1), SpriteEffects.None, 0);
            string text = "Level: " + player.Stats.Level;
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), text, pos + offset, Color.White);
        }

        private void DrawStatsInfo(SpriteBatch SB, TextureRegion texturRegion, SpriteFont font, Vector2 pos, string statsName, int statsNr, int extra = 0)
        {
            SB.Draw(texturRegion, new Vector2(pos.X, pos.Y), texturRegion, Color.White, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);

            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), statsName, pos + new Vector2(100, 10), Color.Gold, 1);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), ""+statsNr, pos + new Vector2(extra == 0 ? 100 : 80, 40), Color.White, 1);
            if (extra > 0)
            {
                float leanght = ResourceManager.GetFont("Arial_Font").MeasureString(statsNr.ToString()).X;
                DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), " + " + extra, pos + new Vector2(90 + leanght, 40), Color.Green, 1);
            }

        }
        private void DrawStatsInfo(SpriteBatch SB, TextureRegion texturRegion, SpriteFont font, Vector2 pos, string statsName_1, string statsNr_1, string statsName_2, string statsNr_2, string statsName_3, string statsNr_3)
        {
            int width = texturRegion.region.Width / 2;
            int height = texturRegion.region.Height / 2;

            SB.Draw(texturRegion, new Vector2(pos.X, pos.Y), texturRegion, Color.White, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);

            DrawString(SB, ResourceManager.GetFont("Arial_Font"), statsName_1, pos + new Vector2(width + 10, -25), Color.Gold, .8f);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), statsNr_1,   pos + new Vector2(100, 5), Color.White, .8f);

            DrawString(SB, ResourceManager.GetFont("Arial_Font"), statsName_2, pos + new Vector2(width + 10, texturRegion.region.Height/4 -10), Color.Gold, .8f);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), statsNr_2,   pos + new Vector2(100, 50), Color.White, .8f);

            DrawString(SB, ResourceManager.GetFont("Arial_Font"), statsName_3, pos + new Vector2(width + 10, 60), Color.Gold, .8f);
            DrawCenterString(SB, ResourceManager.GetFont("Arial_Font"), statsNr_3,   pos + new Vector2(100, 90), Color.White, .8f);
        }

        private void DrawInventory(SpriteBatch SB, Vector2 basePos)
        {

            Item[] temp = player.Inventory.GetInventory;

            if (temp[0].GetItemType != ItemType.NONE)
            {
                SB.Draw(temp[0].GetTexture, basePos + new Vector2(220, 70), temp[0].GetTexture.region, Color.White, 0, new Vector2(0, 0), .7f, SpriteEffects.None, 0);
                DrawString(SB, ResourceManager.GetFont("Arial_Font"), temp[0].Quantity.ToString(), basePos + new Vector2(225, 72), Color.White, .8f);
            }

            if (temp[1].GetItemType != ItemType.NONE)
            { 
                SB.Draw(temp[1].GetTexture, basePos + new Vector2(290, 70), temp[1].GetTexture.region, Color.White, 0, new Vector2(0, 0), .7f, SpriteEffects.None, 0);
                DrawString(SB, ResourceManager.GetFont("Arial_Font"), temp[1].Quantity.ToString(), basePos + new Vector2(295, 72), Color.White, .8f);
            }

            if (temp[2].GetItemType != ItemType.NONE)
                SB.Draw(temp[2].GetTexture, basePos + new Vector2(220, 70 * 2), temp[2].GetTexture.region, Color.White, 0, new Vector2(0, 0), .7f, SpriteEffects.None, 0);

            if (temp[3].GetItemType != ItemType.NONE)
                SB.Draw(temp[3].GetTexture, basePos + new Vector2(290, 70 * 2), temp[3].GetTexture.region, Color.White, 0, new Vector2(0, 0), .7f, SpriteEffects.None, 0);

            if (temp[4].GetItemType != ItemType.NONE)
                SB.Draw(temp[4].GetTexture, basePos + new Vector2(220, 70 * 3), temp[4].GetTexture.region, Color.White, 0, new Vector2(0, 0), .7f, SpriteEffects.None, 0);

            if (temp[5].GetItemType != ItemType.NONE)
                SB.Draw(temp[5].GetTexture, basePos + new Vector2(290, 70 * 3), temp[5].GetTexture.region, Color.White, 0, new Vector2(0, 0), .7f, SpriteEffects.None, 0);

        }

       

        
    }
}
