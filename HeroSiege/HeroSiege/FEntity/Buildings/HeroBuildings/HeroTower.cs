using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;

namespace HeroSiege.FEntity.Buildings.HeroBuildings
{
    class HeroTower : Building
    {
        public HeroTower(float x, float y)
            : base(ResourceManager.GetTexture("HTower_2"), x, y)
        {
            SetHitbox = new Rectangle((int)x - 10, (int)y - 20, 58, 58);
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override bool LevelUp(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
