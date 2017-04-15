using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FTexture2D.SpriteEffect
{
    public class SpriteFX : Sprite, IPoolable
    {
        public enum EffectType
        {
            Big_Explosion,
            Medium_Explosion,
            Light_Magic_Explosion,
            Dark_Magic_Explosion,
            Frost_Hit,
            Brown_Hit,
            Fire_Hit,
            NONE
        }

        public bool Done { get; set; }

        public SpriteFX() : base(null, 0, 0, 0, 0)
        {
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            Done = Animations.CurrentAnimation.GetPercent() == 1;
        }

        public void Reset()
        {
            Animations.Clear();
        }
    }
}
