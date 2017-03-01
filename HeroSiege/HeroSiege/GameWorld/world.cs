using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.GameWorld.map;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.GameWorld
{
    class world
    {
        public TileMap Map { get; private set; }

        public world(string mapName) 
        {
            Initmap(mapName);
        }

        public void Initmap(string mapName)
        {
            this.Map = new TileMap(mapName);
        }

        public void InitEntitys()
        {
            if (Map != null)
            {

            }
        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch SB)
        {

        }
    }
}
