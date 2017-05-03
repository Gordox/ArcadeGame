using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroSiege.Manager;

namespace HeroSiege.InterFace.UIs
{
    abstract class UI : IUI
    {
        public virtual void Draw(SpriteBatch SB)
        {           
        }
       
        public virtual void Update(float delta)
        {
        }

        //----- General draw functions -----//
        //Draw bars
        protected Rectangle GenerateBar(float Current, float Max, int width, int height)
        {
            float Percent = Current / Max;
            return new Rectangle(0, 0, (int)(Percent * width), height);
        }
        protected Rectangle GenerateBar(float Current, float Max, Rectangle region)
        {
            float Percent = Current / Max;
            return new Rectangle(0, 0, (int)(Percent * region.Width), region.Height);
        }

        //Draw strings
        protected void DrawCenterString(SpriteBatch SB, SpriteFont font, string text, Vector2 pos, Color color)
        {
            Vector2 orgin = font.MeasureString(text);
            SB.DrawString(font, text, pos, color, 0, new Vector2(orgin.X / 2, orgin.Y / 2), 1, SpriteEffects.None, 1);
        }
        protected void DrawCenterString(SpriteBatch SB, SpriteFont font, string text, Vector2 pos, Color color, float size)
        {
            Vector2 orgin = font.MeasureString(text);
            SB.DrawString(font, text, pos, color, 0, new Vector2(orgin.X / 2, orgin.Y / 2), size, SpriteEffects.None, 1);
        }
        protected void DrawString(SpriteBatch SB, SpriteFont font, string text, Vector2 pos, Color color, float size)
        {
            SB.DrawString(font, text, pos, color, 0, new Vector2(0, 0), size, SpriteEffects.None, 1);
        }
    }
}
