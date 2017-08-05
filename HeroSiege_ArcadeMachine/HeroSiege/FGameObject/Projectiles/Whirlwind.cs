using HeroSiege.FEntity;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.FTexture2D.SpriteEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FGameObject.Projectiles
{
    class Whirlwind : Projectile
    {
        const float LIFE_TIME = 1.5f; //1.5 sec
        const int DAMAGE = 50;

        public Whirlwind(string animationName, FrameAnimation animation, float x, float y, float width, float height, Entity target, int dmg = 0)
            : base(animationName, animation, x, y, width, height, target, dmg)
        {
        }
        public Whirlwind(string animationName, FrameAnimation animation, float x, float y, float width, float height, Direction direction, int dmg = 0)
            : base(animationName, animation, x, y, width, height, direction, dmg)
        {
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
            return EffectType.Light_Magic_Explosion;
        }
    }
}
