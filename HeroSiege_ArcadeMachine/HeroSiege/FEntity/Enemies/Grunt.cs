using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework;
using HeroSiege.FGameObject;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;

namespace HeroSiege.FEntity.Enemies
{
    class Grunt : Enemy
    {
        const float FRAME_DURATION_MOVEMNT = 0.08f;
        const float FRAME_DURATION_ATTACK = 0.1f;
        const float FRAME_DURATION_DEATH = 0.15f;

        public Grunt(float x, float y, float width, float height, AttackType attackType,int level = 0)
            : base(null, x, y, width, height, attackType, level)
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
            AttackFrame = 3;
            AttackSpeed = 0.8f;
        }

        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxSpeed = 400;
            Stats.Speed = 50;
            Stats.MaxHealth = 200 + 100 * level;
            Stats.MaxMana = 1;
            Stats.visibilityRadius = 250;
            Stats.Radius = 55;
            Damage = 40;
        }

        protected override void AddSpriteAnimations()
        {
            //--- Movment animation ---//
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("Grunt"),   0, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Grunt"),  64, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("Grunt"), 128, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Grunt"), 192, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("Grunt"), 256, 0, 64, 64, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("Grunt"),   0, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Grunt"),  64, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("Grunt"), 128, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Grunt"), 192, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("Grunt"), 256, 320, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));

            //--- Death animation ---//
            sprite.AddAnimation("Death_1", new FrameAnimation(ResourceManager.GetTexture("Grunt"), 0, 576, 64, 64, 3, FRAME_DURATION_DEATH, new Point(3, 1), false));
            sprite.AddAnimation("Death_2", new FrameAnimation(ResourceManager.GetTexture("Grunt"), 0, 640, 64, 64, 3, FRAME_DURATION_DEATH, new Point(3, 1), false));

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
            SpriteEffects ef = SpriteEffects.None;
            if (MovingDirection == Direction.North || MovingDirection == Direction.North_East || MovingDirection == Direction.North_West || MovingDirection == Direction.East)
                temp = new FrameAnimation(ResourceManager.GetTexture("Grunt"), 0, 576, 64, 64, 3, FRAME_DURATION_DEATH, new Point(3, 1), false);
            else
                temp = new FrameAnimation(ResourceManager.GetTexture("Grunt"), 0, 640, 64, 64, 3, FRAME_DURATION_DEATH, new Point(3, 1), false);

            if (MovingDirection == Direction.North || MovingDirection == Direction.North_East || MovingDirection == Direction.East)
                ef = SpriteEffects.None;
            else if (MovingDirection == Direction.North_West || MovingDirection == Direction.West)
                ef = SpriteEffects.FlipHorizontally;

            else if (MovingDirection == Direction.South || MovingDirection == Direction.South_East || MovingDirection == Direction.East)
                ef = SpriteEffects.None;
            else if (MovingDirection == Direction.South_West || MovingDirection == Direction.West)
                ef = SpriteEffects.FlipHorizontally;

            Control.world.SpawnEffect("Death", temp, ef, Position, new Point(64, 64));
        }
    }
}
