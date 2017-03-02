using HeroSiege.GameWorld;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Render
{
    class WorldRender
    {
        public World World { get; private set; }
        private GraphicsDeviceArcade grafics;

        public WorldRender(World world, GraphicsDeviceArcade grafics)
        {
            this.World = world;
            this.grafics = grafics;
        }

        public void Update(float delta)
        {
          
        }


        public void Draw(SpriteBatch SB)
        {
            ////World
            //SB.Begin(SpriteSortMode.Deferred,
            //    BlendState.AlphaBlend,
            //    SamplerState.LinearClamp,
            //    null, null, null,
            //    Camera.Transform);



            ////Test
            //World.testMap.DrawMapTexture(SB);


            //SB.End();

            SB.Begin();
            World.Map.DrawMapTexture(SB);
            SB.End();
        }
    }
}
