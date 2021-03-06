﻿using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D.SpriteEffect;

namespace HeroSiege.FEntity.Buildings.EnemyBuildings
{
    class EnemyTower : Building
    {
        const float HEALTH = 1500;
        const int Armor = 20;

        public float AttackSpeed { get { return ATTACK_SPEED; } }
        const float ATTACK_SPEED = 0.8f;

        public EnemyTower(float x, float y)
            : base(ResourceManager.GetTexture("ETower"), x, y)
        {
            SetHitbox = new Rectangle((int)x - 10, (int)y - 20, 58, 58);
        }

        public override void Init()
        {
            base.Init();
            totalTargets = 1;
            targets = new List<Entity>();
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
