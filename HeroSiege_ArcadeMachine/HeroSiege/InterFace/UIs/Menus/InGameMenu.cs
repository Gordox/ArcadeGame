using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.InterFace.UIs.Buttons;
using HeroSiege.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using HeroSiege.Manager;

namespace HeroSiege.InterFace.UIs.Menus
{
    class InGameMenu : UI
    {
        public bool MenuActive { get; set; }
        GameScene gameScene;

        public enum Buttons
        {
            Continue,
            Restart,
            Main_Menu
        }

        Vector2 position;
        BigButton bnContinue, bnRestart, bnMainMenu;
        int bnindex, offset;
        Buttons buttonState, oldButtonState;

        public InGameMenu(GameScene gameScene)
        {
            this.gameScene = gameScene;
            position = new Vector2(1920 / 2, 1080 / 2);
            Init();
        }

        private void Init()
        {
            offset = ResourceManager.GetTexture("InGameMenu").region.Width / 2;
            //Buttons
            bnContinue = new BigButton(position + new Vector2(0, -320), "Continue");
            bnRestart = new BigButton(position + new Vector2(0, -220), "Restart");
            bnMainMenu = new BigButton(position + new Vector2(0, -120), "Main menu");

            bnContinue.Size = bnRestart.Size = bnMainMenu.Size =  2.7f;
            bnContinue.Selected = true;
        }

        //----- Updates -----//
        public override void Update(float delta)
        {
            if (!MenuActive)
                return;
            UpdateJoystick();
            if (buttonState != oldButtonState)
                UpdateSelectedMark();

            UpdateSelected();
        }

        private void UpdateSelectedMark()
        {
            bnContinue.Selected = bnRestart.Selected = bnMainMenu.Selected = false;
            switch (buttonState)
            {
                case Buttons.Continue:
                    bnContinue.Selected = true;
                    break;
                case Buttons.Main_Menu:
                    bnMainMenu.Selected = true;
                    break;
                case Buttons.Restart:
                    bnRestart.Selected = true;
                    break;
                default:
                    break;
            }
            oldButtonState = buttonState;
        }
        private void UpdateSelected()
        {
            bnContinue.ButtonState = bnRestart.ButtonState = bnMainMenu.ButtonState = ButtonState.Active;
            switch (buttonState)
            {
                case Buttons.Continue:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnContinue.ButtonState = ButtonState.pressed;

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                        MenuActive = false;
                    break;

                case Buttons.Restart:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnRestart.ButtonState = ButtonState.pressed;

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                    {
                        MenuActive = false;
                        gameScene.Restart();
                        buttonState = Buttons.Continue;
                        bnContinue.Selected = true;
                        bnRestart.Selected = bnMainMenu.Selected = false;
                        bnindex = 0;
                    }

                    break;
                case Buttons.Main_Menu:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnMainMenu.ButtonState = ButtonState.pressed;

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                    {
                        MenuActive = false;
                        gameScene.GoToMainManu();
                        bnContinue.ButtonState = bnRestart.ButtonState = bnMainMenu.ButtonState = ButtonState.Active;
                    }
                        break;
                default:

                    break;
            }
            oldButtonState = buttonState;
        }
        private void UpdateSelectIndex(int i)
        {
            bnindex += i;

            if (bnindex > 2)
                bnindex -= 3;
            else if (bnindex < 0)
                bnindex += 3;

            buttonState = (Buttons)bnindex;
        }
        private void UpdateJoystick()
        {
            if (ButtonPress(PlayerIndex.One, PlayerInput.Down) || ButtonPress(PlayerIndex.Two, PlayerInput.Down))
                UpdateSelectIndex(1);
            else if (ButtonPress(PlayerIndex.One, PlayerInput.Up) || ButtonPress(PlayerIndex.Two, PlayerInput.Up))
                UpdateSelectIndex(-1);
        }

        //----- Draw -----//
        public override void Draw(SpriteBatch SB)
        {
            if (!MenuActive)
                return;
            SB.Begin();
            SB.Draw(ResourceManager.GetTexture("BlackPixel"), position + new Vector2(-offset + 10, -position.Y + 120), new Rectangle(0, 0, 550, 450), Color.White * 0.7f);
            SB.Draw(ResourceManager.GetTexture("InGameMenu"), position + new Vector2(-offset, -position.Y + 110), Color.White);
            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "GAME PAUSED", position + new Vector2(0, -position.Y + 200), Color.Gold, 1);

            bnContinue.DrawCenter(SB);
            bnRestart.DrawCenter(SB);
            bnMainMenu.DrawCenter(SB);

            SB.End();
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
