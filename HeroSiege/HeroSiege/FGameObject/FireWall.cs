using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.FGameObject
{
    class FireWall : GameObject
    {
        private int lives = 4;

        List<Sprite> fireWallList;

        public FireWall(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            IsAlive = true;

            fireWallList = new List<Sprite>();

            for (int i = 0; i < 7; i++)
            {
                Sprite temp = new Sprite(null, 0, 0, 0, 0);
                temp.SetPosition(x - 15 + 32 * i, y);
                temp.AddAnimation("fire", new FrameAnimation(ResourceManager.GetTexture("Burning"), 0, 128, 32, 32, 6, 0.05f, new Point(6, 1), true, false)).SetAnimation("fire");
                temp.SetSize(32, 32);
                fireWallList.Add(temp);
                Sprite temp2 = new Sprite(null, 0, 0, 0, 0);
                temp2.SetPosition(x - 15 + 32 * i, y - 22);
                temp2.AddAnimation("fire", new FrameAnimation(ResourceManager.GetTexture("Burning"), 0, 128, 32, 32, 6, 0.05f, new Point(6, 1), true, false)).SetAnimation("fire");
                temp2.SetSize(32, 32);
                fireWallList.Add(temp2);
            }
        }


        public void RemoveLive()
        {
            lives--;
            if (lives <= 0)
                IsAlive = false;
        }


        public override void Update(float delta)
        {
            base.Update(delta);
            foreach (Sprite s in fireWallList)
            {
                if(s != null)
                    s.Update(delta);
            }
        }

        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            foreach (Sprite s in fireWallList)
            {
                if (s != null)
                    s.Draw(SB);
            }
        }
    }
}
