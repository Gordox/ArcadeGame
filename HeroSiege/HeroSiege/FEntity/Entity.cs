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

        public bool isAttaking { get; protected set; }

        List<Entity> targets;
        protected int totalTargets;

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


        public virtual void Init() { totalTargets = 10; targets = new List<Entity>(); }
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
            if (Stats.Health <= 0)
                IsAlive = false;
        }

        //----- Movment (Player only)  -----//
        public virtual void MoveUp(float delta) { velocity.Y = -Stats.Speed; }
        public virtual void MoveDown(float delta) { velocity.Y = Stats.Speed; }
        public virtual void MoveLeft(float delta) { velocity.X = -Stats.Speed; }
        public virtual void MoveRight(float delta) { velocity.X = Stats.Speed; }

        //----- Movment & Attack Animation -----//
        protected virtual void SetMovmentAnimations() { }
        protected virtual void SetAttckAnimations() { }
        protected virtual void AddSpriteAnimations() { }

        //----- Button Input (Player only) -----//
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

        public void ChangeTotalTargets(int value)
        {
            totalTargets += value;
            if (totalTargets <= 0)
                totalTargets = 1;
        }

        //----- Other -----//
        protected void GetTargets(List<Entity> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(Vector2.Distance(Position, enemies[i].Position) <= Stats.Radius)
                {
                    if (targets.Count < totalTargets)
                        targets.Add(enemies[i]);
                }
            }
        }
        
        protected void CreateProjectilesTowardsTarget(World parent, ProjectileType type)
        {
            Projectile temp = null;

            if (targets.Count > 0)
            {
                
                for (int i = 0; i < targets.Count; i++)
                {
                    switch (type)
                    {
                        case ProjectileType.Fire_Bal:
                            temp = new FireBal(ResourceManager.GetTexture("Fire_Bal"), Position.X, Position.Y, 32, 32, targets[i]);
                            break;
                        case ProjectileType.Lighing_bal:
                            break;
                        case ProjectileType.Evil_Hand:
                            break;
                        case ProjectileType.Arrow:
                            break;
                        case ProjectileType.Dark_Eye:
                            break;
                        case ProjectileType.Lightning_Axe:
                            break;
                        case ProjectileType.Normal_Axe:
                            break;
                        default:
                            break;
                    }

                    parent.GameObjects.Add(temp);
                }
                targets.Clear();
            }
            else
            {
                switch (type)
                {
                    case ProjectileType.Fire_Bal:
                        temp = new FireBal(ResourceManager.GetTexture("Fire_Bal"), Position.X, Position.Y, 32, 32, MovingDirection);
                        break;
                    case ProjectileType.Lighing_bal:
                        break;
                    case ProjectileType.Evil_Hand:
                        break;
                    case ProjectileType.Arrow:
                        break;
                    case ProjectileType.Dark_Eye:
                        break;
                    case ProjectileType.Lightning_Axe:
                        break;
                    case ProjectileType.Normal_Axe:
                        break;
                    default:
                        break;
                }

                parent.GameObjects.Add(temp);
            }
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
