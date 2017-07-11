using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;

namespace HeroSiege.FGameObject.Attacks
{
    class LavaFloor : GameObject
    {
        public const float LIFE_TIME = 15; //sec
        private float timer;
        public int Damage { get; private set; }
        public LavaFloor(float x, float y, int dmg = 20, float width = 32, float height = 32)
            : base(null, x, y, width, height)
        {
            sprite.AddAnimation("Burning", new FrameAnimation(ResourceManager.GetTexture("Burning"), 0, 128, 32, 32, 6, 0.05f,
                                           new Point(6, 1), true, false)).SetAnimation("Burning");

            boundingBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, 16, 16);
            IsAlive = true;
            this.Damage = dmg;
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            timer += delta;
            if (timer > LIFE_TIME)
                IsAlive = false;
        }

        public float GetDamage(float delta)
        {
            if (IsAlive)
                return Damage * delta;
            else
                return 0;
        }



        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

        }
    }
}
