using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.InterFace.UIs.Buttons;
using Microsoft.Xna.Framework;
using HeroSiege.FTexture2D;
using HeroSiege.Manager;
using Microsoft.Xna.Framework.Input;
using HeroSiege.Scenes;

namespace HeroSiege.InterFace.UIs.Menus
{
    class StartMenu : Menu
    {
        StartScene startSceen;

        public enum Buttons
        {
            Start_Game,
            HighScore,
            How_To_Play,
            Option,
            Credits
        }

        BigButton bnStartGame, bnOption, bnCredit, bnHighScore, bnHowToPlay;

        int offset;
        int bgIndex; //Background index
        Buttons buttonState, oldButtonState;

        public StartMenu(StartScene startSceen, Viewport viewPort) 
            : base(viewPort)
        {
            this.startSceen = startSceen;
            bgIndex = new Random().Next(1, 11);
            Init();
        }

        private void Init()
        {
            offset = ResourceManager.GetTexture("StartMenu").region.Width / 2;
            //Buttons
            bnStartGame = new BigButton(centerViewPort + new Vector2(0, -320), "Start Game");
            bnHighScore = new BigButton(centerViewPort + new Vector2(0, -220), "High Score");
            bnHowToPlay = new BigButton(centerViewPort + new Vector2(0, -120), "How to play");
            bnOption =    new BigButton(centerViewPort + new Vector2(0, -20), "Option");
            bnCredit =    new BigButton(centerViewPort + new Vector2(0,  80), "Credit");

            bnStartGame.Size = bnOption.Size = bnCredit.Size = bnHighScore.Size = bnHowToPlay.Size = 2.7f;
            bnStartGame.Selected = true;
            bnHighScore.ButtonActive = bnOption.ButtonActive = false;
            bnHighScore.ButtonState = bnOption.ButtonState = ButtonState.inActive;

        }

        //----- Updates -----//
        public override void Update(float delta)
        {
            UpdateJoystick();
            if(buttonState != oldButtonState)
                UpdateSelectedMark();
            UpdateSelected();
        }

        private void UpdateSelectedMark()
        {
            bnStartGame.Selected = bnHighScore.Selected = bnOption.Selected = bnCredit.Selected = bnHowToPlay.Selected = false;
            switch (buttonState)
            {
                case Buttons.Start_Game:
                    bnStartGame.Selected = true;
                    break;
                case Buttons.HighScore:
                    bnHighScore.Selected = true;
                    break;
                case Buttons.How_To_Play:
                    bnHowToPlay.Selected = true;
                    break;
                case Buttons.Option:
                    bnOption.Selected = true;
                    break;   
                case Buttons.Credits:
                    bnCredit.Selected = true;
                    break;
                default:
                    break;
            }
            oldButtonState = buttonState;
        }
        protected override void UpdateSelected()
        {
            bnStartGame.ButtonState = bnCredit.ButtonState = bnHowToPlay.ButtonState = ButtonState.Active;
            bnHighScore.ButtonState = bnOption.ButtonState = ButtonState.inActive;
            switch (buttonState)
            {
                case Buttons.Start_Game:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnStartGame.ButtonState = ButtonState.pressed;                       

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                        startSceen.SetNewScreen(Screens.SelectCharacters);

                    break;
                case Buttons.HighScore:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnHighScore.ButtonState = ButtonState.pressed;

                    break;
                case Buttons.How_To_Play:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnHowToPlay.ButtonState = ButtonState.pressed;

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                        startSceen.SetNewScreen(Screens.HowToPlay);

                    break;
                case Buttons.Option:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnOption.ButtonState = ButtonState.pressed;
                        
                    break;
                case Buttons.Credits:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnCredit.ButtonState = ButtonState.pressed;

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                        startSceen.SetNewScreen(Screens.Credit);
                    break;
                default:

                    break;
            }
            oldButtonState = buttonState;
        }
        protected override void UpdateSelectIndex(int i)
        {
            currentIndex += i;

            if (currentIndex > 4)
                currentIndex -= 5;
            else if (currentIndex < 0)
                currentIndex += 5;

            buttonState = (Buttons)currentIndex;
        }

        //----- Draw -----//
        public override void Draw(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("BG_"+ bgIndex), Vector2.Zero, Color.White);

            SB.Draw(ResourceManager.GetTexture("StartMenu"), centerViewPort + new Vector2(-offset, -centerViewPort.Y - 20), Color.White);
            //Buttons
            bnStartGame.DrawCenter(SB);
            bnHighScore.DrawCenter(SB);
            bnHowToPlay.DrawCenter(SB);
            bnOption.DrawCenter(SB);
            bnCredit.DrawCenter(SB);
            SB.Draw(ResourceManager.GetTexture("ImgStartScreen"),  new Vector2(0, 0), Color.White);
        }

        //----- Other -----//
        public Buttons GetCurrentSelected
        {
            get { return buttonState; }
        }
    }
}
