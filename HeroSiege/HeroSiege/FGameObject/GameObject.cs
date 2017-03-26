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

        public Vector2 Position { get { return position; } }
        //public Vector2 Direction { get { return direction; } }

        protected Rectangle boundingBox, hitBox;
        protected Vector2 position, direction, offSetBound;


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

        public void UpdateBounds()
        {
            boundingBox.X = (int)(position.X - (boundingBox.Width / 2) + offSetBound.X);
            boundingBox.Y = (int)(position.Y - (boundingBox.Height / 2) + offSetBound.Y);
        }

        public void SetPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
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

    }
}
