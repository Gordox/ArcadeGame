using HeroSiege.FGameObject;
using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeroSiege.FEntity.Controllers;
using HeroSiege.GameWorld;

namespace HeroSiege.FEntity
{
    public enum Direction
    {
        North,
        North_East,
        East,
        South_East,
        South,
        South_West,
        West,
        North_West
    }

    class Entity : GameObject
    {
        public Control Control { get; protected set; }

        public StatsData Stats { get; protected set; }

        public Vector2 velocity;

        public Direction MovingDirection { get; set; }

        public bool IsAlive { get; set; }

        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.IsAlive = true;
            velocity = Vector2.Zero;
        }


        //----- Updates-----//
        public override void Update(float delta)
        {
            base.Update(delta);
            CheckIsAlive();

            if (Control != null)
                Control.Update(delta);

        }

        public void UpdatePlayerMovement(float delta, List<Rectangle> objects)
        {
            velocity = Vector2.Zero;

            if(Control != null)
                ((HumanControler)Control).UpdateJoystick(delta);

            int futureposX = (int)(position.X + velocity.X * delta);
            int futureposY = (int)(position.Y + velocity.Y * delta);

            if (!CheckCollision(new Rectangle(futureposX, (int)position.Y, this.GetBounds().Width, this.GetBounds().Height), objects))
                position.X += velocity.X * delta;
                
            if (!CheckCollision(new Rectangle((int)position.X, futureposY, this.GetBounds().Width, this.GetBounds().Height), objects))
                position.Y += velocity.Y * delta;
        }

        private void UpdateRotation() //NOT DONE //Might not use
        {
            Vector2 rotation = new Vector2(velocity.X, velocity.Y);
            rotation.Normalize();
            if (velocity.Length() > 0.0001f)
                sprite.Rotation = (float)Math.Atan2(rotation.Y, rotation.X);
        }



        private void CheckIsAlive()
        {
            if (Stats.Health <= 0)
                IsAlive = false;
        }

        protected virtual void InitStats() { }

        //----- Movment -----//
        public virtual void MoveUp(float delta) { velocity.Y = -Stats.Speed; }
        public virtual void MoveDown(float delta) { velocity.Y = Stats.Speed; }
        public virtual void MoveLeft(float delta) { velocity.X = -Stats.Speed; }
        public virtual void MoveRight(float delta) { velocity.X = Stats.Speed; }

        //----- Button Input -----//
        public virtual void GreenButton(World parent) { } //Key G or numpad 4
        public virtual void BlueButton(World parent) { } //Key H or numpad 5
        public virtual void YellowButton(World parent) { }  //Key B or numpad 1
        public virtual void RedButton(World parent) { } //Key N or numpad 2
        public virtual void AButton(World parent) { } //Key M or numpad 3
        public virtual void BButton(World parent) { }  //Key J or numpad 6


        public void SetControl(Control control)
        {
            this.Control = control;
        }

        public bool SetPauseAnimation
        {
            get { return sprite.PauseAnimation; }
            set { sprite.PauseAnimation = value; }
        }
    }
}
