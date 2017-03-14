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

namespace HeroSiege.FEntity.Player
{
    class TestPlayer : Entity
    {
        const float FRAME_DURATION_MOVEMNT = 0.05f;
        const float FRAME_DURATION_ATTACK = 1.18f;
        const float FRAME_DURATION_DEATH = 0.7f;
        
        Direction olddir;
        

        public TestPlayer(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            InitStats();
            AddSpriteAnimations();
            sprite.SetAnimation("MoveNorth");
            olddir = MovingDirection;
            boundingBox = new Rectangle((int)x, (int)y, 32, 32);
        }

        private void AddSpriteAnimations()
        {
            //--- Movment animation ---//
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"),   0, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"),  64, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 128, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 192, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 256, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",          new FrameAnimation(ResourceManager.GetTexture("MageSheet"),   0, 320, 64, 64, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckNorthWestEast",  new FrameAnimation(ResourceManager.GetTexture("MageSheet"),  64, 320, 64, 64, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckWestEast",       new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 128, 320, 64, 64, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckSouthWestEast",  new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 192, 320, 64, 64, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckSouth",          new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 256, 320, 64, 64, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));

            //--- Death animation ---//
            sprite.AddAnimation("Death", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 0, 512, 64, 64, 7, FRAME_DURATION_DEATH, new Point(5, 2), true));

        }

        public override void Update(float delta)
        {
            UpdateAnimation();

            base.Update(delta);
        }

        private void UpdateAnimation()
        {
            if (olddir != MovingDirection && !isAttaking)
            {
                ChangeMovmentAnimations();
                olddir = MovingDirection;
            }

            if (isAttaking && sprite.Animations.CurrentAnimation.currentFrame == 2)
            {
                //isAttaking = false;
                //UpdateMovmentAnimations();
            }
        }

        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxSpeed = 400;
            Stats.Speed = 200;
            Stats.Health = 1;
            Stats.Mana = 1;
            base.InitStats();
        }

        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            DrawBoundingBox(SB);
        }

        private void ChangeMovmentAnimations()
        {
            switch (MovingDirection)
            {
                case FEntity.Direction.North:
                    sprite.SetAnimation("MoveNorth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.North_East:
                    sprite.SetAnimation("MoveNorthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.East:
                    sprite.SetAnimation("MoveWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.South_East:
                    sprite.SetAnimation("MoveSouthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.South:
                    sprite.SetAnimation("MoveSouth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.South_West:
                    sprite.SetAnimation("MoveSouthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case FEntity.Direction.West:
                    sprite.SetAnimation("MoveWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case FEntity.Direction.North_West:
                    sprite.SetAnimation("MoveNorthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }
        }

        private void UpdateAttckAnimation()
        {
            switch (MovingDirection)
            {
                case FEntity.Direction.North:
                    sprite.SetAnimation("AttckNorth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.North_East:
                    sprite.SetAnimation("AttckNorthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.East:
                    sprite.SetAnimation("AttckWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.South_East:
                    sprite.SetAnimation("AttckSouthWestEast");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.South:
                    sprite.SetAnimation("AttckSouth");
                    sprite.Effect = SpriteEffects.None;
                    break;
                case FEntity.Direction.South_West:
                    sprite.SetAnimation("AttckSouthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case FEntity.Direction.West:
                    sprite.SetAnimation("AttckWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case FEntity.Direction.North_West:
                    sprite.SetAnimation("AttckNorthWestEast");
                    sprite.Effect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }

            //if(isAttaking)
            //{
            //    if (sprite.Animations.CurrentAnimation.GetRegion().region.X == 0)
            //    {
            //        isAttaking = false;
            //        UpdateMovmentAnimations();
            //    }
            //
            //}
        }

        public override void GreenButton(World parent)
        {
            base.GreenButton(parent);
            isAttaking = true;
            //UpdateAttckAnimation();
            sprite.SetAnimation("Death");
        }
        public override void BlueButton(World parent)
        {
            base.BlueButton(parent);

            isAttaking = false;
        }
    }
}
