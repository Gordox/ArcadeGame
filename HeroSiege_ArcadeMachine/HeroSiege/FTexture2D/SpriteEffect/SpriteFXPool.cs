using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FTexture2D.SpriteEffect
{
    public class SpriteFXPool : Pool<SpriteFX>
    {
        public override SpriteFX newObject()
        {
            return new SpriteFX();
        }
    }
}
