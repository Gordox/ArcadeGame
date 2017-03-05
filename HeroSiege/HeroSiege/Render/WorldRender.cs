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

        public void Update(float delta)
        {
            CameraUpdate();
        }

        private void CameraUpdate()
        {
            Camera.Position = World.PlayerOne.Position;
            Camera.Update();
        }

        public void Draw(SpriteBatch SB)
        {
            //World
            SB.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null, null, null,
                Camera.Transform);


            World.Map.DrawMapTexture(SB);
            World.PlayerOne.Draw(SB);

            //Debug draw
            if (DevTools.DevDebugMode)
            {
                foreach (Rectangle r in World.Map.Hitboxes)
                {
                    SB.Draw(ResourceManager.GetTexture("BaseLayer", 250), r, Color.Red);
                }

            }
            
            SB.End();

            //UI
            SB.Begin();




            //Debug draw Text
            if (DevTools.DevDebugMode)
            {
                SB.DrawString(ResourceManager.GetFont("Arial_Font"), "" + World.PlayerOne.MovingDirection, new Vector2(50, 0), Color.Black);
            }
            SB.End();
        }
    }
}
