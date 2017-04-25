using HeroSiege.FGameObject;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.GameWorld;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Players
{
    class Mage : Hero
    {
        const float FRAME_DURATION_MOVEMNT = 0.05f;
        const float FRAME_DURATION_ATTACK = 0.08f;
        const float FRAME_DURATION_DEATH = 0.15f;

        const string HERO_NAME = "Gordox";

        const int START_INT = 20;
        const int START_AGI = 20;
        const int START_STR = 20;
        const int START_ARM = 20;
        const int START_DMG = 20;

        const int START_HEALTH  = 400;
        const int START_MANA    = 200;
        const int START_MSPEED  = 400;
        const int START_SPEED   = 200;
        const int ATTACK_RADIUS = 200;

        public Mage(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            AddSpriteAnimations();
            sprite.SetAnimation("MoveNorth");
            boundingBox = new Rectangle((int)x, (int)y, 32, 32);
            offSetBound = new Vector2(0, 5);
            AttackFrame = 3;
        }

        public override void Init()
        {
            base.Init();
            HeroName = HERO_NAME;
        }
        protected override void InitStats()
        {
            base.InitStats();
            Stats.MaxSpeed  = START_MSPEED;
            Stats.Speed     = START_SPEED;
            Stats.MaxHealth = START_HEALTH;
            Stats.MaxMana   = START_MANA;
            Stats.Radius    = ATTACK_RADIUS;

            Stats.Intelligens = START_INT;
            Stats.Agility     = START_AGI;
            Stats.Strength    = START_STR;
            Stats.Armor       = START_ARM;
            Stats.Damage      = START_DMG;
        }

        protected override void AddSpriteAnimations()
        {
            //--- Movment animation ---//
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 0, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 64, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 128, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 192, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 256, 0, 64, 64, 4, FRAME_DURATION_MOVEMNT, new Point(1, 4)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 0, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 64, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 128, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 192, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 256, 256, 64, 64, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));

            //--- Death animation ---//
            sprite.AddAnimation("Death", new FrameAnimation(ResourceManager.GetTexture("MageSheet"), 0, 512, 64, 64, 7, FRAME_DURATION_DEATH, new Point(5, 2), false));
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

        //
        public override void GreenButton(World parent)
        {
            base.GreenButton(parent);
            
        }
        //Basic Fire ball attack
        public override void BlueButton(World parent)
        {
            base.BlueButton(parent);
            if (isAttaking && IsAlive) return;

            SetAttckAnimations();
            ResetAnimation();
            isAttaking = true;

            GetTargets(parent.Enemies);
            CreateProjectilesTowardsTarget(parent, ProjectileType.Fire_Bal);
        }
        //
        public override void RedButton(World parent)
        {
            base.RedButton(parent);
        }
        //
        public override void YellowButton(World parent)
        {
            base.YellowButton(parent);
        }

        public override void AButton(World parent)
        {
            base.AButton(parent);
        }
        public override void BButton(World parent)
        {
            base.BButton(parent);
        }

        protected override void Death()
        {
            base.Death();
            IsAlive = false;
            sprite.SetAnimation("Death");
            sprite.Animations.CurrentAnimation.ResetAnimation();
        }

        public override int GetDamage()
        {
            return Stats.Damage + GetDmgOnStats();
        }

        public override int GetDmgOnStats()
        {
            return (int)(Stats.Damage * (Stats.Intelligens / 100f)); //+inventory items
        }
    }
}
