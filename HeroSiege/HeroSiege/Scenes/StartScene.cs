using HeroSiege.FTexture2D;
using HeroSiege.InterFace.UIs.Buttons;
using HeroSiege.InterFace.UIs.Menus;
using HeroSiege.Manager;
using HeroSiege.Scenes.SceneSystem;
using HeroSiege.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Scenes
{
    public enum Screens
    {
        MainMenu,
        SelectCharacters,
        Options,
        Credit,
        HighScore
    }

    class StartScene : Scene
    {
        private CharacterSelecter playerOne, playerTwo;
        private GameSettings settings;

        private StartMenu startMenu;

        private DoorAnimation doorAnimation;

        private Screens currentSceene, newSceene;

       

        private bool doDoorAnimation = false;

        public bool PlayerOneActive { get; private set; }
        public bool PlayerTwoActive { get; private set; }

        //----- -----//
        public StartScene(GameSettings settings, GraphicsDeviceArcade graphics) : base()
        {
            this.currentSceene = newSceene = Screens.MainMenu;
            this.startMenu = new StartMenu(this, graphics.Viewport);
            this.settings = settings;
            this.Graphics = graphics;

            this.doorAnimation = new DoorAnimation(graphics.Viewport);
            SetOpenDoorAnimation();

            Init();
        }

        public override void Init()
        {
            PlayerOneActive = false;
            PlayerTwoActive = false;

            playerOne = new CharacterSelecter(PlayerIndex.One, new Vector2(Graphics.Viewport.Width/4, Graphics.Viewport.Height / 2));
            playerTwo = new CharacterSelecter(PlayerIndex.Two, new Vector2((Graphics.Viewport.Width / 4) * 3, Graphics.Viewport.Height / 2));
        }

        //----- Update -----//
        public override void Update(float delta, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(delta, otherSceneHasFocus, coveredByOtherScene);
            BackToMainMenu();
            UpdateSceene(delta);
            UpdateDoorAnimation(delta);
        }

        private void UpdateSceene(float delta)
        {
            if (doDoorAnimation)
                return;

            switch (currentSceene)
            {
                case Screens.MainMenu:
                    startMenu.Update(delta);
                    break;
                case Screens.SelectCharacters:
                    UpdateCharacterSelection(delta);
                    break;
                case Screens.Options:
                    break;
                case Screens.Credit:
                    break;
                default:
                    break;
            }
        }



        private void UpdateDoorAnimation(float delta)
        {
            if (doDoorAnimation)
            {
                doorAnimation.Update(delta);
                if (doorAnimation.Done)
                    doDoorAnimation = false;
            ChangeScreen();
            }
        }

        private void UpdateCharacterSelection(float delta)
        {
            playerOne.Update(delta);
            playerTwo.Update(delta);

            if (InputHandler.GetButtonState(PlayerIndex.One, PlayerInput.Start) == InputState.Released ||
                       InputHandler.GetButtonState(PlayerIndex.Two, PlayerInput.Start) == InputState.Released)
            {
                if (!PlayerOneActive && !PlayerTwoActive)
                    return;

                settings.playerOne = playerOne.SelectedHero;
                settings.playerTwo = playerTwo.SelectedHero;

                if (settings.playerOne == CharacterType.None && settings.playerTwo == CharacterType.None)
                    return;

                if (settings.playerOne == CharacterType.None || settings.playerTwo == CharacterType.None)
                    settings.GameMode = GameMode.singlePlayer;
                else
                    settings.GameMode = GameMode.Multiplayer;

                GameScene scene = new GameScene(settings, Graphics);
                AddScene(scene);
            }


            if (AnyKeyPressed(PlayerIndex.One) && PlayerOneActive == false)
                PlayerOneActive = true;
            if (AnyKeyPressed(PlayerIndex.Two) && PlayerTwoActive == false)
                PlayerTwoActive = true;
        }

        //----- Draw -----//
        public override void Draw(SpriteBatch SB)
        {
            SB.Begin();

            DrawSceenes(SB);

            if(doDoorAnimation)
                DrawDoorAnimation(SB);

            SB.End();
        }

        //Sceenes
        private void DrawSceenes(SpriteBatch SB)
        {
            switch (currentSceene)
            {
                case Screens.MainMenu:
                    DrawMainMenuSceene(SB);
                    break;
                case Screens.SelectCharacters:
                    DrawCharacterSelectionSceene(SB);
                    break;
                case Screens.Options:
                    DrawOptionSceene(SB);
                    break;
                case Screens.Credit:
                    DrawCreditSceene(SB);
                    break;
                default:
                    break;
            }
        }

        private void DrawMainMenuSceene(SpriteBatch SB)
        {
            startMenu.Draw(SB);
        }
        private void DrawCharacterSelectionSceene(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("BG_" + 1), Vector2.Zero, Color.White);
            playerOne.Draw(SB);
            playerTwo.Draw(SB);
        }
        private void DrawOptionSceene(SpriteBatch SB)
        {

        }
        private void DrawCreditSceene(SpriteBatch SB)
        {

        }

        private void DrawDoorAnimation(SpriteBatch SB)
        {
            doorAnimation.Draw(SB);
        }
        //----- Other -----//
        private void BackToMainMenu()
        {
            if (currentSceene != Screens.MainMenu)
                if (ButtonPress(PlayerIndex.One, PlayerInput.B) | ButtonPress(PlayerIndex.Two, PlayerInput.B) && !doDoorAnimation)
                    SetNewScreen(Screens.MainMenu);
        }
        public void SetNormalDoorAnimation()
        {
            doorAnimation.InitNormalAnimation();
            doDoorAnimation = true;
        }
        public void SetOpenDoorAnimation()
        {
            doorAnimation.InitOpenAnimation();
            doDoorAnimation = true;
        }
        public void SetNewScreen(Screens newScreen)
        {
            this.newSceene = newScreen;
            SetNormalDoorAnimation();
        }
        public void ChangeScreen()
        {
            if (doDoorAnimation && doorAnimation.type == DoorAnimation.AnimationType.NormalAnimation)
                if (doorAnimation.DoorClosed)
                {
                    currentSceene = newSceene;
                    if(newSceene == Screens.SelectCharacters)
                    {
                        playerOne.Init();
                        playerTwo.Init();
                    }

                }

        }

        //----- Buttons -----//
        public bool AnyKeyPressed(PlayerIndex index)
        {
            bool pressed = false;
            for (int i = 1; i < Enum.GetValues(typeof(PlayerInput)).Length; i++)
            {
                if (InputHandler.GetButtonState(index, (PlayerInput)i) == InputState.Released)
                    pressed = true;
            }

            return pressed;
        }
        private bool ButtonPress(PlayerIndex index, PlayerInput b)
        {
            return InputHandler.GetButtonState(index, b) == InputState.Released;
        }

    }

    class DoorAnimation
    {
        public enum AnimationType
        {
            OpenDoor,
            NormalAnimation,
            None
        }

        Vector2 lDoorPos, rDoorPos;
        TextureRegion leftDoor, rightDoor;

        public bool DoorOpen { get; private set; }
        public bool DoorClosed { get; private set; }
        public bool Done { get; private set; }

        float speed = 450;
        float timerWait = 0.3f, timer;

        Viewport viewPort;
        public AnimationType type { get; private set; }

        public DoorAnimation(Viewport viewPort)
        {
            this.viewPort = viewPort;
            initTexture();
        }

        private void initTexture()
        {
            leftDoor = ResourceManager.GetTexture("LeftDoor");
            rightDoor = ResourceManager.GetTexture("RightDoor");
        }

        public void InitOpenAnimation()
        {
            lDoorPos = Vector2.Zero;
            rDoorPos = new Vector2(viewPort.Width - rightDoor.region.Width,0);
            Done = false;
            DoorClosed = true;
            type = AnimationType.OpenDoor;
        }

        public void InitNormalAnimation()
        {
            lDoorPos = new Vector2(-leftDoor.region.Width, 0);
            rDoorPos = new Vector2(viewPort.Width, 0);
            Done = false;
            DoorClosed = false;
            DoorOpen = true;
            timer = 0;
            type = AnimationType.NormalAnimation;
        }

        public void Update(float delta)
        {
            switch (type)
            {
                case AnimationType.OpenDoor:
                    if(Done == false)
                    {
                        lDoorPos.X = lDoorPos.X + -speed * delta * 2;
                        rDoorPos.X = rDoorPos.X +  speed * delta * 2;

                        if (lDoorPos.X <= leftDoor.region.Width && rDoorPos.X >= viewPort.Width)
                            AnimationDone();
                    }
                    break;
                case AnimationType.NormalAnimation:
                    if(Done == false)
                    {
                        if (DoorOpen)
                        {
                            lDoorPos.X = lDoorPos.X + speed * delta * 2;
                            rDoorPos.X = rDoorPos.X + -speed * delta * 2.35f;

                            if (lDoorPos.X >= 0 && rDoorPos.X <= viewPort.Width - rightDoor.region.Width)
                                DoorOpen = false;
                        }
                        else
                        {
                            timer += delta;
                            if (timer > timerWait)
                                DoorClosed = true;
                        }

                        if (DoorClosed)
                        {
                            lDoorPos.X = lDoorPos.X + -speed * delta * 2;
                            rDoorPos.X = rDoorPos.X + speed * delta * 2;

                            if (lDoorPos.X <= leftDoor.region.Width && rDoorPos.X >= viewPort.Width)
                                AnimationDone();
                        }
                    }

                    break;
                default:
                    break;
            }
        }


        public void Draw(SpriteBatch SB)
        {
            SB.Draw(leftDoor, lDoorPos, Color.White);
            SB.Draw(rightDoor, rDoorPos, Color.White);
        }

        private void AnimationDone()
        {
            Done = true;
            type = AnimationType.None;
        }
    }
}