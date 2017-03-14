using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FTexture2D.FAnimation
{
    public abstract class Animation
    {
        public int currentFrame { get; protected set; }
        protected int lastFrame = -1;

        public abstract void Update(float delta);

        public abstract TextureRegion GetRegion();

        public abstract float GetPercent();

        public abstract bool HasNext();
    }
}
