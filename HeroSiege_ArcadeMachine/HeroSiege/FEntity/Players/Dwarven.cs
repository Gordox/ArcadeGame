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
    class Dwarven : Hero
    {
        const float FRAME_DURATION_MOVEMNT = 0.05f;
        const float FRAME_DURATION_ATTACK = 0.08f;
        const float FRAME_DURATION_DEATH = 0.15f;

        const string HERO_NAME = "Horpos";

        //--- Hero stats ---//
        //Attributes
        const int START_INT = 200;
        const int START_AGI = 200;
        const int START_STR = 5;
        const int START_ARM = 15;
        const int START_DMG = 30;

        const int START_HEALTH = 2800;
        const int START_MANA = 200;
        const int START_MSPEED = 400;
        const int START_SPEED = 200;
        const int ATTACK_RADIUS = 55;

        //Special attack
        private bool rageActivated;
        const float RAGE_BONUS_MULTI = .15f;
        const float RAGE_MANA_COST = 15.0f;
        const float RAGE_SIZE_INCREASE_VALUE = 1.30f;

        public Dwarven(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            AddSpriteAnimations();
            sprite.SetAnimation("MoveNorth");
            boundingBox = new Rectangle((int)x, (int)y, 32, 32);
            offSetBound = new Vector2(0, 5);
            AttackFrame = 2;
        }

        public override void Init()
        {
            base.Init();
            HeroName = HERO_NAME;
            attackType = AttackType.Melee;
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
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("Dwarven"),   0, 0, 56, 56, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Dwarven"),  56, 0, 56, 56, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("Dwarven"), 112, 0, 56, 56, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Dwarven"), 168, 0, 56, 56, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("Dwarven"), 224, 0, 56, 56, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("Dwarven"),   0, 224, 56, 56, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("Dwarven"),  56, 224, 56, 56, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("Dwarven"), 112, 224, 56, 56, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("Dwarven"), 168, 224, 56, 56, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("Dwarven"), 224, 224, 56, 56, 3, FRAME_DURATION_ATTACK, new Point(1, 3)));

            //--- Death animation ---//
            sprite.AddAnimation("Death", new FrameAnimation(ResourceManager.GetTexture("Dwarven"), 0, 448, 56, 56, 5, FRAME_DURATION_DEATH, new Point(5, 1), false));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (rageActivated)
                UpdateRage(delta);
        }

        private void UpdateRage(float delta)
        {
            Stats.Mana -= RAGE_MANA_COST * delta;

            if (Stats.Mana <= 0)
            {
                Stats.Mana = 0;
                rageActivated = false;
                this.sprite.Size = this.sprite.Size / RAGE_SIZE_INCREASE_VALUE;
                this.sprite.Color = Color.White;
            }
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

        //Melee attack
        public override void GreenButton(World parent)
        {
            base.GreenButton(parent);
            if (isAttaking && IsAlive) return;

            SetAttckAnimations();
            ResetAnimation();
            isAttaking = true;
            GetAllTargets(parent.Enemies, parent.EnemyBuildings);
            if (rageActivated)
                MeleeAttack(GetDamage() * RAGE_BONUS_MULTI);
            else
                MeleeAttack();
        }

        //Activate Rage mode
        public override void YellowButton(World parent)
        {
            base.YellowButton(parent);
            if (Stats.Mana > 0)
                rageActivated = !rageActivated;

            if (Stats.Mana <= 0)
                rageActivated = false;

            if (rageActivated)
            {
                this.sprite.Size = this.sprite.Size * RAGE_SIZE_INCREASE_VALUE;
                this.sprite.Color = Color.Red * 0.8f;
            }
            else
            {
                this.sprite.Size = this.sprite.Size / RAGE_SIZE_INCREASE_VALUE;
                this.sprite.Color = Color.White;
            }
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
            return (int)(Stats.Damage * (GetStrenght() / 100f));
        }
    }
}
