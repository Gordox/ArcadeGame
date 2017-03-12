using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Buildings.HeroBuildings
{
    class HeroCastle : Building
    {

        private const float UPGRADE_TIME = 0;
        private float elapsedTime;


        public HeroCastle(TextureRegion region, float x, float y) 
            : base(region, x, y)
        {

        }

        public override void Init()
        {
            Stats.MaxHealth = 1;
            Stats.Armor = 1;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
        }


        public override bool LevelUp(float delta)
        {
            return true;
        }

    }
}
