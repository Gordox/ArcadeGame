using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.InterFace
{
    interface IUI
    {

        void Update(float delta);
        void Draw(SpriteBatch SB);
    }
}
