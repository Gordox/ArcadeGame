﻿using HeroSiege.FEntity.Controllers;
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
    class Knight : Hero
    {
        const float FRAME_DURATION_MOVEMNT = 0.05f;
        const float FRAME_DURATION_ATTACK = 0.08f;
        const float FRAME_DURATION_DEATH = 0.15f;

        const string HERO_NAME = "Lucifer";

        //--- Hero stats ---//
        //Attributes
        const int START_INT = 8;
        const int START_AGI = 15;
        const int START_STR = 22;
        const int START_ARM = 30;
        const int START_DMG = 25;

        const int START_HEALTH = 1800;
        const int START_MANA = 200;
        const int START_MSPEED = 400;
        const int START_SPEED = 250;
        const int ATTACK_RADIUS = 55;
        //Special attack
        private bool hastActivated;
        const float HAST_VALUE = 1.90f;
        const float HAST_MANA_COST = 10.0f;


        public Knight(float x, float y, float width, float height)
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
            hastActivated = false;
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
            sprite.AddAnimation("MoveNorth",         new FrameAnimation(ResourceManager.GetTexture("KnightSheet"),   0, 0, 72, 72, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("KnightSheet"),  72, 0, 72, 72, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveWestEast",      new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 144, 0, 72, 72, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 216, 0, 72, 72, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));
            sprite.AddAnimation("MoveSouth",         new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 288, 0, 72, 72, 5, FRAME_DURATION_MOVEMNT, new Point(1, 5)));

            //--- Attck animation ---//
            sprite.AddAnimation("AttckNorth",         new FrameAnimation(ResourceManager.GetTexture("KnightSheet"),   0, 360, 72, 72, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckNorthWestEast", new FrameAnimation(ResourceManager.GetTexture("KnightSheet"),  72, 360, 72, 72, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckWestEast",      new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 144, 360, 72, 72, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouthWestEast", new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 216, 360, 72, 72, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));
            sprite.AddAnimation("AttckSouth",         new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 288, 360, 72, 72, 4, FRAME_DURATION_ATTACK, new Point(1, 4)));

            //--- Death animation ---//
            sprite.AddAnimation("Death_1", new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 0, 648, 72, 72, 5, FRAME_DURATION_DEATH, new Point(5, 1), false));
            sprite.AddAnimation("Death_2", new FrameAnimation(ResourceManager.GetTexture("KnightSheet"), 0, 720, 72, 72, 5, FRAME_DURATION_DEATH, new Point(5, 1), false));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (hastActivated)
                UpdateHast(delta);
        }

        private void UpdateHast(float delta)
        {
            Stats.Mana -= HAST_MANA_COST * delta;

            if(Stats.Mana <= 0)
            {
                Stats.Mana = 0;
                hastActivated = false;
            }
        }

        public override void UpdatePlayerMovement(float delta, List<Rectangle> objects)
        {
            if (isBuying)
                return;

            velocity = Vector2.Zero;

            if (Control != null && IsAlive)
                ((HumanControler)Control).UpdateJoystick(delta);

            Vector2 futurePos = Vector2.Zero;
            //Calculate future pos
            if (!hastActivated)
                futurePos = (position + velocity * delta);
            else
                futurePos = (position + velocity * delta * HAST_VALUE);

            if (MovingDirection == Direction.North_East || MovingDirection == Direction.North_West ||
                MovingDirection == Direction.South_East || MovingDirection == Direction.South_West)
                velocity *= 0.75f; //hard coded value so the player moves at the same speed side ways somewhat 

            //Update movement if no collision will happen
            if (!CheckCollision(new Rectangle((int)futurePos.X, (int)position.Y, this.GetBounds().Width, this.GetBounds().Height), objects))
                if (!hastActivated)
                    position.X += velocity.X * delta;
                else
                    position.X += velocity.X * delta * HAST_VALUE;

            if (!CheckCollision(new Rectangle((int)position.X, (int)futurePos.Y, this.GetBounds().Width, this.GetBounds().Height), objects))
                if (!hastActivated)
                    position.Y += velocity.Y * delta;
                else
                    position.Y += velocity.Y * delta * HAST_VALUE;
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

        //Basic attack
        public override void GreenButton(World parent)
        {
            base.GreenButton(parent);
            if (isAttaking && IsAlive) return;

            SetAttckAnimations();
            ResetAnimation();
            isAttaking = true;
            GetAllTargets(parent.Enemies, parent.EnemyBuildings);
            MeleeAttack();

        }
        //Hast
        public override void YellowButton(World parent)
        {
            base.YellowButton(parent);
            if(Stats.Mana > 0)
                hastActivated = !hastActivated;

            if (Stats.Mana <= 0)
                hastActivated = false;
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
            if (MovingDirection == Direction.North || MovingDirection == Direction.North_East || MovingDirection == Direction.North_West || MovingDirection == Direction.East)
                sprite.SetAnimation("Death_1");
            else
                sprite.SetAnimation("Death_2");
            sprite.Animations.CurrentAnimation.ResetAnimation();
            base.Death();
        }

        public override int GetDamage()
        {
            return Stats.Damage + GetDmgOnStats();
        }

        public override int GetDmgOnStats()
        {
            return (int)(Stats.Damage * (GetStrenght()/ 100f)); //+inventory items
        }
    }
}
