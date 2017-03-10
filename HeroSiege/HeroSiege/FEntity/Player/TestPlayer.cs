using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;

namespace HeroSiege.FEntity.Player
{
    class TestPlayer : Entity
    {
        Direction olddir;
        public TestPlayer(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            InitStats();
            AddSpriteAnimations();
            sprite.SetAnimation("MoveNorth");
            olddir = MovingDirection;
        }

        private void AddSpriteAnimations()
        {
            sprite.AddAnimation("MoveNorth", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 0, 0, 64, 64, 5, 0.075f, new Point(1, 5)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 64, 0, 64, 64, 5, 0.075f, new Point(1, 5)));
            sprite.AddAnimation("MoveWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 128, 0, 64, 64, 5, 0.075f, new Point(1, 5)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 192, 0, 64, 64, 5, 0.075f, new Point(1, 5)));
            sprite.AddAnimation("MoveSouth", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 256, 0, 64, 64, 5, 0.075f, new Point(1, 5)));
        }

        public override void Update(float delta)
        {
            if (olddir != MovingDirection)
            {
                UpdateSpriteAnimations();
                olddir = MovingDirection;
            }
            base.Update(delta);
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


        private void UpdateSpriteAnimations()
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
    }
}
