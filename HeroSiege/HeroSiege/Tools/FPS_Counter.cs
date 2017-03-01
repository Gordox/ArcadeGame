using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.Tools
{
    class FPS_Counter
    {
        private float FPS = 0f;
        private float totalTime;
        private float displayFPS;

        public FPS_Counter()
        {
            this.totalTime = 0f;
            this.displayFPS = 0f;
        }

        public void DrawFpsCount(GameTime GT, SpriteBatch SB)
        {

            float elapsed = (float)GT.ElapsedGameTime.TotalMilliseconds;
            totalTime += elapsed;

            if (totalTime >= 1000)
            {
                displayFPS = FPS;
                FPS = 0;
                totalTime = 0;
            }
            FPS++;


            SB.DrawString(ResourceManager.GetFont("Arial_Font"), "FPS: " + this.displayFPS.ToString(), new Vector2(10f, 10f), Color.WhiteSmoke, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1);
        }
    }
}
