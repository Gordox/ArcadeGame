using HeroSiege.FEntity;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.SpriteEffect;

namespace HeroSiege.FGameObject.Projectiles
{
    class FireBal : Projectile
    {
        const float LIFE_TIME = 1.5f; //5 sec
        const int DAMAGE = 20;

        public FireBal(TextureRegion region, float x, float y, float width, float height, Entity target, int dmg = 0)
            : base(region, x, y, width, height, target, dmg)
        {
        }
        public FireBal(TextureRegion region, float x, float y, float width, float height, Direction direction, int dmg = 0)
            : base(region, x, y, width, height, direction, dmg)
        {
        }
        public FireBal(TextureRegion region, float x, float y, float width, float height, float degree, int dmg = 0)
            : base(region, x, y, width, height, degree, dmg)
        {
        }
        protected override void Init()
        {
            base.Init();
            boundingBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, 16, 16);
        }
        protected override void InitStats()
        {
            if (damage == 0)
                damage = DAMAGE;

            stats = new StatsData();
            stats.MaxSpeed = 400;
            stats.Speed = 200;
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
