﻿using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Buildings
{
    class Shop : Building
    {
        public Shop(float x, float y)
            : base(ResourceManager.GetTexture(""), x, y)
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
