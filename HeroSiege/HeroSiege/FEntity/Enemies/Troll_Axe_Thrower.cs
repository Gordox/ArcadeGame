﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.FAnimation;
using Microsoft.Xna.Framework;
using HeroSiege.Manager;
using HeroSiege.FGameObject;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.FEntity.Enemies
{
    class Troll_Axe_Thrower : Enemy
    {
        const float FRAME_DURATION_MOVEMNT = 0.08f;
        const float FRAME_DURATION_ATTACK = 0.08f;
        const float FRAME_DURATION_DEATH = 0.15f;


        public Troll_Axe_Thrower(float x, float y, float width, float height, AttackType attackType)
            : base(null, x, y, width, height, attackType)
        {
            AddSpriteAnimations();
            SetMovmentAnimations();

            boundingBox = new Rectangle((int)x, (int)y, 32, 32);
            offSetBound = new Vector2(0, 5);
            sprite.PauseAnimation = false;
        }

        public override void Init()
        {
            base.Init();
            ProjectType = ProjectileType.Normal_Axe;
            AttackFrame = 3;
            AttackSpeed = 0.8f;
        }
        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxSpeed = 400;
            Stats.Speed = 50;
            Stats.MaxHealth = 100;
            Stats.MaxMana = 1;
            Stats.visibilityRadius = 250;
            Stats.Radius = 180;
        }

        protected override void AddSpriteAnimations()
        {
            //--- Movment animation ---//
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"),   0, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"),  64, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 128, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 192, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 256, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"),   0, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"),  64, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 128, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 192, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 256, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));

            //--- Death animation ---//
            sprite.AddAnimation("Death_1", new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 0, 576, 64, 64, 3, FRAME_DURATION_DEATH, new Point(1, 3), false));
            sprite.AddAnimation("Death_2", new FrameAnimation(ResourceManager.GetTexture("Troll_Thrower"), 0, 640, 64, 64, 3, FRAME_DURATION_DEATH, new Point(1, 3), false));

        }

        protected override void SetMovmentAnimations()
        {
            switch (MovingDirection)
            {
                case Direction.North:
                    sprite.SetAnimation("MoveNorth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.North_East:
                    sprite.SetAnimation("MoveNorthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.East:
                    sprite.SetAnimation("MoveWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.South_East:
                    sprite.SetAnimation("MoveSouthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.South:
                    sprite.SetAnimation("MoveSouth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.South_West:
                    sprite.SetAnimation("MoveSouthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.West:
                    sprite.SetAnimation("MoveWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.North_West:
                    sprite.SetAnimation("MoveNorthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }
        }
        protected override void SetAttckAnimations()
        {
            switch (MovingDirection)
            {
                case Direction.North:
                    sprite.SetAnimation("AttckNorth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.North_East:
                    sprite.SetAnimation("AttckNorthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.East:
                    sprite.SetAnimation("AttckWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.South_East:
                    sprite.SetAnimation("AttckSouthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.South:
                    sprite.SetAnimation("AttckSouth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case Direction.South_West:
                    sprite.SetAnimation("AttckSouthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.West:
                    sprite.SetAnimation("AttckWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.North_West:
                    sprite.SetAnimation("AttckNorthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }

            sprite.Animations.CurrentAnimation.ResetAnimation();
        }
    }
}
