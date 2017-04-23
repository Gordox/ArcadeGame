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
        List<Vector2> rangeDots;

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
            this.rangeDots = calculateRangeCircle();
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
            if (olddir != MovingDirection && !isAttaking && IsAlive)
            {
                SetMovmentAnimations();
                olddir = MovingDirection;
            }

            if (isAttaking && sprite.Animations.CurrentAnimation.currentFrame >= AttackFrame && IsAlive)
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
            //// Work out the minimum step necessary using trigonometry + sine approximation.
            //double angleStep = 1f / Stats.Radius;

            //for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            //{
            //    int x = (int)Math.Round(Stats.Radius + Stats.Radius * Math.Cos(angle));
            //    int y = (int)Math.Round(Stats.Radius + Stats.Radius * Math.Sin(angle));

            //    SB.Draw(ResourceManager.GetTexture("BlackPixel"), new Vector2(position.X + x, position.Y + y), null, Color.Black, 0,
            //        new Vector2(Stats.Radius, Stats.Radius),
            //        1, SpriteEffects.None,0);
            //}

            for (int i = 0; i < rangeDots.Count; i++)
            {
                SB.Draw(ResourceManager.GetTexture("BlackPixel"), new Vector2(position.X + rangeDots[i].X, position.Y + rangeDots[i].Y), null, Color.Black, 0,
                    new Vector2(Stats.Radius, Stats.Radius),
                    1, SpriteEffects.None, 0);
            }


        }
        private List<Vector2> calculateRangeCircle()
        {
            List<Vector2> temp = new List<Vector2>();
            double angleStep = 1f / Stats.Radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                int x = (int)Math.Round(Stats.Radius + Stats.Radius * Math.Cos(angle));
                int y = (int)Math.Round(Stats.Radius + Stats.Radius * Math.Sin(angle));

                temp.Add(new Vector2(x, y));
            }
            return temp;
        }

        //----- Other -----//
        private void CheckIsAlive()
        {
            if (Stats.Health <= 0 && IsAlive)
            {
                IsAlive = false;
                Death();
            }
            if (Stats.Health <= 0)
                Stats.Health = 0;
        }
        public void CalculateDirection(Entity target)
        {

           var movingDirection = new Vector2(target.Position.X - position.X, target.Position.Y - position.Y);
            movingDirection.Normalize();
            movingDirection = RoundValue(movingDirection);

            if ((int)movingDirection.X == 0 && (int)movingDirection.Y == -1)
                MovingDirection = Direction.North;
            else if ((int)movingDirection.X == 0 && (int)movingDirection.Y == 1)
                MovingDirection = Direction.South;
            else if ((int)movingDirection.X == -1 && (int)movingDirection.Y == 0)
                MovingDirection = Direction.West;
            else if ((int)movingDirection.X == 1 && (int)movingDirection.Y == 0)
                MovingDirection = Direction.East;
            else if ((int)movingDirection.X == -1 && (int)movingDirection.Y == -1)
                MovingDirection = Direction.North_West;
            else if ((int)movingDirection.X == 1 && (int)movingDirection.Y == -1)
                MovingDirection = Direction.North_East;
            else if ((int)movingDirection.X == 1 && (int)movingDirection.Y == 1)
                MovingDirection = Direction.South_East;
            else if ((int)movingDirection.X == -1 && (int)movingDirection.Y == 1)
                MovingDirection = Direction.South_West;
        }
        protected Vector2 RoundValue(Vector2 vector)
        {
            int tempX = 0;
            int tempY = 0;

            if (vector.X > 0)
            {
                if (vector.X - (int)vector.X >= 0.5)
                    tempX = (int)Math.Ceiling(vector.X);
                else
                    tempX = (int)Math.Floor(vector.X);
            }
            else
            {
                if (vector.X - (int)vector.X >= 0.5)
                    tempX = (int)Math.Ceiling(vector.X);
                else
                    tempX = (int)Math.Floor(vector.X);
            }

            if (vector.Y > 0)
            {
                if ((int)vector.Y - vector.Y >= 0.5)
                    tempY = (int)Math.Floor(vector.Y);
                else
                    tempY = (int)Math.Ceiling(vector.Y);
            }
            else
            {
                if ((int)vector.Y - vector.Y >= 0.5)
                    tempY = (int)Math.Floor(vector.Y);
                else
                    tempY = (int)Math.Ceiling(vector.Y);
            }

            return new Vector2(tempX, tempY);
        }
        public void ResetAnimation()
        {
            sprite.Animations.CurrentAnimation.ResetAnimation();
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
