using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;

namespace HeroSiege.FGameObject
{
    class FireWall : GameObject
    {
        private int lives = 4;

        public FireWall(float x, float y, float width, float height)
            : base(null, x, y, width, height)
        {
            IsAlive = true;
        }


        public void RemoveLive()
        {
            lives--;
            if (lives <= 0)
                IsAlive = false;
        }
    }
}
