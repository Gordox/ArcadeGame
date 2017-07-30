using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.InterFace.UIs.Buttons;
using Microsoft.Xna.Framework;
using HeroSiege.Manager;
using Microsoft.Xna.Framework.Input;

namespace HeroSiege.InterFace.UIs.Menus
{
    enum Sections
    {
        Guidelines,
        Controlls,
        Heros,
        Enemies,
        Items,
    }
    enum GuidLineStages
    {
        Stage_1,
        Stage_2,
        Stage_3,
        Attributes,
        Other
    }
    enum Heros
    {
        ElvenArcher,
        Mage,
        Gryphon_Rider,
        FootMan,
        Dwarven,
        Gnomish_Flying_Machine,
        Knight,
    }
    enum Enemies
    {
        Goblin,
        Grunt,
        Orge,
        Skeleton,
        Troll,
        DeathKnight,
        Demon,
        Dragon
    }

    class HowToPlayMenu : Menu
    {
        //----- Feilds -----//
        private Sections currentSection, oldSection;

        private BigButton bnHeros, bnEnemies, bnItems, bnControlls, bnGuidlines;

        private int offset;
        string intro;
        Vector2 infoPanelSize;
        private Vector2 centerInfoPanel;
        private int stageIndex, oldStageIndex, hIndex, oldHIndex, eIndex, oldEIndex;
        GuidLineStages currStage, oldStage;
        Heros currHero, oldHero;
        Enemies currEnemy, oldEnemy;

        public HowToPlayMenu(Viewport viewPort)
            : base(viewPort)
        {
            Init();
        }

        private void Init()
        {
            offset = ResourceManager.GetTexture("StartMenu").region.Width / 2;
            intro = "Hello hero! This will be a quick guid to how to play";

            centerInfoPanel = centerViewPort + new Vector2(250, 0);
            infoPanelSize = new Vector2(ResourceManager.GetTexture("ItemInfoWindow").region.Width * 2.5f,
                                        ResourceManager.GetTexture("ItemInfoWindow").region.Height * 2.5f);

            bnGuidlines = new BigButton(centerViewPort + new Vector2(-viewPort.Width / 2 + offset + 10, -320), "Guidlines");
            bnControlls = new BigButton(centerViewPort + new Vector2(-viewPort.Width / 2 + offset + 10, -220), "Controlls");
            bnHeros =     new BigButton(centerViewPort + new Vector2(-viewPort.Width / 2 + offset + 10, -120), "Heros");
            bnEnemies =   new BigButton(centerViewPort + new Vector2(-viewPort.Width / 2 + offset + 10,  -20), "Enemies");
            bnItems =     new BigButton(centerViewPort + new Vector2(-viewPort.Width / 2 + offset + 10,   80), "Items");

            bnGuidlines.Size = bnControlls.Size = bnHeros.Size = bnEnemies.Size = bnItems.Size = 2.7f;
            bnGuidlines.Selected = true;
            bnGuidlines.ButtonState = bnControlls.ButtonState = bnHeros.ButtonState =
                bnEnemies.ButtonState = bnItems.ButtonState = ButtonState.Active;
        }
        //----- Updates -----//
        public override void Update(float delta)
        {
            UpdateJoystick();
            if (currentSection != oldSection)
                UpdateSelectedMark();
            UpdateSelected();
        }

        private void UpdateSelectedMark()
        {
            bnGuidlines.Selected = bnControlls.Selected = bnHeros.Selected = bnEnemies.Selected = bnItems.Selected = false;
            switch (currentSection)
            {
                case Sections.Guidelines:
                    bnGuidlines.Selected = true;
                    break;
                case Sections.Controlls:
                    bnControlls.Selected = true;
                    break;
                case Sections.Heros:
                    bnHeros.Selected = true;
                    break;
                case Sections.Enemies:
                    bnEnemies.Selected = true;
                    break;
                case Sections.Items:
                    bnItems.Selected = true;
                    break;
                default:
                    break;
            }
            oldSection = currentSection;
        }
        protected override void UpdateSelected()
        {
            bnGuidlines.ButtonState = bnControlls.ButtonState = bnHeros.ButtonState =
                bnEnemies.ButtonState = bnItems.ButtonState = ButtonState.Active;
            switch (currentSection)
            {
                case Sections.Guidelines:
                    if (ButtonPress(PlayerIndex.One, PlayerInput.Left) || ButtonPress(PlayerIndex.Two, PlayerInput.Left))
                        stageIndex = UpdateIndex(-1, stageIndex, 5);
                    else if (ButtonPress(PlayerIndex.One, PlayerInput.Right) || ButtonPress(PlayerIndex.Two, PlayerInput.Right))
                        stageIndex = UpdateIndex(1, stageIndex, 5);

                    if(oldStageIndex != stageIndex)
                    {
                        currStage = (GuidLineStages)stageIndex;
                        oldStageIndex = stageIndex;
                    }

                    break;
                case Sections.Controlls:
                   

                    break;
                case Sections.Heros:
                   

                    break;
                case Sections.Enemies:
                    

                    break;
                case Sections.Items:
                   
                    break;
                default:
                    break;
            }
            oldSection = currentSection;
        }
        protected override void UpdateSelectIndex(int i)
        {
            currentIndex += i;

            if (currentIndex > 4)
                currentIndex -= 5;
            else if (currentIndex < 0)
                currentIndex += 5;

            currentSection = (Sections)currentIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">The increace value</param>
        /// <param name="curIndex">The current index value</param>
        /// <param name="nr">nr of objects the index can loop from 0-n</param>
        /// <returns>The new index value</returns>
        private int UpdateIndex(int i, int curIndex, int nr)
        {
            curIndex += i;

            if (curIndex > nr - 1)
                curIndex -= nr;
            else if (curIndex < 0)
                curIndex += nr;

            return curIndex;
        }

        //----- Draws -----//
        public override void Draw(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("BG_1"), Vector2.Zero, Color.White);
            DrawButtonMenu(SB);
            DrawInfoPanel(SB);
            DrawTheSections(SB);

        }

        private void DrawButtonMenu(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("StartMenu"), centerViewPort + new Vector2(-viewPort.Width/2 + 10, -centerViewPort.Y - 20), Color.White);
            bnGuidlines.DrawCenter(SB);
            bnControlls.DrawCenter(SB);
            bnHeros.DrawCenter(SB);
            bnEnemies.DrawCenter(SB);
            bnItems.DrawCenter(SB);
        }
        private void DrawInfoPanel(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("ItemInfoWindow"), centerInfoPanel, null, Color.White, 0,
               new Vector2(ResourceManager.GetTexture("ItemInfoWindow").region.Width / 2, ResourceManager.GetTexture("ItemInfoWindow").region.Height / 2), 5f, SpriteEffects.None, 0);
        }
        private void DrawTheSections(SpriteBatch SB)
        {
            switch (currentSection)
            {
                case Sections.Guidelines:
                    DrawGuidLine(SB);
                    break;
                case Sections.Controlls:
                    DrawControls(SB);
                    break;
                case Sections.Heros:


                    break;
                case Sections.Enemies:


                    break;
                case Sections.Items:

                    break;
                default:
                    break;
            }
        }

        private void DrawGuidLine(SpriteBatch SB)
        {
            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), intro,
                centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 45), Color.Gold, 1);
            switch (currStage)
            {
                case GuidLineStages.Stage_1:
                    DrawGuidSTAGE_1(SB);
                    break;
                case GuidLineStages.Stage_2:
                    DrawGuidSTAGE_2(SB);
                    break;
                case GuidLineStages.Stage_3:
                    DrawGuidSTAGE_3(SB);
                    break;
                case GuidLineStages.Attributes:
                    DrawGuidAttribute(SB);
                    break;
                case GuidLineStages.Other:
                    DrawGuidOther(SB);
                    break;
                default:
                    break;
            }
        }

        //Guid draw
        private void DrawGuidSTAGE_1(SpriteBatch SB)
        {
            //Text
            
            string Info_1 = "First try to level up by kill the Enemies that are spawning and gatter exp and gold.\n"+
                "with the gold, buy yourself some items deal more damage and take less.";
            string Info_2 = "Your first goal is to destroy the towers that are defending the spwaning altars.\n" +
                "Then destroy the the altar, but be carefull it is the home for the Death Knight!";

            string Info_3 = "While doing all that, you the hero needs to defend your own towers and castle.\n" +
                "If your castle is destroyed you will LOSE!";

            DrawString(SB, ResourceManager.GetFont("WarFont_32"), "Stage 1:",
                centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 150), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_1,
                centerInfoPanel + new Vector2(0, -40), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_2,
              centerInfoPanel + new Vector2(0, 50), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_3,
               centerInfoPanel + new Vector2(0, 140), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 1 / 5",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);


            //IMG
            SB.Draw(ResourceManager.GetTexture("ETower"), centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 220),
                new Rectangle(98,128,64,64), Color.White, 0, Vector2.Zero, 2,SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Spawn_Altar"), centerInfoPanel + new Vector2(infoPanelSize.X - 180, -infoPanelSize.Y + 220),
                new Rectangle(0, 129, 98, 98), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("HTower_2"), centerInfoPanel + new Vector2(infoPanelSize.X - 180, -infoPanelSize.Y + 720),
                new Rectangle(320, 0, 64, 64), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Castle_lvl_1"), centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 620),
                new Rectangle(0, 0, 124, 124), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);

        }
        private void DrawGuidSTAGE_2(SpriteBatch SB)
        {
            string Info_1 = "Now that all the towers and spawn altars is destroyed, it is time for the next step.\n" +
                "The first stage might was hard for you but now the real test begins!.";
            string Info_2 = "Take the portal that will take you the the next stage were you will find four demon.\n" +
                "These gardians are resting and keeps the fire wall upp, keeping you from the dragon!";
            string Info_3 = "Destroy them and show your mighty strenght and bring down the fire wall!";

            // Text
            DrawString(SB, ResourceManager.GetFont("WarFont_32"), "Stage 2:",
               centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 150), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_1,
               centerInfoPanel + new Vector2(0, -40), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_2,
              centerInfoPanel + new Vector2(0, 50), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_3,
               centerInfoPanel + new Vector2(0, 140), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 2 / 5",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            //IMG
            SB.Draw(ResourceManager.GetTexture("Portal"), centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 220),
                new Rectangle(0, 0, 64, 64), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Portal"), centerInfoPanel + new Vector2(infoPanelSize.X - 150, -infoPanelSize.Y + 220),
                new Rectangle(0, 0, 64, 64), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Demon"), centerInfoPanel + new Vector2(infoPanelSize.X - 190, -infoPanelSize.Y + 650),
                new Rectangle(254, 0, 64, 64), Color.White, 0, Vector2.Zero, 3f, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Burning"), centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 660),
                new Rectangle(0, 128, 32, 32), Color.White, 0, Vector2.Zero, 4f, SpriteEffects.None, 0);
        }
        private void DrawGuidSTAGE_3(SpriteBatch SB)
        {
            string Info_1 = "Its time for the final fight! Do not falter the end is near.\n" +
                "Now that all the demons guardians are defeted and the fire wall is down.";
            string Info_2 = "Walk north and take a look at the beast it self.\n" +
                "With all the experiance you have now gattered during your heroic trip to get here!";
            string Info_3 = "The dragon will be your final quest for victory or defeat!";

            DrawString(SB, ResourceManager.GetFont("WarFont_32"), "Stage 3:",
               centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 150), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_1,
              centerInfoPanel + new Vector2(0, -40), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_2,
              centerInfoPanel + new Vector2(0, 50), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_3,
               centerInfoPanel + new Vector2(0, 140), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 3 / 5",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            //IMG
            SB.Draw(ResourceManager.GetTexture("Dragon"), centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 220),
                new Rectangle(384, 384, 96, 96), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Dragon"), centerInfoPanel + new Vector2(infoPanelSize.X - 200, -infoPanelSize.Y + 220),
                new Rectangle(384, 384, 96, 96), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Dragon"), centerInfoPanel + new Vector2(infoPanelSize.X - 200, -infoPanelSize.Y + 650),
                new Rectangle(384, 384, 96, 96), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Dragon"), centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 660),
                new Rectangle(384, 384, 96, 96), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
        }
        private void DrawGuidAttribute(SpriteBatch SB)
        {
            string Info_1 = "Your hero has a main attribute that you whan to focus on\n" +
                "The three different attributs are inteligens, strength and agility";

            string Info_2 = "Inteligence";
            string Info_3 = "Strenght";
            string Info_4 = "Agility";
            string Info_5 = "When you increse your hero with the right attribute\n by items, " +
                "you will feel more powerful and\n do extra damage to the enemy";

            // Text
            DrawString(SB, ResourceManager.GetFont("WarFont_32"), "Attribute:",
               centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 150), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_1,
               centerInfoPanel + new Vector2(0, -200), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_2,
              centerInfoPanel + new Vector2(-300, -40), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_3,
               centerInfoPanel + new Vector2(-300, 110), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_4,
              centerInfoPanel + new Vector2(-300, 260), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_5,
              centerInfoPanel + new Vector2(220, 110), Color.Gold, 1);


            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 4 / 5",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            //IMG
            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 150, -100),
                new Rectangle(0, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 150, 50),
                new Rectangle(224, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 150, 200),
                new Rectangle(112, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);


        }
        private void DrawGuidOther(SpriteBatch SB)
        {
            string Info_1 = "So where can you buy the items to increse your attributes?\n" +
                "At the shop of course!";
            string Info_2 = "There are four shop in the world, one in each direction where the\n" +
                "enemies are comming from so be carefull!";
            string Info_3 = "Side not, he who decided to place a shop\n were enemies comes from most be stupid!";
            // Text
            DrawString(SB, ResourceManager.GetFont("WarFont_32"), "Other:",
               centerInfoPanel + new Vector2(-infoPanelSize.X + 20, -infoPanelSize.Y + 150), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_1,
               centerInfoPanel + new Vector2(0, -200), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_2,
              centerInfoPanel + new Vector2(0, -120), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), Info_3,
               centerInfoPanel + new Vector2(400, -60), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 5 / 5",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            //IMG
            SB.Draw(ResourceManager.GetTexture("Shop_1"), centerInfoPanel + new Vector2(-192,50),
                new Rectangle(0, 256, 95, 98), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);


        }
        //Control draw
        private void DrawControls(SpriteBatch SB)
        {
            string Info_1 = "Controll scheme";

            string Info_2 = "Its time to memories all the buttons";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), Info_1,
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 150), Color.Gold, 2);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_22"), Info_2,
               centerInfoPanel + new Vector2(0, -200), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Controlls"), centerInfoPanel + new Vector2(-576, -150),
                null, Color.White, 0, Vector2.Zero, .9f, SpriteEffects.None, 0);
        }
        //Heros draw
        private void DrawHeros(SpriteBatch SB)
        {
            switch (currHero)
            {
                case Heros.ElvenArcher:
                    break;
                case Heros.Mage:
                    break;
                case Heros.Gryphon_Rider:
                    break;
                case Heros.FootMan:
                    break;
                case Heros.Dwarven:
                    break;
                case Heros.Gnomish_Flying_Machine:
                    break;
                case Heros.Knight:
                    break;
                default:
                    break;
            }
        }
        //Enemies draw
        private void DrawEnemies(SpriteBatch SB)
        {
            switch (currEnemy)
            {
                case Enemies.Goblin:
                    break;
                case Enemies.Grunt:
                    break;
                case Enemies.Orge:
                    break;
                case Enemies.Skeleton:
                    break;
                case Enemies.Troll:
                    break;
                case Enemies.DeathKnight:
                    break;
                case Enemies.Demon:
                    break;
                case Enemies.Dragon:
                    break;
                default:
                    break;
            }
        }
        //Items draw

        //----- Other -----//

    }
}
