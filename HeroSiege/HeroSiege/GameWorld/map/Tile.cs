using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework;

namespace HeroSiege.GameWorld.map
{
    class Tile : Sprite
    {
        public FogOfWarState Visibility { get; set; }
        public WalkTypes WalkType { get; private set; }

        public bool Wakeble { get; set; }

        public int TileSize { get { return tileSize; } }
        private int tileSize;

        public Tile(TextureRegion region, float x, float y, int tileSize)
            : base(region, x, y, tileSize, tileSize)
        {
            this.tileSize = tileSize;
        }

        /// <summary>
        /// Constructor for wakeble path
        /// </summary>
        /// <param name="region"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileSize"></param>
        /// <param name="wakeble"></param>
        public Tile(TextureRegion region, float x, float y, int tileSize, WalkTypes type, bool wakeble = true)
            : base(region, x, y, tileSize, tileSize)
        {
            this.tileSize = tileSize;
            this.Wakeble = wakeble;
            this.WalkType = type;

            Color = Color.White * 0.2f;
        }

        /// <summary>
        /// Constructor for Fog of war
        /// </summary>
        /// <param name="region"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileSize"></param>
        public Tile(TextureRegion region, float x, float y, int tileSize, FogOfWarState state)
            : base(region, x, y, tileSize, tileSize)
        {
            this.Visibility = state;
            this.tileSize = tileSize;
        }
    }
}
