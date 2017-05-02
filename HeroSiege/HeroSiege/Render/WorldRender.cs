using HeroSiege.FEntity;
using HeroSiege.FEntity.Buildings;
using HeroSiege.FGameObject;
using HeroSiege.GameWorld;
using HeroSiege.InterFace.GUI;
using HeroSiege.Manager;
using HeroSiege.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Render
{
    class WorldRender
    {
        GameSettings settings;

        public World World { get; private set; }
        private GraphicsDeviceArcade graphicsDev;
        private HUD playerOneHUD, playerTwoHUD;
        private ShopWindow plOneShopWindow, plTwoShopWindow;
        public Camera2D Camera { get; private set; }
        public Camera2D PlayerOneCamera { get; private set; }
        public Camera2D PlayerTwoCamera { get; private set; }

        private Viewport singlePlayerView, playerOneView, playerTwoView;

        public WorldRender(GameSettings gameSettings, World world, GraphicsDeviceArcade graphicsDev)
        {
            this.settings = gameSettings;
            this.World = world;
            this.graphicsDev = graphicsDev;

            InitCameraAndViewPorts(graphicsDev);
            InitHUDwindows();
        }

        //----- Initiator -----//
        private void InitCameraAndViewPorts(GraphicsDeviceArcade graphicsDev)
        {
            switch (settings.GameMode)
            {
                case GameMode.singlePlayer:
                    //Init view
                    singlePlayerView = graphicsDev.Viewport;
                    this.Camera = new Camera2D(singlePlayerView, settings);
                    //Init camera
                    break;
                case GameMode.Multiplayer:
                    //Init view
                    singlePlayerView = graphicsDev.Viewport;
                    playerOneView = singlePlayerView;
                    playerTwoView = singlePlayerView;

                    playerOneView.Width = playerOneView.Width / 2;
                    playerTwoView.Width = playerTwoView.Width / 2;
                    playerTwoView.X = playerOneView.Width;

                    //Init camera
                    this.PlayerOneCamera = new Camera2D(playerOneView, settings);
                    this.PlayerTwoCamera = new Camera2D(playerTwoView, settings);
                    break;
                default:
                    break;
            }
        }
        private void InitHUDwindows()
        {
            switch (settings.GameMode)
            {
                case GameMode.singlePlayer:
                    if (World.PlayerOne != null)
                    {
                        playerOneHUD = new HUD(World.gameSettings, singlePlayerView, World.PlayerOne, PlayerIndex.One);
                        plOneShopWindow = new ShopWindow(World.gameSettings, singlePlayerView, World.PlayerOne, PlayerIndex.One);
                    }
                    if (World.PlayerTwo != null)
                    {
                        playerOneHUD = new HUD(World.gameSettings, singlePlayerView, World.PlayerTwo, PlayerIndex.Two);
                        plOneShopWindow = new ShopWindow(World.gameSettings, singlePlayerView, World.PlayerTwo, PlayerIndex.Two);
                    }
                    break;
                case GameMode.Multiplayer:
                    if (World.PlayerOne != null)
                    {
                        playerOneHUD = new HUD(World.gameSettings, playerOneView, World.PlayerOne, PlayerIndex.One);
                        plOneShopWindow = new ShopWindow(World.gameSettings, playerOneView, World.PlayerOne, PlayerIndex.One);
                    }
                    if (World.PlayerTwo != null)
                    {
                        playerTwoHUD = new HUD(World.gameSettings, playerTwoView, World.PlayerTwo, PlayerIndex.Two);
                        plTwoShopWindow = new ShopWindow(World.gameSettings, playerTwoView, World.PlayerTwo, PlayerIndex.Two);
                    }
                    break;
                default:
                    break;
            }
        }

        //----- Updates -----//
        public void Update(float delta)
        {
            CameraUpdate();

            if (plOneShopWindow != null)
                plOneShopWindow.Update(delta);
            if (plTwoShopWindow != null)
                plTwoShopWindow.Update(delta);
        }

        private void CameraUpdate()
        {
            switch (settings.GameMode)
            {
                case GameMode.singlePlayer:
                    if(World.PlayerOne != null)
                        Camera.Position = World.PlayerOne.Position;
                    if (World.PlayerTwo != null)
                        Camera.Position = World.PlayerTwo.Position;
                    Camera.Update();
                    break;
                case GameMode.Multiplayer:
                    PlayerOneCamera.Position = World.PlayerOne.Position;
                    PlayerTwoCamera.Position = World.PlayerTwo.Position;

                    PlayerOneCamera.Update();
                    PlayerTwoCamera.Update();
                    break;
                default:
                    break;
            }
            
        }

        //----- Draws -----//
        public void Draw(SpriteBatch SB)
        {

            switch (settings.GameMode)
            {
                case GameMode.singlePlayer:                 
                    DrawScenne(SB, Camera);
                    break;
                case GameMode.Multiplayer:
                    graphicsDev.Viewport = playerOneView;
                    DrawScenne(SB, PlayerOneCamera);

                    graphicsDev.Viewport = playerTwoView;
                    DrawScenne(SB, PlayerTwoCamera);

                    graphicsDev.Viewport = singlePlayerView;
                    break;
                default:
                    break;
            }

            //UI
            SB.Begin();

            switch (settings.GameMode)
            {
                case GameMode.singlePlayer:
                    playerOneHUD.Draw(SB);
                    if ((World.PlayerOne != null && World.PlayerOne.isBuying) ||
                        (World.PlayerTwo != null && World.PlayerTwo.isBuying))
                        plOneShopWindow.Draw(SB);
                    break;
                case GameMode.Multiplayer:
                    playerOneHUD.Draw(SB);
                    playerTwoHUD.Draw(SB);

                    if (World.PlayerOne.isBuying)
                        plOneShopWindow.Draw(SB);
                    if (World.PlayerTwo.isBuying)
                        plTwoShopWindow.Draw(SB);

                    break;
                default:
                    break;
            }

            DebugTextDraw(SB);

            SB.End();
        }

        private void DrawScenne(SpriteBatch SB, Camera2D camera)
        {
            //World
            SB.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null, null, null,
                camera.Transform);

            //--- World map ---//
            World.Map.DrawMapTexture(SB);

            //--- Debug Wakeble tiles ---//
            if (DevTools.DevDebugMode || DevTools.DevDrawWalkbleTiles)
                World.Map.DrawWakebleTiles(SB);

            //Buildings
            DrawAllBuildings(SB);

            //Enemies
            DrawEnemies(SB);

            //GameObjects
            DrawAllGameObjects(SB);

            //PLayers
            DrawPlayers(SB);

            DrawEffects(SB);

            //Debug
            DebugHitboxesDraw(SB);

            SB.End();

        }


        private void DrawPlayers(SpriteBatch SB)
        {
            if (World.PlayerOne != null)
                World.PlayerOne.Draw(SB);
            if (World.PlayerTwo != null)
                World.PlayerTwo.Draw(SB);
        }
        private void DrawEnemies(SpriteBatch SB)
        {
            foreach (Entity e in World.Enemies)
            {
                if (e != null)
                    e.Draw(SB);
            }

            foreach (Entity e in World.EnemieBosses)
            {
                if (e != null)
                    e.Draw(SB);
            }
        }

        private void DrawAllBuildings(SpriteBatch SB)
        {
            foreach (Building b in World.HeroBuildings)
                b.Draw(SB);

            foreach (Building b in World.EnemyBuildings)
                b.Draw(SB);

            foreach (Building b in World.GeneralBuildings)
                b.Draw(SB);
        }
        private void DrawAllGameObjects(SpriteBatch SB)
        {
            foreach(GameObject obj in World.GameObjects)
            {
                if(obj != null)
                    obj.Draw(SB);
            }

            foreach (GameObject obj in World.EnemyObjects)
            {
                if (obj != null)
                    obj.Draw(SB);
            }
        }
   
        private void DrawEffects(SpriteBatch SB)
        {
            foreach (var obj in World.Effects)
                obj.Draw(SB);
        }

        //----- Debug draw -----//
        private void DebugTextDraw(SpriteBatch SB)
        {
           
            if (DevTools.DevDebugMode || DevTools.DevTopBarInfo)
            {
                SB.Draw(ResourceManager.GetTexture("WhitePixel"), new Rectangle(0, 0, 1920, 60), Color.Gray);

                //----- PLayer ONE INFO -----//
                if (World.PlayerOne != null)
                {
                    Entity tempPlayer = World.PlayerOne;
                    string textDir = "P_1: " + tempPlayer.MovingDirection;
                    string textStats = "Health: " + tempPlayer.Stats.Health +" Mana: "+ tempPlayer.Stats.Mana;
                    string textGold = "Gold: " + World.PlayerOne.GetGold;
                    SB.DrawString(ResourceManager.GetFont("Arial_Font"), textDir + "\n"+ textStats + "  " + textGold, new Vector2(0, 2), Color.Black);
                }
                //----- PLayer TWO INFO -----//
                if (World.PlayerTwo != null)
                {
                    Entity tempPlayer = World.PlayerTwo;
                    string textDir = "P_2: " + tempPlayer.MovingDirection;
                    string textStats = "Health: " + tempPlayer.Stats.Health + " Mana: " + tempPlayer.Stats.Mana;
                    string textGold = "Gold: " + World.PlayerTwo.GetGold;
                    SB.DrawString(ResourceManager.GetFont("Arial_Font"), textDir + "\n" + textStats + "  "+ textGold, new Vector2(350, 2), Color.Black);
                }


                //----- Version Build -----//
                SB.DrawString(ResourceManager.GetFont("Arial_Font"), "Version: Alpha 2.0 ", new Vector2(1600 / 2, 0), Color.Black);

                //----- Camera pos -----//
                if (Camera != null)
                    SB.DrawString(ResourceManager.GetFont("Arial_Font"), "Camera: " + Camera.Position, new Vector2(1600/2, 0), Color.Black);

                //if (PlayerOneCamera != null)
                //    SB.DrawString(ResourceManager.GetFont("Arial_Font"), "Pl_1 Camera: " + PlayerOneCamera.Position, new Vector2(1600 / 6 * 2, 0), Color.Black);

                //if (PlayerTwoCamera != null)
                //    SB.DrawString(ResourceManager.GetFont("Arial_Font"), "Pl_2 Camera: " + PlayerTwoCamera.Position, new Vector2(1600 / 6 * 4, 0), Color.Black);


            }
        }
        private void DebugHitboxesDraw(SpriteBatch SB)
        {

            if (DevTools.DevDebugMode)
            {
                //----- Hitboxes -----//
                foreach (Rectangle r in World.Map.Hitboxes)
                {
                    SB.Draw(ResourceManager.GetTexture("BaseLayer", 250), new Rectangle(r.X - World.Map.TileSize / 2, r.Y - World.Map.TileSize / 2, r.Width, r.Height), Color.Red);
                }

            }
        }


    }
}
