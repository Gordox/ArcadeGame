using HeroSiege.Scenes.SceneSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.GameWorld;
using HeroSiege.Render;
using HeroSiege.Tools;
using HeroSiege.InterFace.UIs.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HeroSiege.Scenes
{
    class GameScene : Scene
    {
        public bool victory, defeat;
        public World World { get; private set; }
        public WorldRender Renderer { get; private set; }
        public GameSettings GameSettings { get; private set; }

        private InGameMenu gameMenu;
        private DefeatMenu defeatMenu;

        public GameScene(GameSettings gameSettings, GraphicsDeviceArcade graphics)
            : base()
        {
            this.GameSettings = gameSettings;
            Graphics = graphics; Init();

            this.gameMenu = new InGameMenu(this);
            this.defeatMenu = new DefeatMenu(this);
        }

        public override void Init()
        {
            this.World = new World(GameSettings);
            this.Renderer = new WorldRender(GameSettings, World, Graphics);
        }

        public override void Update(float delta, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            Renderer.Update(delta);

            if (!gameMenu.MenuActive && !defeat && !victory) //Pause game if in game menu is active
                World.Update(delta);

            UpdateInGameMenu(delta);

            CheackWInDefeatState();
            UpdateDefeatVictory(delta);

            base.Update(delta, otherSceneHasFocus, coveredByOtherScene);
        }

        private void UpdateDefeatVictory(float delta)
        {
            if (defeat || victory)
            {
                if (defeat)
                    defeatMenu.Update(delta);
                World.UpdateEffects(delta);
            }
        }

        private void UpdateInGameMenu(float delta)
        {
            if (!gameMenu.MenuActive && !victory && !defeat &&
                (ButtonPress(PlayerIndex.One, PlayerInput.Start) || ButtonPress(PlayerIndex.Two, PlayerInput.Start)))
                gameMenu.MenuActive = true;

            if (gameMenu.MenuActive)
                gameMenu.Update(delta);

        }
        private void CheackWInDefeatState()
        {
            if(GameSettings.GameMode == GameMode.singlePlayer && !defeat)
            {
                if (World.PlayerOne != null && !World.PlayerOne.IsAlive)
                    defeat = true;
                else if (World.PlayerTwo != null && !World.PlayerTwo.IsAlive)
                    defeat = true;
            }
            else if (GameSettings.GameMode == GameMode.Multiplayer && !defeat)
            {
                if (!World.PlayerOne.IsAlive && 
                    !World.PlayerTwo.IsAlive)
                    defeat = true;
            }

            if (World.HeroCastleDestroyed)
                defeat = true;
        }



        public override void Draw(SpriteBatch SB)
        {
            Renderer.Draw(SB);
            if (gameMenu.MenuActive) //Draw in game menu if active
                gameMenu.Draw(SB);

            if (defeat)
                defeatMenu.Draw(SB);
        }


        public void GoToMainManu()
        {
            GameSettings.playerOne = GameSettings.playerTwo = CharacterType.None;
            StartScene startscene = new StartScene(GameSettings, Graphics);
            AddScene(startscene);
        }
        public void Restart()
        {
            this.World = new World(GameSettings);
            this.Renderer = new WorldRender(GameSettings, World, Graphics);
        }

        private bool ButtonPress(PlayerIndex index, PlayerInput b)
        {
            return InputHandler.GetButtonState(index, b) == InputState.Released;
        }
    }
}
