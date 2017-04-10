using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using HeroSiege.FTexture2D.FAnimation;

namespace HeroSiege.FEntity.Enemies
{
    class TestEnemy : Enemy
    {
        public TestEnemy(float x, float y, float width, float height, AttackType attackType)
            : base(null, x, y, width, height, attackType)
        {
            InitStats();
            sprite.AddAnimation("Death", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 0, 512, 64, 64, 7, 1, new Point(5, 2)));
            sprite.SetAnimation("Death");
            
        }

        protected override void AddSpriteAnimations()
        {
        }

        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxSpeed = 400;
            Stats.Speed = 10;
            Stats.Health = 1;
            Stats.Mana = 1;
            AttackFrame = 2;
        }
    }
}
