using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.FEntity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.Tools;
using HeroSiege.FTexture2D.SpriteEffect;

namespace HeroSiege.FGameObject.Projectiles
{

    abstract class Projectile : GameObject
    {
        //----- Feilds -----//
        public bool Collision { get; set; }
        protected float timer = 0, lifeTimer;
        protected int damage;
        protected StatsData stats;
        public Direction Dir { get; protected set; }
        protected Direction oldDir;
        protected Vector2 movingDirection;
        public Entity target { get; protected set; }
        TextureRegion region;

        //----- Constructors -----//
        public Projectile(TextureRegion region, float x, float y, float width, float height, Entity target, int dmg = 0)
            : base(null, x, y, width, height)
        {
            this.target = target;
            this.region = region;
            this.IsAlive = true;
            this.damage = dmg;
            Init();
            CalculateDirection();
            InitTexture(region, Dir);
            oldDir = Dir;
        }
        public Projectile(TextureRegion region, float x, float y, float width, float height, Direction direction, int dmg = 0)
            : base(null, x, y, width, height)
        {
            Dir = direction;
            SetMovingDirection(Dir);
            InitTexture(region, Dir);
            this.region = region;
            this.IsAlive = true;
            this.damage = dmg;
            Init();
        }

        public Projectile(string animationName, FrameAnimation animation, float x, float y, float width, float height, Direction direction, int dmg = 0)
            : base(null, x, y, width, height)
        {
            Dir = direction;
            SetMovingDirection(Dir);
            this.IsAlive = true;
            this.damage = dmg;
            sprite.AddAnimation(animationName,animation).SetAnimation(animationName);
            Init();
        }
        public Projectile(string animationName, FrameAnimation animation, float x, float y, float width, float height, Entity target, int dmg = 0)
            : base(null, x, y, width, height)
        {
            this.target = target;
            this.IsAlive = true;
            this.damage = dmg;
            sprite.AddAnimation(animationName, animation).SetAnimation(animationName);
            Init();
        }
        public Projectile(string animationName, FrameAnimation animation, float width, float height, Direction direction, int dmg = 0)
            : base(null, 0, 0, width, height)
        {
            Dir = direction;
            this.IsAlive = true;
            this.damage = dmg;
            sprite.AddAnimation(animationName, animation).SetAnimation(animationName);
            Init();
        }

        //----- INIT -----//
        public void InitTexture(TextureRegion reg, Direction dir)
        {
            int regSize = reg.region.Width / 5;
            switch (dir)
            {
                case Direction.North:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.North_East:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.East:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 2, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.South_East:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 3, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.South:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 4, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.South_West:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 3, reg.region.Y, regSize, reg.region.Height));
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    break;
                case Direction.West:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 2, reg.region.Y, regSize, reg.region.Height));
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    break;
                case Direction.North_West:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize, reg.region.Y, regSize, reg.region.Height));
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }
        }
        protected virtual void Init()
        {
            InitStats();
        }
        protected abstract void InitStats();

        //----- Updates -----//
        public override void Update(float delta)
        {
            base.Update(delta);
            UpdateLifeTime(delta);
            position += movingDirection * stats.Speed * delta;
        }
        public void UpdateTextureReg(TextureRegion reg, Direction dir)
        {
            int regSize = reg.region.Width / 5;
            switch (dir)
            {
                case Direction.North:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.North_East:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.East:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 2, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.South_East:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 3, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.South:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 4, reg.region.Y, regSize, reg.region.Height));
                    break;
                case Direction.South_West:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 3, reg.region.Y, regSize, reg.region.Height));
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    break;
                case Direction.West:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize * 2, reg.region.Y, regSize, reg.region.Height));
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    break;
                case Direction.North_West:
                    sprite.SetRegion(new TextureRegion(reg.GetTexture(), reg.region.X + regSize, reg.region.Y, regSize, reg.region.Height));
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }
        }
        protected void UpdateMovingDirTowardsTarget()
        {
            movingDirection = new Vector2(target.Position.X - position.X, target.Position.Y - position.Y);
            movingDirection.Normalize();
        }
        protected void UpdateLifeTime(float delta)
        {
            timer += delta;
            if (timer > lifeTimer)
                IsAlive = false;
        }

        public bool AttackCollision()
        {
            if(target != null)
            {
                if(Vector2.Distance(Position, target.Position) < 5 && !Collision)
                {
                    return true;
                }
            }
            return false;
        } 


        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            if (DevTools.DevDebugMode || DevTools.DevDrawBoundingbox)
                DrawBoundingBox(SB);
        }

        //----- Seters -----//
        public Entity SetTarget
        {
            set { target = value; }
        }
        public void SetMovingDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    movingDirection = new Vector2(0, -1);
                    break;
                case Direction.North_East:
                    movingDirection = new Vector2(1, -1);
                    break;
                case Direction.East:
                    movingDirection = new Vector2(1, 0);
                    break;
                case Direction.South_East:
                    movingDirection = new Vector2(1, 1);
                    break;
                case Direction.South:
                    movingDirection = new Vector2(0, 1);
                    break;
                case Direction.South_West:
                    movingDirection = new Vector2(-1, 1);
                    break;
                case Direction.West:
                    movingDirection = new Vector2(-1, 0);
                    break;
                case Direction.North_West:
                    movingDirection = new Vector2(-1, -1);
                    break;
                default:
                    break;
            }
        }

        //----- Other -----//
        protected void CalculateDirection()
        {
            movingDirection = new Vector2(target.Position.X - position.X, target.Position.Y - position.Y);
            movingDirection.Normalize();
            movingDirection = RoundValue(movingDirection);

            if ((int)movingDirection.X == 0 && (int)movingDirection.Y == -1)
                Dir = Direction.North;
            else if ((int)movingDirection.X == 0 && (int)movingDirection.Y == 1)
                Dir = Direction.South;
            else if ((int)movingDirection.X == -1 && (int)movingDirection.Y == 0)
                Dir = Direction.West;
            else if ((int)movingDirection.X == 1 && (int)movingDirection.Y == 0)
                Dir = Direction.East;
            else if ((int)movingDirection.X == -1 && (int)movingDirection.Y == -1)
                Dir = Direction.North_West;
            else if ((int)movingDirection.X == 1 && (int)movingDirection.Y == -1)
                Dir = Direction.North_East;
            else if ((int)movingDirection.X == 1 && (int)movingDirection.Y == 1)
                Dir = Direction.South_East;
            else if ((int)movingDirection.X == -1 && (int)movingDirection.Y == 1)
                Dir = Direction.South_West;
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

        public abstract EffectType GetCollisionFX();

        public float GetDamage
        {
            get { return stats.Damage; }
        }


    }
}
