using HeroSiege.FEntity.Controllers;
using HeroSiege.FGameObject;
using HeroSiege.FGameObject.Projectiles;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.SpriteEffect;
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

    abstract class Building : Entity
    {

        protected List<Entity> targets;
        protected int totalTargets;

        protected bool isUpgrading;


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

            if (Stats.Health < Stats.MaxHealth)
                DrawHealtBar(SB);
        }

        //Health bar
        protected void DrawHealtBar(SpriteBatch SB)
        {
            //Background
            SB.Draw(ResourceManager.GetTexture("WhitePixel"), new Vector2(Position.X - 25, Position.Y - 30), new Rectangle(0, 0, 50, 8), Color.Black);
            SB.Draw(ResourceManager.GetTexture("WhitePixel"), new Vector2(Position.X - 24, Position.Y - 29),
                                                              GenerateBar(Stats.Health, Stats.MaxHealth, 48, 6),
                                                              LerpHealthColor(Stats.Health, Stats.MaxHealth));
        }

        //----- NAME HERE -----//
        private void CheckIsAlive()
        {
            if (Stats != null && Stats.Health <= 0)
                IsAlive = false;
        }

        public abstract bool LevelUp(float delta);
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
                if (enemies[i] == null || !enemies[i].IsAlive)
                    continue;

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
                            temp = new BigCanonBal(ResourceManager.GetTexture("Big_Canon_bal"), Position.X, Position.Y, 32, 32, targets[i]);
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

        public abstract EffectType GetDeathFX();

    }
}
