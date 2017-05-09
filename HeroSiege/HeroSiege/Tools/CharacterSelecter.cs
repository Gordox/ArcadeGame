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
            animations = new Sprite(null, drawPos.X, drawPos.Y, 0, 0);
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
                    break;
                case CharacterType.Mage:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Mage");
                    break;
                case CharacterType.Gryphon_Rider:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Gryphon");
                    break;
                case CharacterType.FootMan:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Foot");
                    break;
                case CharacterType.Dwarven:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Dwarven");
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Gnome");
                    break;
                case CharacterType.Knight:
                    animations.SetSize(128, 128);
                    animations.SetAnimation("Knight");
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
            int w = ResourceManager.GetTexture("ItemInfoWindow").region.Width;
            int h = ResourceManager.GetTexture("ItemInfoWindow").region.Height;
            SB.Draw(ResourceManager.GetTexture("ItemInfoWindow"), drawPos, null, Color.White, 0, new Vector2(w/4, h/8), new Vector2(1, 1), SpriteEffects.None, 0);
            animations.Draw(SB);

            if(SelectedHero == CharacterType.None)
                DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Looking for player", drawPos + new Vector2(80, -40), Color.Gold, 1);
        }

        private bool ButtonPress(PlayerInput b)
        {
            return InputHandler.GetButtonState(playerIndex, b) == InputState.Released;
        }
    }
}
