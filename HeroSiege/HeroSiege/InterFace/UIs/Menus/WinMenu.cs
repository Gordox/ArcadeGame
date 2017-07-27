using HeroSiege.InterFace.UIs.Buttons;
using HeroSiege.Manager;
using HeroSiege.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeroSiege.InterFace.UIs.Menus
{
    class WinMenu : UI
    {
        GameScene gameScene;

        public enum Buttons
        {
            Restart,
            Main_Menu
        }

        Vector2 position;
        BigButton bnRestart, bnMainMenu;
        int bnindex, offset;
        Buttons buttonState, oldButtonState;

        public WinMenu(GameScene gameScene)
        {
            this.gameScene = gameScene;
            position = new Vector2(1920 / 2, 1080 / 2);
            Init();
        }

        private void Init()
        {
            offset = ResourceManager.GetTexture("InGameMenu").region.Width / 2;
            //Buttons
            bnRestart = new BigButton(position + new Vector2(0, -120), "Restart");
            bnMainMenu = new BigButton(position + new Vector2(0, -220), "Main menu");
            bnRestart.Size = bnMainMenu.Size = 2.7f;
            bnRestart.Selected = true;
        }

        public override void Update(float delta)
        {
            UpdateJoystick();
            if (buttonState != oldButtonState)
                UpdateSelectedMark();

            UpdateSelected();
        }
        private void UpdateSelectedMark()
        {
            bnRestart.Selected = bnMainMenu.Selected = false;
            switch (buttonState)
            {
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
            bnRestart.ButtonState = bnMainMenu.ButtonState = ButtonState.Active;
            switch (buttonState)
            {

                case Buttons.Restart:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnRestart.ButtonState = ButtonState.pressed;

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                    {
                        gameScene.defeat = gameScene.victory = false;
                        gameScene.Restart();
                    }

                    break;
                case Buttons.Main_Menu:
                    if (ButtonDown(PlayerIndex.One, PlayerInput.A) || ButtonDown(PlayerIndex.Two, PlayerInput.A))
                        bnMainMenu.ButtonState = ButtonState.pressed;

                    if (ButtonPress(PlayerIndex.One, PlayerInput.A) || ButtonPress(PlayerIndex.Two, PlayerInput.A))
                    {
                        gameScene.GoToMainManu();
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

            if (bnindex > 1)
                bnindex -= 2;
            else if (bnindex < 0)
                bnindex += 2;

            buttonState = (Buttons)bnindex;
        }
        private void UpdateJoystick()
        {
            if (ButtonPress(PlayerIndex.One, PlayerInput.Down) || ButtonPress(PlayerIndex.Two, PlayerInput.Down))
                UpdateSelectIndex(1);
            else if (ButtonPress(PlayerIndex.One, PlayerInput.Up) || ButtonPress(PlayerIndex.Two, PlayerInput.Up))
                UpdateSelectIndex(-1);
        }

        public override void Draw(SpriteBatch SB)
        {
            SB.Begin();
            int w = ResourceManager.GetTexture("ImgDefeat").region.Width;
            int h = ResourceManager.GetTexture("ImgDefeat").region.Height;
            SB.Draw(ResourceManager.GetTexture("ImgDefeat"), position + new Vector2(-offset - w - 100, -h / 2 - 100), Color.White);
            SB.Draw(ResourceManager.GetTexture("ImgDefeat"), position + new Vector2(+offset + 100, -h / 2 - 100), Color.White);

            SB.Draw(ResourceManager.GetTexture("BlackPixel"), position + new Vector2(-offset + 10, -position.Y + 190), new Rectangle(0, 0, 550, 450), Color.White * 0.7f);
            SB.Draw(ResourceManager.GetTexture("InGameMenu"), position + new Vector2(-offset, -position.Y + 180), Color.White);
            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Victory", position + new Vector2(0, -position.Y + 280), Color.Gold, 2);
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
