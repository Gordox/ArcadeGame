using HeroSiege.FEntity.Controllers;
using HeroSiege.FGameObject;
using HeroSiege.FGameObject.Projectiles;
using HeroSiege.FTexture2D;
using HeroSiege.GameWorld;
using HeroSiege.Manager;
using HeroSiege.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Buildings
{
    enum BuildingLevel
    {
        Level_1,
        Level_2,
        Level_3
    }

    abstract class Building : GameObject
    {

        protected List<Entity> targets;
        protected int totalTargets;
        public bool isAttaking { get; set; }
        protected int AttackFrame; //Which frame the attack shall be used

        protected bool isUpgrading;

        public Control Control { get; protected set; }
        public StatsData Stats { get; protected set; }

        protected BuildingLevel buildingLevel;
        public Direction Dir { get; set; }

        public Building(TextureRegion region, float x, float y)
            : base(region, x, y, region.region.Width, region.region.Height)
        {
            Init();
            IsAlive = true;
            if(region != null)
                boundingBox = new Rectangle((int)x - region.region.Width / 2, (int)y - region.region.Height / 2, region.region.Width, region.region.Height);
        }

        public Building(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            Init();
            IsAlive = true;
           
        }

        public abstract void Init();

        public override void Update(float delta)
        {
            base.Update(delta);
            CheckIsAlive();

            if (Control != null && IsAlive)
                Control.Update(delta);
        }

        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            if (DevTools.DevDebugMode || DevTools.DevDrawBoundingbox)
                DrawBoundingBox(SB);

            if (DevTools.DevDebugMode || DevTools.DevDrawRange && Stats != null && Stats.Radius > 0)
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
                    1, SpriteEffects.None, 0);
            }


        }

        //----- NAME HERE -----//
        private void CheckIsAlive()
        {
            if (Stats != null && Stats.Health <= 0)
                IsAlive = false;
        }

        public abstract bool LevelUp(float delta);
        public void SetControl(Control control)
        {
            this.Control = control;
        }
        //----- Other -----//
        public int GetTargetCount
        {
            get { if (targets != null) return targets.Count; else return -1; }
        }
        public void ClearTargets()
        {
            targets.Clear();
        }
        public void GetTargets(List<Entity> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector2.Distance(Position, enemies[i].Position) <= Stats.Radius)
                {
                    if (targets.Count < totalTargets)
                        targets.Add(enemies[i]);
                }
            }
        }

        public void CreateProjectilesTowardsTarget(World parent, ProjectileType type)
        {
            Projectile temp = null;

            if (targets.Count > 0)
            {

                for (int i = 0; i < targets.Count; i++)
                {
                    switch (type)
                    {
                        case ProjectileType.Harpon:
                            temp = new Harpon(ResourceManager.GetTexture("Harpon"), Position.X, Position.Y, 56, 56, targets[i]);
                            break;
                        case ProjectileType.Big_Canon_bal:
                            break;
                        case ProjectileType.Medium_Canon_Bal:
                            break;
                        case ProjectileType.small_Canon_Bal:
                            break;
                        case ProjectileType.Fire_Canon_Bal:
                            break;
                        case ProjectileType.Canon_Ball:
                            break;
                        default:
                            break;
                    }

                    parent.GameObjects.Add(temp);
                }
                targets.Clear();
            }
        }
        public void Hit(float damage)
        {
            Stats.Health = Stats.Health - (damage - (damage * (Stats.Armor / 1000)));
        }
    }
}
