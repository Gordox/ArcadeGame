using HeroSiege.FEntity;
using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D.SpriteEffect;

namespace HeroSiege.FGameObject.Projectiles
{
    class Harpon : Projectile
    {
        const float LIFE_TIME = 1.5f; //5 sec
        const float DAMAGE = 20;

        public Harpon(TextureRegion region, float x, float y, float width, float height, Entity target)
            : base(region, x, y, width, height, target)
        {
        }

        public Harpon(TextureRegion region, float x, float y, float width, float height, Direction direction)
            : base(region, x, y, width, height, direction)
        {
            InitTexture(region, direction);
        }

        protected override void InitStats()
        {
            stats = new StatsData();
            stats.MaxSpeed = 400;
            stats.Speed = 200;
            stats.Damage = DAMAGE;
            lifeTimer = LIFE_TIME;
        }

        public override void Update(float delta)
        {
            if (target != null)
                UpdateMovingDirTowardsTarget();

            if (target != null && !target.IsAlive)
                target = null;

            base.Update(delta);
        }

        public override EffectType GetCollisionFX()
        {
            return EffectType.NONE;
        }
    }
}
