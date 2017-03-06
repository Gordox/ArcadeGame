using HeroSiege.FGameObject;
using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Buildings
{
    class Building : GameObject
    {
        public StatsData Stats { get; protected set; }

        public Building(TextureRegion region, float x, float y)
            : base(region, x, y, region.region.Width, region.region.Height)
        {

        }





    }
}
