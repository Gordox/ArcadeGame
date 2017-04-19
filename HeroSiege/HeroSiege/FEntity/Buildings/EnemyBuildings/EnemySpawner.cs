﻿using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Buildings.EnemyBuildings
{
    class EnemySpawner : Building
    {
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
            Stats.MaxHealth = 100;
            Stats.Armor = 1;
        }

        public override bool LevelUp(float delta)
        {
            throw new NotImplementedException();
        }
    }
}