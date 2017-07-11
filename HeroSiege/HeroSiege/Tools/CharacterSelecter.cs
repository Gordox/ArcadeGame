using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.InterFace.UIs;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Tools
{
    class CharacterSelecter : UI
    {
        const float FRAME_DURATION_MOVEMNT = 0.05f;

        public CharacterType SelectedHero { get; private set; }
        private PlayerIndex playerIndex;
        private int selectIndex, oldIndex;
        private Vector2 drawPos;
        Sprite animations;
        private string titleText = string.Empty, bioText = string.Empty;
        HeroInfo hInfo;

        public CharacterSelecter(PlayerIndex playerIndex, Vector2 pos)
        {
            this.playerIndex = playerIndex;
            this.drawPos = pos;
            InitAnimations();
            Init();
        }

        public void Init()
        {         
            SelectedHero = CharacterType.None;
            selectIndex = oldIndex = 7;
            animations.SetSize(128, 128);
            animations.SetAnimation("Looking");
        }

        private void InitAnimations()
        {
            animations = new Sprite(null, drawPos.X, drawPos.Y+20, 0, 0);
            animations.PauseAnimation = false;
            animations.AddAnimation("Mage",        new FrameAnimation(ResourceManager.GetTexture("MageSheet"),        256, 0, 64, 64,  4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            animations.AddAnimation("Knight",      new FrameAnimation(ResourceManager.GetTexture("KnightSheet"),      288, 0, 72, 72,  5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            animations.AddAnimation("Gryphon",     new FrameAnimation(ResourceManager.GetTexture("GryponRiderSheet"), 384, 0, 96, 96,  4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            animations.AddAnimation("Foot",        new FrameAnimation(ResourceManager.GetTexture("FootManSheet"),     256, 0, 64, 64,  5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            animations.AddAnimation("Dwarven",     new FrameAnimation(ResourceManager.GetTexture("Dwarven"),          224, 0, 56, 56,  5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            animations.AddAnimation("Gnome",       new FrameAnimation(ResourceManager.GetTexture("Gnomish"),          320, 0, 80, 80,  2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            animations.AddAnimation("Elven",       new FrameAnimation(ResourceManager.GetTexture("ArcherSheet"),      256, 0, 64, 64,  4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            animations.AddAnimation("Looking",     new FrameAnimation(ResourceManager.GetTexture("Eye"),                0, 0, 64, 64, 30, FRAME_DURATION_MOVEMNT, new Point(30, 1)));

        }

        public override void Update(float delta)
        {
            animations.Update(delta);

            UpdateJoystick();
            if (selectIndex != oldIndex)
                UpdateSelection();
        }

        private void UpdateSelection()
        {
            switch (SelectedHero)
            {
                case CharacterType.ElvenArcher:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Elven");
                    titleText = "Elven Archer";
                    hInfo = new HeroInfo() { name = "Horpos", type = "Range", hp = "1200", mp = "200", dif = "Easy" };
                    break;
                case CharacterType.Mage:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Mage");
                    titleText = "Mage";
                    hInfo = new HeroInfo() { name = "Horpos", type = "Range", hp = "1200", mp = "200", dif = "Easy" };
                    break;
                case CharacterType.Gryphon_Rider:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Gryphon");
                    titleText = "Gryphon Rider";
                    hInfo = new HeroInfo() { name = "Horpos", type = "Range", hp = "1200", mp = "200", dif = "Easy" };
                    break;
                case CharacterType.FootMan:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Foot");
                    titleText = "Foot Man";
                    hInfo = new HeroInfo() { name = "Horpos", type = "Melee", hp = "1200", mp = "200", dif = "Easy" };
                    break;
                case CharacterType.Dwarven:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Dwarven");
                    titleText = "Dwarven";
                    hInfo = new HeroInfo() { name = "Gordox", type = "Melee", hp = "1200", mp = "200", dif = "Easy" };
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Gnome");
                    titleText = "Gnomish Flying Machine";
                    hInfo = new HeroInfo() { name = "Horpos", type = "Range", hp = "1200", mp = "200", dif = "Easy" };
                    break;
                case CharacterType.Knight:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Knight");
                    titleText = "Knight";
                    hInfo = new HeroInfo() { name = "Horpos", type = "Melee", hp = "1200", mp = "200", dif = "Easy"};
                    break;
                case CharacterType.None:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Looking");
                    break;
                default:
                    break;
            }
            oldIndex = selectIndex;
        }

        private void UpdateSelected(int i)
        {
            selectIndex += i;

            if (selectIndex > 6)
                selectIndex -= 7;
            else if (selectIndex < 0)
                selectIndex += 7;

            SelectedHero = (CharacterType)selectIndex;
        }
        private void UpdateJoystick()
        {
            if (ButtonPress(PlayerInput.Right))
                UpdateSelected(1);
            else if (ButtonPress(PlayerInput.Left))
                UpdateSelected(-1);
        }


        public override void Draw(SpriteBatch SB)
        {
            int w = ResourceManager.GetTexture("InGameMenu").region.Width;
            int h = ResourceManager.GetTexture("InGameMenu").region.Height;
            //Animation
            SB.Draw(ResourceManager.GetTexture("BlackPixel"), drawPos + new Vector2(10, 10), new Rectangle(0, 0, (int)(w) - 40, (int)(h) - 40), Color.White, 0, new Vector2(w / 4, h / 8), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            SB.Draw(ResourceManager.GetTexture("InGameMenu"), drawPos, null, Color.White, 0, new Vector2(w/4, h/8), 0.5f, SpriteEffects.None, 0);
            animations.Draw(SB);
            //Hero info
            if (SelectedHero != CharacterType.None)
            {
                SB.Draw(ResourceManager.GetTexture("ItemInfoWindow"), drawPos + new Vector2(-85, 220), null, Color.White, 0, Vector2.One, 1.2f, SpriteEffects.None, 0);
                string infoText1 = "Name: " + hInfo.name + "\n"+
                                   "Type: " + hInfo.type + "\n"+
                                   "Dificulty: "+ hInfo.dif + "\n";
                string infoText2 = "Hp: " + hInfo.hp + "\n" + "Mp: " + hInfo.mp;

                DrawString(SB, ResourceManager.GetFont("WarFont_16"), infoText1, drawPos + new Vector2(-80, 230), Color.Gold, 1);
                DrawString(SB, ResourceManager.GetFont("WarFont_16"), infoText2, drawPos + new Vector2(120, 230), Color.Gold, 1);
            }


            if(SelectedHero == CharacterType.None)
                DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Looking for player", drawPos + new Vector2(65, -40), Color.Gold, 1);
            else
            {
                DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), titleText, drawPos + new Vector2(65, -40), Color.Gold, 1);

                DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), bioText, drawPos + new Vector2(65, -40), Color.Gold, 1);
            }
        }

        private bool ButtonPress(PlayerInput b)
        {
            return InputHandler.GetButtonState(playerIndex, b) == InputState.Released;
        }

        struct HeroInfo
        {
            public string name;
            public string type;
            public string hp;
            public string mp;
            public string dif;
        }
    }
}
