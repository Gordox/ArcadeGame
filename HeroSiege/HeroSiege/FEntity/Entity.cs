using HeroSiege.FGameObject;
using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeroSiege.FEntity.Controllers;
using HeroSiege.GameWorld;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.Tools;
using HeroSiege.FGameObject.Projectiles;
using HeroSiege.Manager;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.FEntity.Enemies;

namespace HeroSiege.FEntity
{
    class Entity : GameObject
    {
        //----- Feilds -----//
        public Control Control { get; protected set; }

        public StatsData Stats { get; protected set; }

        public Vector2 velocity, projectileOffset;

        public Direction MovingDirection { get; set; }
        private Direction olddir;

        public bool isAttaking { get; set; }
 
        protected int AttackFrame; //Which frame the attack shall be used

        //----- Constructor -----//
        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.IsAlive = true;
            this.isAttaking = false;
            this.velocity = Vector2.Zero;
            this.olddir = MovingDirection;
            Init();
        }


        public virtual void Init() { InitStats(); }

        protected virtual void InitStats() { }

        //----- Updates-----//
        public override void Update(float delta)
        {
            base.Update(delta);
            CheckIsAlive();

            if (Control != null && IsAlive)
                Control.Update(delta);
        }
        protected void UpdateAnimation()
        {
            if (olddir != MovingDirection && !isAttaking)
            {
                SetMovmentAnimations();
                olddir = MovingDirection;
            }

            if (isAttaking && sprite.Animations.CurrentAnimation.currentFrame >= AttackFrame)
            {
                isAttaking = false;
                SetMovmentAnimations();
            }
        }
        protected virtual void UpdateMovmentDirection() { }

        //----- Draw -----//
        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            if (DevTools.DevDebugMode || DevTools.DevDrawBoundingbox)
                DrawBoundingBox(SB);

            if (DevTools.DevDebugMode || DevTools.DevDrawRange && Stats.Radius > 0)
                DrawRange(SB);

        }
        public void DrawRange(SpriteBatch SB)
        {
            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / Stats.Radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                int x = (int)Math.Round(Stats.Radius + Stats.Radius * Math.Cos(angle));
                int y = (int)Math.Round(Stats.Radius + Stats.Radius * Math.Sin(angle));

                SB.Draw(ResourceManager.GetTexture("BlackPixel"), new Vector2(position.X + x, position.Y + y), null, Color.Black, 0,
                    new Vector2(Stats.Radius, Stats.Radius),
                    1, SpriteEffects.None,0);
            }


        }

        //----- NAME HERE -----//
        private void CheckIsAlive()
        {
            if (Stats.Health <= 0 && IsAlive)
            {
                IsAlive = false;
                Death();
            }
        }

        //----- Movment & Attack Animation -----//
        protected virtual void SetMovmentAnimations() { }
        protected virtual void SetAttckAnimations() { }
        protected virtual void AddSpriteAnimations() { }

        //----- Death -----//
        protected virtual void Death()
        {
            SetPauseAnimation = false;
        }

        //----- Setter and getter -----//
        public void SetControl(Control control)
        {
            this.Control = control;
        }


        public void Hit(float damage)
        {
            Stats.Health = Stats.Health - (damage - (damage * (Stats.Armor / 1000)));
        }
    }
}


/* How to crate a circle texture
 public Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(new GraphicsDevice, 1, 1);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.Black;
            }

            texture.SetData(data);
            return texture;
        }  
     */
