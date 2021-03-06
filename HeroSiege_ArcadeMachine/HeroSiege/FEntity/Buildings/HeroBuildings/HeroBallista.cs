﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.Manager;
using HeroSiege.FTexture2D.FAnimation;
using Microsoft.Xna.Framework;
using HeroSiege.FGameObject;
using HeroSiege.FTexture2D.SpriteEffect;

namespace HeroSiege.FEntity.Buildings.HeroBuildings
{
    class HeroBallista : Building
    {
        const float FRAME_DURATION_ATTACK = 0.08f;

        public float AttackSpeed { get { return ATTACK_SPEED; } }

        const float ATTACK_SPEED = 0.8f;

        public HeroBallista(float x, float y)
            : base(x, y, 64, 64)
        {
            AddSpriteAnimations();
            setDirection(x, y);
            setAttackAnimation();
            sprite.PauseAnimation = true;
        }

        public override void Init()
        {
            base.Init();
            totalTargets = 1;
            targets = new List<Entity>();
            AttackFrame = 3;

            
        }
        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.Radius = 250;
            Stats.MaxHealth = 600;
            Stats.Armor = 120;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
        }

        /// <summary>
        /// Hardcoded 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void setDirection(float x, float y)
        {
            if (x == 1184 + 16 && y == 3488 + 16)
                Dir = Direction.East ;
            else if (x == 1280 + 16 && y == 3392 + 16)
                Dir = Direction.North;
            else if (x == 1376 + 16 && y == 3488 + 16)
                Dir = Direction.South;
            else if (x == 1280 + 16 && y == 3584 + 16)
                Dir = Direction.West;
        }
        public void setIdleTexture()
        {
            switch (Dir)
            {
                case Direction.North:
                    sprite.SetRegion(new TextureRegion(ResourceManager.GetTexture("Balista"), 0, 0, 64, 64));
                    break;
                case Direction.East:
                    sprite.SetRegion(new TextureRegion(ResourceManager.GetTexture("Balista"), 128, 0, 64, 64));
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    break;
                case Direction.South:
                    sprite.SetRegion(new TextureRegion(ResourceManager.GetTexture("Balista"), 256, 0, 64, 64));
                    break;
                case Direction.West:
                    sprite.SetRegion(new TextureRegion(ResourceManager.GetTexture("Balista"), 128, 0, 64, 64));
                    break;
                default:
                    break;
            }
        }
        public void setAttackAnimation()
        {
            switch (Dir)
            {
                case Direction.North:
                    sprite.SetAnimation("AttckNorth");
                    sprite.Animations.CurrentAnimation.ResetAnimation();
                    break;
                case Direction.East:
                    sprite.SetAnimation("AttckWestEast");
                    sprite.Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally;
                    sprite.Animations.CurrentAnimation.ResetAnimation();
                    break;
                case Direction.South:
                    sprite.SetAnimation("AttckWestEast");
                    sprite.Animations.CurrentAnimation.ResetAnimation();
                    break;
                case Direction.West:
                    sprite.SetAnimation("AttckSouth");
                    sprite.Animations.CurrentAnimation.ResetAnimation();
                    break;
                default:
                    break;
            }
        }
        public int GetCurrentFrame
        {
            get { return sprite.Animations.CurrentAnimation.currentFrame; }
        }
        public int GetAttackFrame
        {
            get { return AttackFrame; }
        }

        protected override void AddSpriteAnimations()
        {
            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",    new FrameAnimation(ResourceManager.GetTexture("Balista"),   0, 0, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckWestEast", new FrameAnimation(ResourceManager.GetTexture("Balista"), 128, 0, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouth",    new FrameAnimation(ResourceManager.GetTexture("Balista"), 256, 0, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
        }
        public override bool LevelUp(float delta)
        {
            return true;
        }

        public override EffectType GetDeathFX()
        {
            return EffectType.Medium_Explosion;
        }
    }
}
