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

            World.Map.DrawMapTexture(SB);

            if(World.PlayerOne != null)
                World.PlayerOne.Draw(SB);
            if (World.PlayerTwo != null)
                World.PlayerTwo.Draw(SB);

            //Debug
            DebugHitboxesDraw(SB);


            SB.End();


            //UI
            SB.Begin();

            DebugTextDraw(SB);

            SB.End();
        }

        private void DebugTextDraw(SpriteBatch SB)
        {
           
            if (DevTools.DevDebugMode)
            {
                //----- PLayer ONE Direcion -----//
                if (DevTools.DevDebugMode)
                {
                    SB.DrawString(ResourceManager.GetFont("Arial_Font"), "" + World.PlayerOne.MovingDirection, new Vector2(50, 0), Color.Black);
                }


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
