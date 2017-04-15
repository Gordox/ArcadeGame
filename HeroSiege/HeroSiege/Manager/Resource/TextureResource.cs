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
        //Enemies
        private Texture2D trollTrowerSpriteSheet, daemonSprite, deathKnightSprite, dragonSprite, gruntSPrite, orgeSprite, zeppelinSprite;
        //Buildings
        private Texture2D heroBuildings, enemieBuildnings, etcBuildings, heroBalista;
        //Magic and Missiels
        private Texture2D explosionSprite, projectileSprite, effecSprite;
        //Other
        private Texture2D blackPixel, whitePixel;
        //Debug
        private Texture2D debugWalkTile, debugRange;

        //Layer Name for the Tile Texure
        const string BASE_LAYER_NAME = "BaseLayer";
        const string HITBOX_LAYER_NAME = "HitBoxLayer";
        const string WALKEBLE_TILE_LAYER = "WalkebleLayer";

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

            //Hero
            mageHeroSpriteSheet = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\MageHero");

            //Enemies
            trollTrowerSpriteSheet = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Troll_Thrower");
            deathKnightSprite =      content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Death_Knight");
            zeppelinSprite =         content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Zeppelin");
            daemonSprite =           content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Daemon");
            dragonSprite =           content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Dragon");
            gruntSPrite =            content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Grunt");
            orgeSprite =             content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Orge");

            //Buildings
            enemieBuildnings = content.Load<Texture2D>(@"Assets\Texture\Buildings\EnemieBuildings");
            heroBuildings =    content.Load<Texture2D>(@"Assets\Texture\Buildings\HeroBuildings");
            etcBuildings =     content.Load<Texture2D>(@"Assets\Texture\Buildings\EtcBuildnings");
            heroBalista =      content.Load<Texture2D>(@"Assets\Texture\Units\Heros\Ballista");

            //Debug
            debugWalkTile = content.Load<Texture2D>(@"Assets\DebugTexture\Debug_CanWalk");
            debugRange =    content.Load<Texture2D>(@"Assets\DebugTexture\Debug_Range");

            //Effect, projectiles and explostion
            effecSprite =      content.Load<Texture2D>(@"Assets\Texture\MagicAndProjectiles\Particles_&_Effects");
            projectileSprite = content.Load<Texture2D>(@"Assets\Texture\MagicAndProjectiles\AttacksSprites");
            explosionSprite =  content.Load<Texture2D>(@"Assets\Texture\MagicAndProjectiles\Explosion");

            //Other
            blackPixel = content.Load<Texture2D>(@"Assets\Other\BlackPixel");
            whitePixel = content.Load<Texture2D>(@"Assets\Other\WhitePixel");
            //Add all textures and sprite to Dict
            AddTextureRegionToDict();
        }

        //----- Add texture -----//
        private void AddTextureRegionToDict()
        {
            //Exapmle:
            //textureRegion["night"] = new TextureRegion(standTiles, 0, 0, 1, 1);

            //Other
            textureRegion["BlackPixel"] = new TextureRegion(blackPixel, 0, 0, 1, 1);
            textureRegion["WhitePixel"] = new TextureRegion(whitePixel, 0, 0, 1, 1);

            //Debug
            AddSpriteSheetRegionsToDict(WALKEBLE_TILE_LAYER, debugWalkTile);
            textureRegion["DebugRange"] = new TextureRegion(debugRange, 0, 0, 64, 64);

            //UI

            //Tiles
            AddSpriteSheetRegionsToDict(BASE_LAYER_NAME, tileTextureSummer);

            //Enemies
            textureRegion["Troll_Thrower"] = new TextureRegion(trollTrowerSpriteSheet, 0, 0, trollTrowerSpriteSheet.Width, trollTrowerSpriteSheet.Height);
            textureRegion["Death_Knight"] =  new TextureRegion(deathKnightSprite,      0, 0, deathKnightSprite.Width,      deathKnightSprite.Height);
            textureRegion["Zeppelin"] =      new TextureRegion(zeppelinSprite,         0, 0, zeppelinSprite.Width,         zeppelinSprite.Height);
            textureRegion["Dragon"] =        new TextureRegion(dragonSprite,           0, 0, dragonSprite.Width,           dragonSprite.Height);
            textureRegion["Daemon"] =        new TextureRegion(daemonSprite,           0, 0, daemonSprite.Width,           daemonSprite.Height);
            textureRegion["Grunt"] =         new TextureRegion(gruntSPrite,            0, 0, gruntSPrite.Width,            gruntSPrite.Height);
            textureRegion["Orge"] =          new TextureRegion(orgeSprite,             0, 0, orgeSprite.Width,             orgeSprite.Height);

            //Explosion
            textureRegion["Big_Explosion"] =         new TextureRegion(explosionSprite,   0,   0, 512, 128);
            textureRegion["Medium_Explosion"] =      new TextureRegion(explosionSprite,   0, 128, 192, 128);
            textureRegion["Light_Magic_Explosion"] = new TextureRegion(explosionSprite, 192, 128, 192, 128);
            textureRegion["Dark_Magic_Explosion"] =  new TextureRegion(explosionSprite,   0, 256, 384,  64);
            textureRegion["Frost_Hit"] =             new TextureRegion(explosionSprite, 256, 192, 128,  64);
            textureRegion["Brown_Hit"] =             new TextureRegion(explosionSprite, 384, 128,  64,  64);
            textureRegion["Fire_Hit"] =              new TextureRegion(explosionSprite, 448, 128,  64,  64);

            //Projectiles
            textureRegion["Harpon"] =           new TextureRegion(projectileSprite,   0,   0, 280,  56);
            textureRegion["Soul_Tornado"] =     new TextureRegion(projectileSprite,   0,  56, 256,  64);
            textureRegion["Fire_Bal"] =         new TextureRegion(projectileSprite,   0, 120, 160,  32);
            textureRegion["Lighing_bal"] =      new TextureRegion(projectileSprite,   0, 152, 160,  32);
            textureRegion["Big_Canon_bal"] =    new TextureRegion(projectileSprite,   0, 184, 160,  32);
            textureRegion["Medium_Canon_Bal"] = new TextureRegion(projectileSprite, 160, 208, 120,  24);
            textureRegion["small_Canon_Bal"] =  new TextureRegion(projectileSprite, 160, 184, 120,  24);
            textureRegion["Evil_Hand"] =        new TextureRegion(projectileSprite,   0, 216, 160,  32);
            textureRegion["Arrow"] =            new TextureRegion(projectileSprite,   0, 248, 160,  32);
            textureRegion["Dark_Eye"] =         new TextureRegion(projectileSprite,   0, 280, 160,  32);
            textureRegion["Lightning_Axe"] =    new TextureRegion(projectileSprite, 160, 120,  96,  32);
            textureRegion["Normal_Axe"] =       new TextureRegion(projectileSprite, 160, 152,  96,  32);
            textureRegion["Blizzard"] =         new TextureRegion(projectileSprite, 160, 232, 128,  32);
            textureRegion["Fire_Canon_Bal"] =   new TextureRegion(projectileSprite, 160, 264,  64,  16);
            textureRegion["Canon_Ball"] =       new TextureRegion(projectileSprite, 160, 280,  48,  16);

            //Effects
            textureRegion["Fire_Storm"] =     new TextureRegion(effecSprite,   0,   0, 320, 128);
            textureRegion["Burning"] =        new TextureRegion(effecSprite,   0, 128, 192,  32);
            textureRegion["Magic_Particle"] = new TextureRegion(effecSprite,   0, 160, 192,  32);
            textureRegion["Soul_Spin"] =      new TextureRegion(effecSprite,   0, 192, 160,  32);
            textureRegion["Fire_Emit"] =      new TextureRegion(effecSprite,   0, 224,  96,  32);

            //Player Heros
            textureRegion["MageSheet"] = new TextureRegion(mageHeroSpriteSheet, 0, 0, mageHeroSpriteSheet.Width, mageHeroSpriteSheet.Height);

            //Hero Buildings
            textureRegion["Balista"] = new TextureRegion(heroBalista, 0, 0, heroBalista.Width, heroBalista.Height);

            textureRegion["Castle_lvl_1"] =        new TextureRegion(heroBuildings,   0,   0, 128, 128);
            textureRegion["Castle_lvl_1_Broken"] = new TextureRegion(heroBuildings,   0, 128, 128, 128);
            textureRegion["Castle_lvl_2"] =        new TextureRegion(heroBuildings, 128,   0, 128, 128);
            textureRegion["Castle_lvl_2_Broken"] = new TextureRegion(heroBuildings, 128, 128, 128, 128);

            textureRegion["HTower_1"] = new TextureRegion(heroBuildings, 256,  0, 64, 64);
            textureRegion["HTower_2"] = new TextureRegion(heroBuildings, 320,  0, 64, 64);
            textureRegion["HTower_3"] = new TextureRegion(heroBuildings, 256, 64, 64, 64);

            //Enemie Buildings
            textureRegion["DarkPortal_1"] =      new TextureRegion(enemieBuildnings,   0,   0, 128, 128);
            textureRegion["DarkPortal_2"] =      new TextureRegion(enemieBuildnings, 128,   0, 128, 128);
            textureRegion["DarkPortal_Broken"] = new TextureRegion(enemieBuildnings, 256,   0, 128, 128);

            textureRegion["Spawn_Altar"] =   new TextureRegion(enemieBuildnings,   0, 128,  98,  98);

            textureRegion["ETower"] =        new TextureRegion(enemieBuildnings,  98, 128,  64,  64);
            textureRegion["ETower_Broken"] = new TextureRegion(enemieBuildnings, 162, 128,  64,  64);

            //ETC Buildings
            textureRegion["Portal"] = new TextureRegion(etcBuildings, 0, 0, 64, 64);

        }
        private void AddSpriteSheetRegionsToDict(string LayerName, Texture2D spriteSheet)
        {
            int id = 0;
            for (int y = 0; y < spriteSheet.Height / 32; y++)
            {
                for (int x = 0; x < spriteSheet.Width / 32; x++)
                {
                    tileTextureRegion[Tuple.Create(LayerName, id)] = new TextureRegion(spriteSheet, x * 32, y * 32, 32, 32);
                    id++;
                }
            }

        }


        //----- Get Texture -----//
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
