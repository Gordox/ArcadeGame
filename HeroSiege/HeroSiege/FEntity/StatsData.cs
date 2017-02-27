using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity
{
    class StatsData
    {
        //Health
        public float Health { get; set; }

        private float maxHealth;
        public float MaxHealth { get { return maxHealth; } set { maxHealth = value; Health = value; } }

        //Mana
        public float Mana { get; set; }

        private float maxMana;
        public float MaxMana { get { return maxMana; } set { maxMana = value; Mana = value; } }

        //Movment
        private float maxSpeed;
        public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; Speed = value; } }

        private float speed;
        public float Speed { get { return speed; } set { speed = MathHelper.Clamp(value, 0, MaxSpeed); } }

        public Vector2 velocity;

        //ETC
        public float Damage { get; set; }

        public float Armor { get; set; }

        public float CritChance { get; set; }

        public float Radius { get; set; }

        public float visibilityRadius { get; set; }
    }
}
