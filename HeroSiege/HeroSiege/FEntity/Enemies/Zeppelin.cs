using HeroSiege.FGameObject;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Enemies
{
    class Zeppelin : Enemy
    {
        const float FRAME_DURATION_MOVEMNT = 0.008f;
        const float FRAME_DURATION_ATTACK = 0.008f;
        const float FRAME_DURATION_DEATH = 0.15f;

        public Zeppelin(float x, float y, float width, float height, AttackType attackType)
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
            ProjectType = ProjectileType.Fire_Canon_Bal;
            AttackFrame = 1;
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
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("Zeppelin"),   0, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Zeppelin"),  72, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 144, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 216, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 288, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 0, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 72, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 144, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 216, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("Zeppelin"), 288, 0, 72, 72, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));

            //--- Death animation ---//
            //NONE

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
