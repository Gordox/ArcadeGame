using HeroSiege.FTexture2D;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject
{
    class GameObject
    {
        protected Sprite sprite;

        public bool IsAlive { get; set; }

        public Vector2 Position { get { return position; } }

        protected Rectangle boundingBox, hitBox;
        protected Vector2 position, offSetBound;

        public GameObject(TextureRegion region, float x, float y, float width, float height)
        {
            this.sprite = new Sprite(region, x, y, width, height);
            this.position = new Vector2(x, y);
            this.boundingBox = new Rectangle((int)x, (int)y, (int)width, (int)height);
            this.offSetBound = Vector2.Zero;
        }

        public virtual void Update(float delta)
        {
            sprite.SetPosition(position);
            UpdateBounds();
            sprite.Update(delta);
        }
        public void UpdateBounds()
        {
            boundingBox.X = (int)(position.X - (boundingBox.Width / 2) + offSetBound.X);
            boundingBox.Y = (int)(position.Y - (boundingBox.Height / 2) + offSetBound.Y);
        }

        public virtual void Draw(SpriteBatch SB)
        {
            sprite.Draw(SB);
        }
        public virtual void DrawBoundingBox(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("WhitePixel"), boundingBox, Color.Red);
        }
        public virtual void DrawHitBox(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("WhitePixel"), hitBox, Color.Red);
        }

        public Rectangle GenerateBar(float Current, float Max, int width, int height)
        {
            float Percent = Current / Max;
            return new Rectangle(0, 0, (int)(Percent * width), height);
        }
        public Color LerpHealthColor(float current, float max)
        {
            Color temp;
            if (current < max / 2)
                temp = Color.Lerp(Color.Red, Color.Yellow, current * 2 / max);
            else
                temp = Color.Lerp(Color.Yellow, Color.Green, (current - max / 2) * 2);

            return temp;
        }

        public Rectangle GetBounds()
        {
            return boundingBox;
        }
        public Rectangle GetHitbox()
        {
            return hitBox;
        }
        public Rectangle SetHitbox
        {
            set { hitBox = value; }
        }
        public void SetPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
        }

        public bool CheckCollision(Rectangle _this, List<GameObject> objects)
        {
            foreach (var ob in objects)
            {
                if (!ob.Equals(this))
                {                   
                    if (ob.GetBounds().Intersects(_this))
                    {
                        return true;
                    }
                }  
            }
            return false;
        }
        public bool CheckCollision(Rectangle rec, List<Rectangle> objects)
        {
            foreach (var ob in objects)
            {
                if (ob.Intersects(rec))
                {
                    return true;
                }
            }
            return false;
        }

        public bool SetPauseAnimation
        {
            get { return sprite.PauseAnimation; }
            set { sprite.PauseAnimation = value; }
        }
    }
}
