using HeroSiege.FGameObject;
using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeroSiege.FEntity
{
    class Entity : GameObject
    {
        //public Control Control { get; protected set; }
        public StatsData Stats { get; protected set; }

        public Vector2 velocity { get; protected set; }

        protected bool IsAlive { get; set; }

        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.IsAlive = true;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            CheckIsAlive();

           /*
            * if (Stats.Speed < 1.0f)
                 sprite.PauseAnimation = true;
            else
                sprite.PauseAnimation = false;
           */
        }
        public void UpdatePlayerMovement(float delta, List<GameObject> objects)
        {
            if (!CheckCollision(objects))
            {

            }
        }

        /*
        public void UpdateMovement(float delta)
        {
            // Keeps entity within map boundaries
            if (velocity.Length() >= Globals.MAX_SPEED)
            {
                var vel = new Vector2(velocity.X, velocity.Y);
                vel.Normalize();

                velocity.X = vel.X * Globals.MAX_SPEED;
                velocity.Y = vel.Y * Globals.MAX_SPEED; //MathHelper.Clamp(velocity.Y, -Globals.MAX_SPEED, Globals.MAX_SPEED);
            }

            position += velocity * delta;
            position.X = MathHelper.Clamp(position.X, 500, 2650);
            position.Y = MathHelper.Clamp(position.Y, 500, 2200);

            // Check collision
            if (Control == null)
                return;
            List<Rectangle> hitboxes = Control.scene.Hitboxes;
            for (int i = 0; i < hitboxes.Count; i++)
            {
                if (this.Hitbox.Intersects(hitboxes[i]))
                {
                    position = prePosition - preVelocity * delta * 2;
                    velocity = new Vector2();
                }
            }
        }
        */

        private void UpdateRotation() //NOT DONE
        {
            Vector2 rotation = new Vector2(velocity.X, velocity.Y);
            rotation.Normalize();
            if (velocity.Length() > 0.0001f)
                sprite.Rotation = (float)Math.Atan2(rotation.Y, rotation.X);// +Globals.ROT_OFFSET + rotationOffset;
        }
        private void CheckIsAlive()
        {
            if (Stats.Health <= 0)
                IsAlive = false;
        }

        
    }
}
