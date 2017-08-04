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
    class GnomishFlyingMachine : Hero
    {
        const float FRAME_DURATION_MOVEMNT = 0.05f;
        const float FRAME_DURATION_ATTACK = 0.08f;
        const float FRAME_DURATION_DEATH = 0.15f;

        const string HERO_NAME = "Zoegas Nation";

        //--- Hero stats ---//
        //Attributes
        const int START_INT = 30;
        const int START_AGI = 15;
        const int START_STR = 1;
        const int START_ARM = 25;
        const int START_DMG = 15;

        const int START_HEALTH = 1500;
        const int START_MANA = 200;
        const int START_MSPEED = 400;
        const int START_SPEED = 200;
        const int ATTACK_RADIUS = 200;

        //Special attack
        const float BIG_CANON_BAL_MANA_COST = 50;

        public GnomishFlyingMachine(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            AddSpriteAnimations();
            sprite.SetAnimation("MoveNorth");
            boundingBox = new Rectangle((int)x, (int)y, 32, 32);
            offSetBound = new Vector2(0, 5);
            AttackFrame = 1;
        }

        public override void Init()
        {
            base.Init();
            HeroName = HERO_NAME;
            attackType = AttackType.Range;
        }
        protected override void InitStats()
        {
            base.InitStats();
            Stats.MaxSpeed = START_MSPEED;
            Stats.Speed = START_SPEED;
            Stats.MaxHealth = START_HEALTH;
            Stats.MaxMana = START_MANA;
            Stats.Radius = ATTACK_RADIUS;

            Stats.Intelligens = START_INT;
            Stats.Agility = START_AGI;
            Stats.Strength = START_STR;
            Stats.Armor = START_ARM;
            Stats.Damage = START_DMG;
        }

        protected override void AddSpriteAnimations()
        {
            //--- Movment animation ---//
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("Gnomish"),   0, 0, 80, 80, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Gnomish"),  80, 0, 80, 80, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("Gnomish"), 160, 0, 80, 80, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Gnomish"), 240, 0, 80, 80, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("Gnomish"), 320, 0, 80, 80, 2, FRAME_DURATION_MOVEMNT, new Point(1, 2)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("Gnomish"),   0, 0, 80, 80, 2, FRAME_DURATION_ATTACK, new Point(1, 2)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Gnomish"),  80, 0, 80, 80, 2, FRAME_DURATION_ATTACK, new Point(1, 2)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("Gnomish"), 160, 0, 80, 80, 2, FRAME_DURATION_ATTACK, new Point(1, 2)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Gnomish"), 240, 0, 80, 80, 2, FRAME_DURATION_ATTACK, new Point(1, 2)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("Gnomish"), 320, 0, 80, 80, 2, FRAME_DURATION_ATTACK, new Point(1, 2)));

            //--- Death animation ---//
            sprite.AddAnimation("Death", new FrameAnimation(ResourceManager.GetTexture("Gnomish"), 0, 160, 80, 80, 1, FRAME_DURATION_DEATH, new Point(2, 1), false));
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

        //Basic Fire ball attack
        public override void GreenButton(World parent)
        {
            base.GreenButton(parent);
            if (isAttaking && IsAlive) return;

            SetAttckAnimations();
            //ResetAnimation();
            isAttaking = true;

            GetTargets(parent.Enemies);
            CreateProjectilesTowardsTarget(parent, ProjectileType.small_Canon_Bal);

        }
        // Big canon vall
        public override void YellowButton(World parent)
        {
            base.YellowButton(parent);

            if (isAttaking && IsAlive) return;

            if (Stats.Mana < 0 || Stats.Mana < BIG_CANON_BAL_MANA_COST) return;

            Stats.Mana -= BIG_CANON_BAL_MANA_COST;

            SetAttckAnimations();
            ResetAnimation();
            isAttaking = true;

            GetTargets(parent.Enemies);
            CreateProjectilesTowardsTarget(parent, ProjectileType.Big_Canon_bal);
        }

        //Use Mana potion
        public override void BlueButton(World parent)
        {
            base.BlueButton(parent);
        }
        // Use Healing potion
        public override void RedButton(World parent)
        {
            base.RedButton(parent);
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
            IsAlive = false;
            sprite.SetAnimation("Death");
            sprite.Animations.CurrentAnimation.ResetAnimation();
            base.Death();
        }

        public override int GetDamage()
        {
            return Stats.Damage + GetDmgOnStats();
        }

        public override int GetDmgOnStats()
        {
            return (int)(Stats.Damage * (GetInteligence() / 100f));
        }
    }
}
