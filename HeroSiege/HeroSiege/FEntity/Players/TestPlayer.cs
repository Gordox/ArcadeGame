using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using HeroSiege.GameWorld;
using HeroSiege.FGameObject;

namespace HeroSiege.FEntity.Players
{
    class TestPlayer : Hero
    {
        const float FRAME_DURATION_MOVEMNT = 0.05f;
        const float FRAME_DURATION_ATTACK = 0.08f;
        const float FRAME_DURATION_DEATH = 0.15f;
        
        
        public TestPlayer(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            AddSpriteAnimations();
            sprite.SetAnimation("MoveNorth");
            boundingBox = new Rectangle((int)x, (int)y, 32, 32);
            offSetBound = new Vector2(0, 5);
            AttackFrame = 3;
        }

        protected override void AddSpriteAnimations()
        {
            //--- Movment animation ---//
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"),   0, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"),  64, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 128, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 192, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 256, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",          new FrameAnimation(ResourceManager.GetTexture("MageSheet"),   0, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckNorthWestEast",  new FrameAnimation(ResourceManager.GetTexture("MageSheet"),  64, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckWestEast",       new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 128, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouthWestEast",  new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 192, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouth",          new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 256, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));

            //--- Death animation ---//
            sprite.AddAnimation("Death", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 0, 512, 64, 64, 7, FRAME_DURATION_DEATH, new Point(5, 2), false));

        }

        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxSpeed = 400;
            Stats.Speed = 200;
            Stats.MaxHealth = 100;
            Stats.MaxMana = 1;
            Stats.Radius = 200;
        }


        public override void Update(float delta)
        {
            
            base.Update(delta);
        }

        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);
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
        }

        public override void GreenButton(World parent)
        {
            base.GreenButton(parent);
            Death();
        }

        public override void BlueButton(World parent)
        {
            base.BlueButton(parent);
            if (!isAttaking && IsAlive)
            {
                SetAttckAnimations();
                ResetAnimation();
                isAttaking = true;

                GetTargets(parent.Enemies);
                CreateProjectilesTowardsTarget(parent, ProjectileType.Fire_Bal);
            }
        }

        protected override void Death()
        {
            base.Death();
            IsAlive = false;
            sprite.SetAnimation("Death");
            sprite.Animations.CurrentAnimation.ResetAnimation();
        }
    }
}
