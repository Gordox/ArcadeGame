using HeroSiege.FEntity;
using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;
using HeroSiege.FTexture2D.SpriteEffect;

namespace HeroSiege.FGameObject.Projectiles
{
    class FireBal : Projectile
    {
        const float LIFE_TIME = 1.5f; //5 sec
        const float DAMAGE = 20;

        public FireBal(TextureRegion region, float x, float y, float width, float height, Entity target)
            : base(region, x, y, width, height, target)
        {
        }
        public FireBal(TextureRegion region, float x, float y, float width, float height, Direction direction)
            : base(region, x, y, width, height, direction)
        {
        }
        protected override void Init()
        {
            base.Init();
            boundingBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, 16, 16);
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

            base.Update(delta);
        }

        public override SpriteFX.EffectType GetCollisionFX()
        {
            return SpriteFX.EffectType.Fire_Hit;
        }
    }
}
