using HeroSiege.FEntity;
using HeroSiege.FEntity.Buildings;
using HeroSiege.FGameObject;
using HeroSiege.GameWorld;
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
        public Camera2D Camera { get; private set; }
        public World World { get; private set; }
        private GraphicsDeviceArcade graphicsDev;

        public WorldRender(World world, GraphicsDeviceArcade graphicsDev)
        {
            this.Camera = new Camera2D(graphicsDev.Viewport);
            this.World = world;
            this.graphicsDev = graphicsDev;
        }

        //----- Updates -----//
        public void Update(float delta)
        {
            CameraUpdate();
        }

        private void CameraUpdate()
        {
            Camera.Position = World.PlayerOne.Position;
            Camera.Update();
        }

        //----- Draws -----//
        public void Draw(SpriteBatch SB)
        {
            //World
            SB.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null, null, null,
                Camera.Transform);

            //--- World map ---//
            World.Map.DrawMapTexture(SB);

            //--- Debug Wakeble tiles ---//
            if (DevTools.DevDebugMode || DevTools.DevDrawWalkbleTiles)
                World.Map.DrawWakebleTiles(SB);

            //Buildings
            DrawAllBuildings(SB);

            //GameObjects
            DrawAllGameObjects(SB);

            //Enemies
            DrawEnemies(SB);

            //PLayers
            DrawPlayers(SB);

            //Debug
            DebugHitboxesDraw(SB);

            SB.End();


            //UI
            SB.Begin();

            
            DebugTextDraw(SB);

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
            foreach (Entity e in World.Enimies)
            {
                if (e != null)
                    e.Draw(SB);
            }
        }

        private void DrawAllBuildings(SpriteBatch SB)
        {
            foreach (Building b in World.HeroBuildings)
            {
                b.Draw(SB);
            }

            foreach (Building b in World.EnemyBuildings)
            {
                b.Draw(SB);
            }
        }
        private void DrawAllGameObjects(SpriteBatch SB)
        {
            foreach(GameObject obj in World.GameObjects)
            {
                if(obj != null)
                    obj.Draw(SB);
            }
        }

        //----- Debug draw -----//
        private void DebugTextDraw(SpriteBatch SB)
        {
           
            if (DevTools.DevDebugMode)
            {
                SB.Draw(ResourceManager.GetTexture("WhitePixel"), new Rectangle(0, 0, 1920, 60), Color.Gray);

                //----- PLayer ONE Direcion -----//
                if (World.PlayerOne != null)
                    SB.DrawString(ResourceManager.GetFont("Arial_Font"), "P_1: " + World.PlayerOne.MovingDirection, new Vector2(0, 0), Color.Black);

                //----- PLayer TWO Direcion -----//
                if (World.PlayerTwo != null)
                    SB.DrawString(ResourceManager.GetFont("Arial_Font"), "P_2: " + World.PlayerTwo.MovingDirection, new Vector2(0, 30), Color.Black);


                //----- Camera pos -----//
                SB.DrawString(ResourceManager.GetFont("Arial_Font"), "Camera: " + Camera.Position, new Vector2(1600/2, 0), Color.Black);


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
