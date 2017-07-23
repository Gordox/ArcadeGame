using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D.SpriteEffect;

namespace HeroSiege.FEntity.Buildings.EnemyBuildings
{
    class EnemySpawner : Building
    {
        const float HEALTH = 3800;
        const int Armor = 100;

        public EnemySpawner(float x, float y)
            : base(ResourceManager.GetTexture("Spawn_Altar"), x, y)
        {
            SetHitbox = new Rectangle((int)x - 10, (int)y - 20, 58, 58);

            if (x > 2000)
                sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
        }

        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.Radius = 250;
            Stats.MaxHealth = HEALTH;
            Stats.Armor = Armor;
        }

        public override bool LevelUp(float delta)
        {
            throw new NotImplementedException();
        }

        public override EffectType GetDeathFX()
        {
            return EffectType.Big_Explosion;
        }



    }
}
