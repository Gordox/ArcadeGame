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
    class StartMenu : UI
    {
        StartScene startSceen;

        public enum Buttons
        {
            Start_Game,
            HighScore,
            Option,
            Credits
        }

        BigButton bnStartGame, bnOption, bnCredit, bnHighScore;
        Vector2 position;

        int offset;
        int bgIndex;
        Buttons buttonState, oldButtonState;
        int bnindex;

        public StartMenu(StartScene startSceen, Viewport viewPort)
        {
            this.startSceen = startSceen;
            bgIndex = new Random().Next(1, 11);
            position = new Vector2(viewPort.Width / 2, viewPort.Height / 2);
            Init();
        }

        private void Init()
        {
            offset = ResourceManager.GetTexture("StartMenu").region.Width / 2;
            //Buttons
            bnStartGame = new BigButton(position + new Vector2(0, -320), "Start Game");
            bnHighScore = new BigButton(position + new Vector2(0, -220), "High Score");
            bnOption =    new BigButton(position + new Vector2(0, -120), "Option");
            bnCredit =    new BigButton(position + new Vector2(0,  -20), "Credit");

            bnStartGame.Size = bnOption.Size = bnCredit.Size = bnHighScore.Size = 2.7f;
            bnStartGame.Selected = true;
            bnHighScore.ButtonActive = bnOption.ButtonActive = bnCredit.ButtonActive = false;
            bnHighScore.ButtonState = bnOption.ButtonState = bnCredit.ButtonState = ButtonState.inActive;

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
            bnStartGame.Selected = bnHighScore.Selected = bnOption.Selected = bnCredit.Selected = false;
            switch (buttonState)
            {
                case Buttons.Start_Game:
                    bnStartGame.Selected = true;
                    break;
                case Buttons.HighScore:
                    bnHighScore.Selected = true;
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
        private void UpdateSelected()
        {
            bnStartGame.ButtonState =  ButtonState.Active;
            bnHighScore.ButtonState = bnOption.ButtonState = bnCredit.ButtonState = ButtonState.inActive;
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
                case Buttons.Option:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnOption.ButtonState = ButtonState.pressed;
                        
                    break;
                case Buttons.Credits:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnCredit.ButtonState = ButtonState.pressed;
                    break;
                default:

                    break;
            }
            oldButtonState = buttonState;
        }
        private void UpdateSelectIndex(int i)
        {
            bnindex += i;

            if (bnindex > 3)
                bnindex -= 4;
            else if (bnindex < 0)
                bnindex += 4;

            buttonState = (Buttons)bnindex;
        }
        private void UpdateJoystick()
        {
            if (ButtonPress(PlayerIndex.One,PlayerInput.Down) || ButtonPress(PlayerIndex.Two, PlayerInput.Down))
                UpdateSelectIndex(1);
            else if (ButtonPress(PlayerIndex.One, PlayerInput.Up) || ButtonPress(PlayerIndex.Two, PlayerInput.Up))
                UpdateSelectIndex(-1);
        }

        //----- Draw -----//
        public override void Draw(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("BG_"+ bgIndex), Vector2.Zero, Color.White);

            SB.Draw(ResourceManager.GetTexture("StartMenu"), position + new Vector2(-offset, -position.Y - 20), Color.White);
            //Buttons
            bnStartGame.DrawCenter(SB);
            bnHighScore.DrawCenter(SB);
            bnOption.DrawCenter(SB);
            bnCredit.DrawCenter(SB);
            SB.Draw(ResourceManager.GetTexture("ImgStartScreen"),  new Vector2(0, 0), Color.White);
        }


        public Buttons GetCurrentSelected
        {
            get { return buttonState; }
        }

        private bool ButtonDown(PlayerIndex index, PlayerInput b)
        {
            return InputHandler.GetButtonState(index, b) == InputState.Down;
        }
        private bool ButtonPress(PlayerIndex index, PlayerInput b)
        {
            return InputHandler.GetButtonState(index, b) == InputState.Released;
        }
    }
}
