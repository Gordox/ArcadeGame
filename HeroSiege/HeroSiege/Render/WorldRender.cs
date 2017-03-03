using HeroSiege.GameWorld;
using HeroSiege.Tools;
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
            ////World
            SB.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null, null, null,
                Camera.Transform);


            World.Map.DrawMapTexture(SB);
            World.PlayerOne.Draw(SB);

            SB.End();

            //UI
            SB.Begin();
            SB.End();
        }
    }
}
