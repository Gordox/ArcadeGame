using HeroSiege.FEntity;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.SpriteEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Projectiles
{
    class EvilHand : Projectile
    {
        const float LIFE_TIME = 1.5f; //1.5 sec
        const int DAMAGE = 20;

        public EvilHand(TextureRegion region, float x, float y, float width, float height, Entity target, int dmg = 0)
            : base(region, x, y, width, height, target, dmg)
        {
        }


        protected override void InitStats()
        {
            if (damage == 0)
                damage = DAMAGE;
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
            return EffectType.Soul_Spin;
        }
    }
}
