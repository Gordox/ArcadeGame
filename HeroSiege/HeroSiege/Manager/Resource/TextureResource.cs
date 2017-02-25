using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Manager.Resource
{
    public class TextureResource
    {
        private Dictionary<string, TextureRegion> textureRegion;
        private Texture2D example;

        public void Load(ContentManager content)
        {
            this.textureRegion = new Dictionary<string, TextureRegion>();
            LoadAssets(content);
        }
        private void LoadAssets(ContentManager content)
        {
            //Load in all sprite sheets
            //standTiles = content.Load<Texture2D>(@"Assets\Tile_sheet\Stand-tile");
            



            AddTextureRegionToDict();
        }


        private void AddTextureRegionToDict()
        {
            //Exapmle:
            //textureRegion["night"] = new TextureRegion(standTiles, 0, 0, 1, 1);

            //UI


            //Tiles
            //Street

            //farmland
        }

        public TextureRegion GetTextureRegion(string name)
        {
            try { return textureRegion[name]; }
            catch { return null; }
        }
        public TextureRegion GetTextureRegion(string name, int id)
        {
            try { return textureRegion[name]; }
            catch { return null; }
        }

        /// <summary>
        /// Remove all testures
        /// Do only before closing the program
        /// </summary>
        public void UnloadTextures()
        {
            textureRegion.Clear();
        }
    }
}
