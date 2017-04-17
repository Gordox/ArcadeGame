using HeroSiege.FTexture2D;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
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


        public HeroCastle(float x, float y) 
            : base(ResourceManager.GetTexture("Castle_lvl_1"), x, y)
        {
            SetHitbox = new Rectangle((int)x - 35, (int)y - 35, 95, 95);
        }

        public override void Init()
        {
            base.Init();
        }
        protected override void InitStats()
        {
            Stats = new StatsData();
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