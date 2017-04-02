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
        //----- Feilds -----//
        public Control Control { get; protected set; }

        public StatsData Stats { get; protected set; }

        public Vector2 velocity;

        public Direction MovingDirection { get; set; }
        private Direction olddir;

        public bool IsAlive { get; set; }
        public bool isAttaking { get; protected set; }

        protected int AttackFrame; //Which frame the attack shall be used

        //----- Constructor -----//
        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.IsAlive = true;
            this.isAttaking = false;
            this.velocity = Vector2.Zero;
            this.olddir = MovingDirection;
        }

        protected virtual void InitStats() { }

        //----- Updates-----//
        public override void Update(float delta)
        {
            base.Update(delta);
            CheckIsAlive();

            if (Control != null && IsAlive)
                Control.Update(delta);
        }

        public void UpdatePlayerMovement(float delta, List<Rectangle> objects)
        {
            velocity = Vector2.Zero;

            if (Control != null && IsAlive)
                ((HumanControler)Control).UpdateJoystick(delta);
            
            //Calculate future pos
            Vector2 futurePos = (position + velocity * delta);

            if (MovingDirection == Direction.North_East || MovingDirection == Direction.North_West ||
                MovingDirection == Direction.South_East || MovingDirection == Direction.South_West)
                velocity *= 0.75f; //hard coded value so the player moves at the same speed side ways somewhat 

            //Update movement if no collision will happen
            if (!CheckCollision(new Rectangle((int)futurePos.X, (int)position.Y, this.GetBounds().Width, this.GetBounds().Height), objects))
                position.X += velocity.X * delta;
                
            if (!CheckCollision(new Rectangle((int)position.X, (int)futurePos.Y, this.GetBounds().Width, this.GetBounds().Height), objects))
                position.Y += velocity.Y * delta;
        }

        protected void UpdateAnimation()
        {
            if (olddir != MovingDirection && !isAttaking)
            {
                SetMovmentAnimations();
                olddir = MovingDirection;
            }

            if (isAttaking && sprite.Animations.CurrentAnimation.currentFrame == AttackFrame)
            {
                isAttaking = false;
                SetMovmentAnimations();
            }
        }

        //----- NAME HERE -----//
        private void CheckIsAlive()
        {
            if (Stats.Health <= 0)
                IsAlive = false;
        }

        //----- Movment -----//
        public virtual void MoveUp(float delta) { velocity.Y = -Stats.Speed; }
        public virtual void MoveDown(float delta) { velocity.Y = Stats.Speed; }
        public virtual void MoveLeft(float delta) { velocity.X = -Stats.Speed; }
        public virtual void MoveRight(float delta) { velocity.X = Stats.Speed; }

        //----- Movment & Attack Animation -----//
        protected virtual void SetMovmentAnimations() { }
        protected virtual void SetAttckAnimations() { }

        //----- Button Input -----//
        public virtual void GreenButton(World parent) { } //Key G or numpad 4
        public virtual void BlueButton(World parent) { } //Key H or numpad 5
        public virtual void YellowButton(World parent) { }  //Key B or numpad 1
        public virtual void RedButton(World parent) { } //Key N or numpad 2
        public virtual void AButton(World parent) { } //Key M or numpad 3
        public virtual void BButton(World parent) { }  //Key J or numpad 6

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

        public bool SetPauseAnimation
        {
            get { return sprite.PauseAnimation; }
            set { sprite.PauseAnimation = value; }
        }

        //----- Other -----//
        public void Hit(float damage)
        {
            Stats.Health = Stats.Health - (damage - (damage * (Stats.Armor / 1000)));
        }
    }
}
