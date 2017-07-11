﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FGameObject;

namespace HeroSiege.FEntity.Enemies.Bosses
{
    class Demon : Enemy
    {
        const float FRAME_DURATION_MOVEMNT = 0.07f;
        const float FRAME_DURATION_ATTACK = 0.1f;
        const float FRAME_DURATION_DEATH = 0.15f;

        public Demon(float x, float y, float width, float height)
            : base(null, x, y, width, height, AttackType.Range, 50)
        {
            AddSpriteAnimations();
            MovingDirection = Direction.South;
            SetMovmentAnimations();

            boundingBox = new Rectangle((int)x, (int)y, 32, 32);
            offSetBound = new Vector2(0, 5);
            sprite.PauseAnimation = false;
        }

        public override void Init()
        {
            base.Init();
            ProjectType = ProjectileType.Fire_Bal;
            AttackFrame = 4;
            AttackSpeed = 0.8f;
        }
        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxSpeed = 400;
            Stats.Speed = 50;
            Stats.MaxHealth = 2000;
            Stats.MaxMana = 1;
            Stats.visibilityRadius = 250;
            Stats.Radius = 180;
        }

        protected override void AddSpriteAnimations()
        {
            //--- Movment animation ---//
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("Demon"),   0, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Demon"),  64, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("Demon"), 128, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Demon"), 192, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("Demon"), 256, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("Demon"),   0, 320, 64, 64, 5, FRAME_DURATION_ATTACK, new Point(1, 5)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Demon"),  64, 320, 64, 64, 5, FRAME_DURATION_ATTACK, new Point(1, 5)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("Demon"), 128, 320, 64, 64, 5, FRAME_DURATION_ATTACK, new Point(1, 5)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Demon"), 192, 320, 64, 64, 5, FRAME_DURATION_ATTACK, new Point(1, 5)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("Demon"), 256, 320, 64, 64, 5, FRAME_DURATION_ATTACK, new Point(1, 5)));

            //--- Death animation ---//
            sprite.AddAnimation("Death_1", new FrameAnimation(ResourceManager.GetTexture("Demon"), 0, 576, 64, 64, 5, FRAME_DURATION_DEATH, new Point(5, 1), false));

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


        protected override void Death()
        {
            base.Death();
            FrameAnimation temp;
            temp = new FrameAnimation(ResourceManager.GetTexture("Demon"), 0, 576, 64, 64, 5, FRAME_DURATION_DEATH, new Point(5, 1), false);

            Control.world.SpawnEffect("Death", temp, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, Position, new Point(64, 64));
        }


    }
}