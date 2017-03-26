using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HeroSiege.Manager.Resource
{
    public class TextureResource
    {
        private Dictionary<string, TextureRegion> textureRegion; // Used for all the rest f the textures
        private Dictionary<Tuple<string, int>, TextureRegion> tileTextureRegion; // Used only to store tile texture region
        //Tiles
        private Texture2D tileTextureSummer;
        //Heros
        private Texture2D mageHeroSpriteSheet;
        //HeroBuildings
        private Texture2D heroBuildings;
        //Other
        private Texture2D blackPixel, whitePixel;

        //Layer Name for the Tile Texure
        const string BASE_LAYER_NAME = "BaseLayer";
        const string HITBOX_LAYER_NAME = "HitBoxLayer";

        public void Load(ContentManager content)
        {
            this.textureRegion = new Dictionary<string, TextureRegion>();
            this.tileTextureRegion = new Dictionary<Tuple<string, int>, TextureRegion>();
            LoadAssets(content);
        }

        private void LoadAssets(ContentManager content)
        {
            //Load in all sprite sheets and Textures
            tileTextureSummer = content.Load<Texture2D>(@"Assets\Texture\Tiles\Wc2-Tiles");
            mageHeroSpriteSheet = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\MageHero");
            heroBuildings = content.Load<Texture2D>(@"Assets\Texture\Buildings\HeroBuildings");
            blackPixel = content.Load<Texture2D>(@"Assets\Other\BlackPixel");
            whitePixel = content.Load<Texture2D>(@"Assets\Other\WhitePixel");
            //Add all textures and sprite to Dict
            AddTextureRegionToDict();
        }

       
        private void AddTextureRegionToDict()
        {
            //Exapmle:
            //textureRegion["night"] = new TextureRegion(standTiles, 0, 0, 1, 1);

            //Other
            textureRegion["BlackPixel"] = new TextureRegion(blackPixel, 0, 0, 1, 1);
            textureRegion["WhitePixel"] = new TextureRegion(whitePixel, 0, 0, 1, 1);

            //UI

            //Tiles
            AddAllTileRegionsToDict();

            //Player Heros
            textureRegion["MageSheet"] = new TextureRegion(mageHeroSpriteSheet, 0, 0, mageHeroSpriteSheet.Width, mageHeroSpriteSheet.Height);

            //Hero Buildings
            textureRegion["Castle_lvl_1"] =        new TextureRegion(heroBuildings,   0,   0, 128, 128);
            textureRegion["Castle_lvl_1_Broken"] = new TextureRegion(heroBuildings,   0, 128, 128, 128);
            textureRegion["Castle_lvl_2"] =        new TextureRegion(heroBuildings, 128,   0, 128, 128);
            textureRegion["Castle_lvl_2_Broken"] = new TextureRegion(heroBuildings, 128, 128, 128, 128);

            textureRegion["HTower_1"] = new TextureRegion(heroBuildings, 256,  0, 64, 64);
            textureRegion["HTower_2"] = new TextureRegion(heroBuildings, 320,  0, 64, 64);
            textureRegion["HTower_3"] = new TextureRegion(heroBuildings, 256, 64, 64, 64);


        }

        private void AddAllTileRegionsToDict()
        {
            //Summer Tile;
            int id = 0;
            for (int y = 0; y < tileTextureSummer.Height / 32; y++)
            {
                for (int x = 0; x < tileTextureSummer.Width / 32; x++)
                {
                    tileTextureRegion[Tuple.Create(BASE_LAYER_NAME, id)] = new TextureRegion(tileTextureSummer, x * 32, y * 32, 32, 32);
                    id++;
                }
            }
            
        }

        public TextureRegion GetTextureRegion(string name)
        {
            try { return textureRegion[name]; }
            catch { return null; }
        }

        /// <summary>
        /// Use to get tile texture ONLY!
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public TextureRegion GetTextureRegion(string name, int id)
        {
            var key = Tuple.Create(name, id);
            try { return tileTextureRegion[key]; }
            catch { return null; }
        }

        /// <summary>
        /// Remove all testures
        /// Do only before closing the program
        /// </summary>
        public void UnloadTextures()
        {
            textureRegion.Clear();
            tileTextureRegion.Clear();
        }




    }
}
