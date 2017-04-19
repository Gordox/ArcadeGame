using HeroSiege.FEntity;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.FTexture2D.SpriteEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Projectiles
{
    class Axe : Projectile
    {
        const float LIFE_TIME = 1.5f; //1.5 sec
        const float DAMAGE = 20;

        public Axe(string animationName, FrameAnimation animation, float x, float y, float width, float height, Entity target)
            : base(animationName, animation, x, y, width, height, target)
        {

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
            return EffectType.NONE;
        }
    }
}
