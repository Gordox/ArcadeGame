using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.GameWorld.map
{
    class Tile : Sprite
    {
        public bool Wakeble { get; set; }

        public int TileSize { get { return tileSize; } }
        private int tileSize;

        public Tile(TextureRegion region, float x, float y, int tileSize, bool wakeble = true)
            : base(region, x, y, tileSize, tileSize)
        {
            this.tileSize = tileSize;
            this.Wakeble = wakeble;
        }
    }
}
