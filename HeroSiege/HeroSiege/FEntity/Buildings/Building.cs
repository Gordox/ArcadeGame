using HeroSiege.FGameObject;
using HeroSiege.FTexture2D;
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

        public Building(TextureRegion region, float x, float y)
            : base(region, x, y, region.region.Width, region.region.Height)
        {

        }

        public abstract void Init();

        public abstract bool LevelUp(float delta);


    }
}
