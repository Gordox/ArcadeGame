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
        const int DAMAGE = 120;

        public BigCanonBal(TextureRegion region, float x, float y, float width, float height, Entity target, int dmg = 0)
            : base(region, x, y, width, height, target, dmg)
        {
        }

        public BigCanonBal(TextureRegion region, float x, float y, float width, float height, Direction direction, int dmg = 0)
            : base(region, x, y, width, height, direction, dmg)
        {
            InitTexture(region, direction);
        }

        protected override void InitStats()
        {
            if (damage == 0)
                damage = DAMAGE;
            else
                damage += DAMAGE + damage / 4;
            stats = new StatsData();
            stats.MaxSpeed = 400;
            stats.Speed = 350;
            stats.Damage = damage;
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
