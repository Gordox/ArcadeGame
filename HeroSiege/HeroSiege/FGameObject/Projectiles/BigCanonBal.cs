using HeroSiege.FEntity;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.SpriteEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Projectiles
{
    class BigCanonBal : Projectile
    {
        const float LIFE_TIME = 1.5f; //1.5 sec
        const float DAMAGE = 20;

        public BigCanonBal(TextureRegion region, float x, float y, float width, float height, Entity target)
            : base(region, x, y, width, height, target)
        {
        }

        public BigCanonBal(TextureRegion region, float x, float y, float width, float height, Direction direction)
            : base(region, x, y, width, height, direction)
        {
            InitTexture(region, direction);
        }

        protected override void InitStats()
        {
            stats = new StatsData();
            stats.MaxSpeed = 400;
            stats.Speed = 350;
            stats.Damage = DAMAGE;
            lifeTimer = LIFE_TIME;
        }

        public override void Update(float delta)
        {
            if (target != null)
                UpdateMovingDirTowardsTarget();

            base.Update(delta);
        }

        public override EffectType GetCollisionFX()
        {
            return EffectType.Fire_Hit;
        }
    }
}
