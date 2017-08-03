using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.InterFace.UIs.Buttons;
using Microsoft.Xna.Framework;
using HeroSiege.Manager;
using Microsoft.Xna.Framework.Input;
using HeroSiege.FGameObject.Items;
using HeroSiege.FGameObject.Items.Armors;
using HeroSiege.FGameObject.Items.Weapons;
using HeroSiege.FGameObject.Items.Potions;

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
    enum Items
    {
        StartInfo,
        HealingPotion,
        ManaPotion,
        Recarnation,

        Cleave,
        Multishoot,
        StaffOfHappines,
        Devastation,

        CloakOfWisdom,
        DragonScaleArmor,
        SaruansResolv,
        JusticeGaze,
        FirestoneWalkers
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
        private int stageIndex, oldStageIndex, hIndex, oldHIndex, eIndex, oldEIndex, ItemIndex, oldItemIndex;
        GuidLineStages currStage;
        Heros currHero;
        Enemies currEnemy;
        Items currItem;
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
                    if (ButtonPress(PlayerIndex.One, PlayerInput.Left) || ButtonPress(PlayerIndex.Two, PlayerInput.Left))
                        hIndex = UpdateIndex(-1, hIndex, 7);
                    else if (ButtonPress(PlayerIndex.One, PlayerInput.Right) || ButtonPress(PlayerIndex.Two, PlayerInput.Right))
                        hIndex = UpdateIndex(1, hIndex, 7);

                    if (oldHIndex != hIndex)
                    {
                        currHero = (Heros)hIndex;
                        oldHIndex = hIndex;
                    }

                    break;
                case Sections.Enemies:
                    if (ButtonPress(PlayerIndex.One, PlayerInput.Left) || ButtonPress(PlayerIndex.Two, PlayerInput.Left))
                        eIndex = UpdateIndex(-1, eIndex, 8);
                    else if (ButtonPress(PlayerIndex.One, PlayerInput.Right) || ButtonPress(PlayerIndex.Two, PlayerInput.Right))
                        eIndex = UpdateIndex(1, eIndex, 8);

                    if (oldEIndex != eIndex)
                    {
                        currEnemy = (Enemies)eIndex;
                        oldEIndex = eIndex;
                    }

                    break;
                case Sections.Items:
                    if (ButtonPress(PlayerIndex.One, PlayerInput.Left) || ButtonPress(PlayerIndex.Two, PlayerInput.Left))
                        ItemIndex = UpdateIndex(-1, ItemIndex, 13);
                    else if (ButtonPress(PlayerIndex.One, PlayerInput.Right) || ButtonPress(PlayerIndex.Two, PlayerInput.Right))
                        ItemIndex = UpdateIndex(1, ItemIndex, 13);

                    if (oldItemIndex != ItemIndex)
                    {
                        currItem = (Items)ItemIndex;
                        oldItemIndex = ItemIndex;
                    }
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
                    DrawGuideLine(SB);
                    break;
                case Sections.Controlls:
                    DrawControls(SB);
                    break;
                case Sections.Heros:
                    DrawHeros(SB);
                    break;
                case Sections.Enemies:
                    DrawEnemies(SB);
                    break;
                case Sections.Items:
                    DrawItemInfo(SB);
                    break;
                default:
                    break;
            }
        }

        private void DrawGuideLine(SpriteBatch SB)
        {
            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), intro,
                centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 45), Color.Gold, 1);
            switch (currStage)
            {
                case GuidLineStages.Stage_1:
                    DrawGuideSTAGE_1(SB);
                    break;
                case GuidLineStages.Stage_2:
                    DrawGuideSTAGE_2(SB);
                    break;
                case GuidLineStages.Stage_3:
                    DrawGuideSTAGE_3(SB);
                    break;
                case GuidLineStages.Attributes:
                    DrawGuideAttribute(SB);
                    break;
                case GuidLineStages.Other:
                    DrawGuideOther(SB);
                    break;
                default:
                    break;
            }
        }

        //Guide draw
        private void DrawGuideSTAGE_1(SpriteBatch SB)
        {
            //Text
            
            string Info_1 = "First try to level up by kill the Enemies that are spawning and gatter exp and gold.\n"+
                "with the gold, buy yourself some items, to deal more damage and take less.";
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
        private void DrawGuideSTAGE_2(SpriteBatch SB)
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
        private void DrawGuideSTAGE_3(SpriteBatch SB)
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
        private void DrawGuideAttribute(SpriteBatch SB)
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
        private void DrawGuideOther(SpriteBatch SB)
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
            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Hero information",
                centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 45), Color.Gold, 1);
            switch (currHero)
            {
                case Heros.ElvenArcher:
                    DrawArcherInfo(SB);
                    break;
                case Heros.Mage:
                    DrawMageInfo(SB);
                    break;
                case Heros.Gryphon_Rider:
                    DrawGryphonRiderInfo(SB);
                    break;
                case Heros.FootMan:
                    DrawFootManInfo(SB);
                    break;
                case Heros.Dwarven:
                    DrawDwarvenInfo(SB);
                    break;
                case Heros.Gnomish_Flying_Machine:
                    DrawGnomeInfo(SB);
                    break;
                case Heros.Knight:
                    DrawKnightInfo(SB);
                    break;
                default:
                    break;
            }
        }

        private void DrawArcherInfo(SpriteBatch SB)
        {
            string heroInfo = "Emma a strong archer from the woods\n"+
                "Hp: 1200\nMp: 200\nDificulty: Easy\nAgi: 20\nStrg: 20\nInt: 20\n"+
                "Special ability: Shooting harpons at enemies";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Emma",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), heroInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 1 / 7",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Main attribute",
                centerInfoPanel + new Vector2(-420, -infoPanelSize.Y + 180), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-500, -infoPanelSize.Y + 250),
               new Rectangle(112, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("ArcherSheet"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawMageInfo(SpriteBatch SB)
        {
            string heroInfo = "Constantine an aprentis the dark art of magic\n" +
                "Hp: 1200\nMp: 200\nDificulty: Easy\nAgi: 20\nStrg: 20\nInt: 20\n" +
                "Special ability: Shooting a spirit tornados at enemies";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Constantine",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), heroInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 2 / 7",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Main attribute",
                centerInfoPanel + new Vector2(-420, -infoPanelSize.Y + 180), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-500, -infoPanelSize.Y + 250),
               new Rectangle(0, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("MageSheet"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawGryphonRiderInfo(SpriteBatch SB)
        {
            string heroInfo = "Gordox the thunder warrior in the sky\n" +
                "Hp: 1200\nMp: 200\nDificulty: Easy\nAgi: 20\nStrg: 20\nInt: 20\n" +
                "Special ability: Increase the movment speed";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Gordox",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), heroInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 3 / 7",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Main attribute",
                centerInfoPanel + new Vector2(-420, -infoPanelSize.Y + 180), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-500, -infoPanelSize.Y + 250),
               new Rectangle(0, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("GryponRiderSheet"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(384, 0, 96, 96), Color.White, 0, Vector2.Zero, 2.7f, SpriteEffects.None, 0);
        }
        private void DrawFootManInfo(SpriteBatch SB)
        {
            string heroInfo = "Jakob a nobel soldier that take pride in his work\n" +
                "Hp: 1200\nMp: 200\nDificulty: Easy\nAgi: 20\nStrg: 20\nInt: 20\n" +
                "Special ability: Slam of Justice";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Jakob",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), heroInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 4 / 7",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Main attribute",
                 centerInfoPanel + new Vector2(-420, -infoPanelSize.Y + 180), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-500, -infoPanelSize.Y + 250),
               new Rectangle(224, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("FootManSheet"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawDwarvenInfo(SpriteBatch SB)
        {
            string heroInfo = "Horpos bros, two brothers that loves to fight and hate to lose!\n" +
                "Hp: 1200\nMp: 200\nDificulty: Easy\nAgi: 20\nStrg: 20\nInt: 20\n" +
                "Special ability: Rage";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Horpos",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), heroInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 5 / 7",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Main attribute",
                centerInfoPanel + new Vector2(-420, -infoPanelSize.Y + 180), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-500, -infoPanelSize.Y + 250),
               new Rectangle(224, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Dwarven"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(224, 0, 56, 56), Color.White, 0, Vector2.Zero, 4.6f, SpriteEffects.None, 0);
        }
        private void DrawGnomeInfo(SpriteBatch SB)
        {
            string heroInfo = "Zoegas Nation is a crasy gnome that think he is in the sky\nbut is biking on the ground!\n" +
                "Hp: 1200\nMp: 200\nDificulty: Easy\nAgi: 20\nStrg: 20\nInt: 20\n" +
                "Special ability: Big canon bal";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Zoegas Nation",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), heroInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 6 / 7",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Main attribute",
                centerInfoPanel + new Vector2(-420, -infoPanelSize.Y + 180), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-500, -infoPanelSize.Y + 250),
               new Rectangle(112, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("Gnomish"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(320, 0, 80, 80), Color.White, 0, Vector2.Zero, 3.2f, SpriteEffects.None, 0);
        }
        private void DrawKnightInfo(SpriteBatch SB)
        {
            string heroInfo = "Lucifer, an asshole that only care about him self\n" +
                "Hp: 1200\nMp: 200\nDificulty: Easy\nAgi: 20\nStrg: 20\nInt: 20\n" +
                "Special ability: Increase the movment speed";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Lucifer",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), heroInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 7 / 7",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Main attribute",
                 centerInfoPanel + new Vector2(-420, -infoPanelSize.Y + 180), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("StatsIcons"), centerInfoPanel + new Vector2(-500, -infoPanelSize.Y + 250),
               new Rectangle(224, 111, 112, 111), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("KnightSheet"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(288, 0, 72, 72), Color.White, 0, Vector2.Zero, 3.5f, SpriteEffects.None, 0);
        }

        //Enemies draw
        private void DrawEnemies(SpriteBatch SB)
        {
            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Enemy information",
                centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 45), Color.Gold, 1);
            switch (currEnemy)
            {
                case Enemies.Goblin:
                    DrawGoblinInfo(SB);
                    break;
                case Enemies.Grunt:
                    DrawGruntInfo(SB);
                    break;
                case Enemies.Orge:
                    DrawOrgeInfo(SB);
                    break;
                case Enemies.Skeleton:
                    DrawSkeletonInfo(SB);
                    break;
                case Enemies.Troll:
                    DrawTrollInfo(SB);
                    break;
                case Enemies.DeathKnight:
                    DrawDeathKnightInfo(SB);
                    break;
                case Enemies.Demon:
                    DrawDemonInfo(SB);
                    break;
                case Enemies.Dragon:
                    DrawDragonInfo(SB);
                    break;
                default:
                    break;
            }
        }

        private void DrawGoblinInfo(SpriteBatch SB)
        {
            string enemyInfo = "Goblin\n" +
                "Type: Melee";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Goblin",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 1 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Goblin"), centerInfoPanel + new Vector2(-96, -infoPanelSize.Y + 200),
               new Rectangle(192, 0, 48, 48), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawGruntInfo(SpriteBatch SB)
        {
            string enemyInfo = "Grunt\n" +
                "Type: Melee";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Grunt",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 2 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Grunt"), centerInfoPanel + new Vector2(-96, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawOrgeInfo(SpriteBatch SB)
        {
            string enemyInfo = "Orge\n" +
                "Type: Range";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Orge",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 3 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Orge"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawSkeletonInfo(SpriteBatch SB)
        {
            string enemyInfo = "Skeleton\n" +
                "Type: Melee";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Skeleton",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 4 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Skeleton"), centerInfoPanel + new Vector2(-96, -infoPanelSize.Y + 200),
               new Rectangle(192, 0, 48, 48), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawTrollInfo(SpriteBatch SB)
        {
            string enemyInfo = "Troll\n" +
                "Type: Range";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Troll",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 5 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Troll_Thrower"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawDeathKnightInfo(SpriteBatch SB)
        {
            string enemyInfo = "Death Knight\n" +
                "Type: Range\nStatus: Sub boss";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Death Knight",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 6 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Death_Knight"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawDemonInfo(SpriteBatch SB)
        {
            string enemyInfo = "Demon\n" +
                "Type: Range\nStatus: Mini boss";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Demon",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 7 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Demon"), centerInfoPanel + new Vector2(-128, -infoPanelSize.Y + 200),
               new Rectangle(256, 0, 64, 64), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }
        private void DrawDragonInfo(SpriteBatch SB)
        {
            string enemyInfo = "Dragon\n" +
                "Type: Range\nStatus: Final boss";

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Dragon",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 180), Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), enemyInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, 0), Color.Gold, 1);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page 8 / 8",
               centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);

            SB.Draw(ResourceManager.GetTexture("Dragon"), centerInfoPanel + new Vector2(-192, -infoPanelSize.Y + 200),
               new Rectangle(384, 384, 96, 96), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        }

        //Items draw
        private void DrawItemInfo(SpriteBatch SB)
        {
            //Text
            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Item information",
               centerInfoPanel + new Vector2(0, -infoPanelSize.Y + 45), Color.Gold, 1);

            switch (currItem)
            {
                case Items.StartInfo:
                    DrawItemStartInfo(SB);
                    break;
                case Items.HealingPotion:
                    //Text
                    DrawPotionInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new HealingPotion());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(78, 0, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.ManaPotion:
                    //Text
                    DrawPotionInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new ManaPotion());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(156, 0, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.Recarnation:
                    //Text
                    DrawPotionInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new Reincarnation());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(234, 0, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.Cleave:
                    //Text
                    DrawWeaponInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new Cleave());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(0, 78, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.Multishoot:
                    //Text
                    DrawWeaponInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new MultiShot());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(234, 78, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.StaffOfHappines:
                    //Text
                    DrawWeaponInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new StaffOfHappiness());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(0, 156, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.Devastation:
                    //Text
                    DrawWeaponInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new Devastation());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(78, 78, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.CloakOfWisdom:
                    //Text
                    DrawArmorInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new CloakOfWisdom());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(234, 156, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.DragonScaleArmor:
                    //Text
                    DrawArmorInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new DragonScaleChest());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(156, 156, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.SaruansResolv:
                    //Text
                    DrawArmorInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new SaruansResolve());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(0, 234, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.JusticeGaze:
                    //Text
                    DrawArmorInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new JusticeGaze());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(78, 234, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                case Items.FirestoneWalkers:
                    //Text
                    DrawArmorInfo(SB, centerInfoPanel + new Vector2(-infoPanelSize.X + 200, -140), new FirestoneWalkers());
                    //IMG
                    SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -150),
                       new Rectangle(156, 234, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    break;
                default:
                    break;
            }

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_16"), "Page "+(ItemIndex+1)+" / 13",
                       centerInfoPanel + new Vector2(0, infoPanelSize.Y - 20), Color.Gold, 1);
        }

        private void DrawItemStartInfo(SpriteBatch SB)
        {
            string itemInfo = "The shop has an excellent varieties of items for you to buy.\n" +
                            "You have up to 16 different typs of items to choose from.\nFrom health potions to cheast armor.\n" +
                            "But do not take to long to decide what to buy. You still have a castle to protect.\nSo be safe my hero!\n" +
                            "On the following pages more details on each item will be found.";

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), itemInfo,
              centerInfoPanel + new Vector2(-infoPanelSize.X + 30, -250), Color.Gold, 1);

            //IMG
            SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-500, -50),
               new Rectangle(78, 0, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-100, -50),
               new Rectangle(234, 0, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(350, -50),
               new Rectangle(234, 78, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-500, 200),
               new Rectangle(0, 156, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(-100, 200),
               new Rectangle(156, 156, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            SB.Draw(ResourceManager.GetTexture("ItemIcons"), centerInfoPanel + new Vector2(350, 200),
               new Rectangle(78, 234, 78, 78), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
        }

        private void DrawPotionInfo(SpriteBatch SB, Vector2 pos, Potion p)
        {
            DrawString(SB, ResourceManager.GetFont("WarFont_22"),
                "Name: " + p.ItemName
                + "\nCost: " + p.GetItemCost + " gold", pos, Color.Gold, 1);

            if (p.PotionType == PotionType.HealingPotion || p.PotionType == PotionType.GreaterHealingPotion)
                DrawString(SB, ResourceManager.GetFont("WarFont_22"), "\n\nHeals: " + p.Healing, pos, Color.Gold, 1);
            else if (p.PotionType == PotionType.ManaPotion || p.PotionType == PotionType.GreaterManaPotion)
                DrawString(SB, ResourceManager.GetFont("WarFont_22"), "\n\nMana restoring: " + p.ManaRestoring, pos, Color.Gold, 1);
            else if (p.PotionType == PotionType.RejuvenationPotion)
                DrawString(SB, ResourceManager.GetFont("WarFont_22"), "\n\nRevive on death" +
                                                                      "\nWill REMOVE ALL healing potion" +
                                                                      "\nHeals: " + p.Healing +
                                                                      "\nMana restoring: " + p.ManaRestoring, pos, Color.Gold, 1);
        }
        private void DrawWeaponInfo(SpriteBatch SB, Vector2 pos, Weapon w)
        {
            DrawString(SB, ResourceManager.GetFont("WarFont_22"),
                "Name: " + w.ItemName
                + "\nCost: " + w.GetItemCost + " gold", pos, Color.Gold, 1);

            string extraInfo = "\n";
            if (w.WeaponType == WeaponType.Cleave)
                extraInfo += "\nFor: Melee type";
            else if (w.WeaponType == WeaponType.MultiShot)
                extraInfo += "\nFor: Range type";

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), extraInfo +
                                                                  "\nDamage: " + w.GetItemDamage +
                                                                  "\nStrength: " + w.GetItemStrength +
                                                                  "\nAgility: " + w.GetItemAgility +
                                                                  "\nInteligence: " + w.GetItemInteligence,

                                                                  pos, Color.Gold, 1);
        }
        private void DrawArmorInfo(SpriteBatch SB, Vector2 pos, Armor a)
        {
            DrawString(SB, ResourceManager.GetFont("WarFont_22"),
                "Name: " + a.ItemName
                + "\nCost: " + a.GetItemCost + " gold", pos, Color.Gold, 1);

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), "\n\nArmor: " + a.GetItemArmor +
                                                                  "\nStrength: " + a.GetItemStrength +
                                                                  "\nAgility: " + a.GetItemAgility +
                                                                  "\nInteligence: " + a.GetItemInteligence,
                                                                  pos, Color.Gold, 1);
        }
        //----- Other -----//

    }
}
