using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.GameWorld;
using Microsoft.Xna.Framework;
using HeroSiege.FGameObject;
using HeroSiege.FEntity.Controllers;
using HeroSiege.FGameObject.Projectiles;
using HeroSiege.Manager;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.FEntity.Buildings;

namespace HeroSiege.FEntity.Players
{
    abstract class Hero : Entity
    {
        const float START_XP = 100;
        const int START_GOLD = 300;
        public string HeroName { get; protected set; }
        private int gold;

        public List<Entity> Targets { get; protected set; }
        protected int totalTargets;
        public bool isBuying { get; set; }
        public Hero(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
        }

        public override void Init()
        {
            base.Init();
            isBuying = false;
            totalTargets = 1;
            Targets = new List<Entity>();
        }
        protected override void InitStats()
        {
            base.InitStats();
            Stats = new StatsData();
            Stats.Level = 1;
            Stats.MaxXP = START_XP;
            Stats.XP = 0;
        }

        //----- Updates-----//
        public override void Update(float delta)
        {
            base.Update(delta);
            UpdateAnimation();
            CheckXP();
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

        //----- XP handeling -----//
        protected void LevelUP()
        {
            Stats.Level++;
            Stats.MaxXP = Stats.MaxXP + Stats.MaxXP / 2;
            Stats.XP = 0;
        }
        protected void CheckXP()
        {
            if (Stats.XP >= Stats.MaxXP)
                LevelUP();
        }
        public void IncreaseXP(int xpValue) { Stats.XP += xpValue; }
        public void IncreaseGold(int gold) { this.gold += gold; }

        //----- Shoping -----//


        //----- Inventory -----//


        //----- Movment -----//
        public virtual void MoveUp(float delta)    { velocity.Y = -Stats.Speed; }
        public virtual void MoveDown(float delta)  { velocity.Y = Stats.Speed; }
        public virtual void MoveLeft(float delta)  { velocity.X = -Stats.Speed; }
        public virtual void MoveRight(float delta) { velocity.X = Stats.Speed; }

        //----- Button Input -----//
        public virtual void GreenButton(World parent)  { } //Key G or numpad 4
        public virtual void BlueButton(World parent)   { } //Key H or numpad 5
        public virtual void YellowButton(World parent) { } //Key B or numpad 1
        public virtual void RedButton(World parent)    { } //Key N or numpad 2
        public virtual void AButton(World parent) { } //Key M or numpad 3
        public virtual void BButton(World parent) { }  //Key J or numpad 6

        //----- Attacking method and functions -----//
        public void ChangeTotalTargets(int value)
        {
            totalTargets += value;
            if (totalTargets <= 0)
                totalTargets = 1;
        }
        public void GetTargets(List<Entity> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector2.Distance(Position, enemies[i].Position) <= Stats.Radius)
                {
                    if (Targets.Count < totalTargets)
                        Targets.Add(enemies[i]);
                }
            }
        }
        public void GetAllTargets(List<Entity> enemies, List<Building> enemyB)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector2.Distance(Position, enemies[i].Position) <= Stats.Radius)
                {
                    if (Targets.Count < totalTargets)
                        Targets.Add(enemies[i]);
                }
            }
            for (int i = 0; i < enemyB.Count; i++)
            {
                if (Vector2.Distance(Position, enemyB[i].Position) <= Stats.Radius)
                {
                    if (Targets.Count < totalTargets)
                        Targets.Add(enemyB[i]);
                }
            }
        }
        public void CreateProjectilesTowardsTarget(World parent, ProjectileType type)
        {
            Projectile temp = null;

            if (Targets.Count > 0)
            {

                for (int i = 0; i < Targets.Count; i++)
                {
                    switch (type)
                    {
                        case ProjectileType.Fire_Bal:
                            temp = new FireBal(ResourceManager.GetTexture("Fire_Bal"), Position.X, Position.Y, 32, 32, Targets[i], GetDamage());
                            break;
                        case ProjectileType.Lighing_bal:
                            break;
                        case ProjectileType.Arrow:
                            temp = new Arrow(ResourceManager.GetTexture("Arrow"), Position.X, Position.Y, 32, 32, Targets[i], GetDamage());
                            break;
                        case ProjectileType.Dark_Eye:
                            break;
                        case ProjectileType.Lightning_Axe:
                            int x = ResourceManager.GetTexture("Lightning_Axe").region.X;
                            int y = ResourceManager.GetTexture("Lightning_Axe").region.Y;
                            temp = new LightningAxe("LightningAxeAnimation", new FrameAnimation(ResourceManager.GetTexture("Lightning_Axe"), x, y, 32, 32, 3, 0.08f, new Point(3, 1)), Position.X, Position.Y, 32, 32, Targets[i], GetDamage());
                            break;
                        case ProjectileType.Normal_Axe:
                            //int x = ResourceManager.GetTexture("Troll_Thrower").region.X;
                            //int y = ResourceManager.GetTexture("Troll_Thrower").region.Y;
                            //temp = new Axe("AxeAnimation", new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), x, y, 32, 32, 5, 0.2f, new Point(1, 5)), Position.X, Position.Y, 32, 32, Targets[i]);
                            break;
                        default:
                            break;
                    }
                    parent.GameObjects.Add(temp);
                }
                Targets.Clear();
            }
            else
            {
                switch (type)
                {
                    case ProjectileType.Fire_Bal:
                        temp = new FireBal(ResourceManager.GetTexture("Fire_Bal"), Position.X, Position.Y, 32, 32, MovingDirection, GetDamage());
                        break;
                    case ProjectileType.Lighing_bal:
                        break;
                    case ProjectileType.Arrow:
                        temp = new Arrow(ResourceManager.GetTexture("Arrow"), Position.X, Position.Y, 32, 32, MovingDirection, GetDamage());
                        break;
                    case ProjectileType.Dark_Eye:
                        break;
                    case ProjectileType.Lightning_Axe:
                        int x = ResourceManager.GetTexture("Lightning_Axe").region.X;
                        int y = ResourceManager.GetTexture("Lightning_Axe").region.Y;
                        temp = new LightningAxe("LightningAxeAnimation", new FrameAnimation(ResourceManager.GetTexture("Lightning_Axe"), x, y, 32, 32, 3, 0.08f, new Point(3, 1)), Position.X, Position.Y, 32, 32, MovingDirection, GetDamage());
                        break;
                    case ProjectileType.Normal_Axe:
                        temp = new FireBal(ResourceManager.GetTexture("Normal_Axe"), Position.X, Position.Y, 32, 32, MovingDirection);
                        break;
                    default:
                        break;
                }

                parent.GameObjects.Add(temp);
            }
        }
        public void MeleeAttack()
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                Targets[i].Hit(GetDamage());
            }
            Targets.Clear();
        }

        public abstract int GetDamage();
        public abstract int GetDmgOnStats();
    }
}
