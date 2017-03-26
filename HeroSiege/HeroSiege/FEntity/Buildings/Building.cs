using HeroSiege.FGameObject;
using HeroSiege.FTexture2D;
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

        protected bool isUpgrading;

        public StatsData Stats { get; protected set; }

        protected BuildingLevel buildingLevel;

        public bool IsAlive { get; set; }

        public Building(TextureRegion region, float x, float y)
            : base(region, x, y, region.region.Width, region.region.Height)
        {
            IsAlive = true;
            boundingBox = new Rectangle((int)x - region.region.Width / 2, (int)y - region.region.Height / 2, region.region.Width, region.region.Height);
        }

        public abstract void Init();

        public override void Update(float delta)
        {
            CheckIsAlive();
        }

        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            //DrawBoundingBox(SB);
        }

        //----- NAME HERE -----//
        private void CheckIsAlive()
        {
            if (Stats.Health <= 0)
                IsAlive = false;
        }

        public abstract bool LevelUp(float delta);

        //----- Other -----//
        public void Hit(float damage)
        {
            Stats.Health = Stats.Health - (damage - (damage * (Stats.Armor / 1000)));
        }
    }
}
