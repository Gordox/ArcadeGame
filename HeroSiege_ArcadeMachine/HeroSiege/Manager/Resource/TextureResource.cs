using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HeroSiege.Manager.Resource
{
    public class TextureResource
    {
        private Dictionary<string, TextureRegion> textureRegion; // Used for all the rest of the textures
        private Dictionary<Tuple<string, int>, TextureRegion> tileTextureRegion; // Used only to store tile texture region
        //Tiles
        private Texture2D tileTextureSummer;
        //Heros
        private Texture2D heroMage, heroArcher, heroFootMan, heroKnight, heroGryponRider, heroDwarven, heroGnomish;
        //Enemies
        private Texture2D trollTrowerSpriteSheet, demonSprite, deathKnightSprite, dragonSprite, gruntSprite, orgeSprite, zeppelinSprite, skeletonSprite,
            goblinSprite;
        //Buildings
        private Texture2D heroBuildings, enemieBuildnings, etcBuildings, heroBalista;
        //Magic and Missiels
        private Texture2D explosionSprite, projectileSprite, effecSprite;
        //----- UI -----//
        private Texture2D hudTexture, xpbarLayer_1, xpbarLayer_2, basicIcons, heroPortraits, shopWindow, ItemIcons, ItemInfoWindow;
        private Texture2D goldUI, screenSplitter, bigButton, startMenu, inGameMenu, eye;
        //Img
        private Texture2D imgStartScreen, imgDefeat, bg_1, bg_2, bg_3, bg_4, bg_5, bg_6, bg_7, bg_8, bg_9, bg_10;
        //Other
        private Texture2D leftDoor, rightDoor;

        //----- Other Textures -----//
        private Texture2D blackPixel, whitePixel, controlls, imgOnDev;
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

            //Heros
            heroMage = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\MageHero");
            heroArcher = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\ArcherHero");
            heroFootMan = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\FootManHero");
            heroKnight = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\KnightHero");
            heroGryponRider = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\GryphonRiderHero");
            heroDwarven = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\Dwarven");
            heroGnomish = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\Gnomish_Flying_Machine");
            //Enemies
            trollTrowerSpriteSheet = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Troll_Thrower");
            deathKnightSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Death_Knight");
            zeppelinSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Zeppelin");
            demonSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Daemon");
            dragonSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Dragon");
            gruntSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Grunt");
            orgeSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Orge");
            skeletonSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Skeleton");
            goblinSprite = content.Load<Texture2D>(@"Assets\Texture\Units\Enemies\Goblin_Sappers");

            //Buildings
            enemieBuildnings = content.Load<Texture2D>(@"Assets\Texture\Buildings\EnemieBuildings");
            heroBuildings = content.Load<Texture2D>(@"Assets\Texture\Buildings\HeroBuildings");
            etcBuildings = content.Load<Texture2D>(@"Assets\Texture\Buildings\EtcBuildnings");
            heroBalista = content.Load<Texture2D>(@"Assets\Texture\Units\Heros\Ballista");

            //Debug
            debugWalkTile = content.Load<Texture2D>(@"Assets\DebugTexture\Debug_CanWalk");
            debugRange = content.Load<Texture2D>(@"Assets\DebugTexture\Debug_Range");

            //Effect, projectiles and explostion
            effecSprite = content.Load<Texture2D>(@"Assets\Texture\MagicAndProjectiles\Particles_&_Effects_V2");
            projectileSprite = content.Load<Texture2D>(@"Assets\Texture\MagicAndProjectiles\AttacksSprites");
            explosionSprite = content.Load<Texture2D>(@"Assets\Texture\MagicAndProjectiles\Explosion");

            //--- UI ---//
            LoadInUITextures(content);

            LoadInBackGroundIMG(content);


            blackPixel = content.Load<Texture2D>(@"Assets\Other\BlackPixel");
            whitePixel = content.Load<Texture2D>(@"Assets\Other\WhitePixel");
            controlls = content.Load<Texture2D>(@"Assets\Other\Controlls");
            imgOnDev = content.Load<Texture2D>(@"Assets\Other\Me");
            //Add all textures and sprite to Dict
            AddTextureRegionToDict();
        }

        private void LoadInBackGroundIMG(ContentManager content)
        {
            bg_1 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_1");
            bg_2 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_2");
            bg_3 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_3");
            bg_4 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_4");
            bg_5 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_5");
            bg_6 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_6");
            bg_7 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_7");
            bg_8 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_8");
            bg_9 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_9");
            bg_10 = content.Load<Texture2D>(@"Assets\Texture\UI\BackgroundImg\Normal\Img_10");
        }
        private void LoadInUITextures(ContentManager content)
        {
            //Console
            hudTexture =     content.Load<Texture2D>(@"Assets\Texture\UI\Console\UI-HUD");
            ItemInfoWindow = content.Load<Texture2D>(@"Assets\Texture\UI\Console\Item_Info_UI");
            shopWindow =     content.Load<Texture2D>(@"Assets\Texture\UI\Console\ShopWindow");
            goldUI =         content.Load<Texture2D>(@"Assets\Texture\UI\Console\Money_UI");
            screenSplitter = content.Load<Texture2D>(@"Assets\Texture\UI\Console\ScreenSplitter");
            startMenu =      content.Load<Texture2D>(@"Assets\Texture\UI\Console\StartMenuBackground");
            inGameMenu =     content.Load<Texture2D>(@"Assets\Texture\UI\Console\InGameMenu");
            //Buttons
            bigButton =      content.Load<Texture2D>(@"Assets\Texture\UI\Buttons\BigButton");
            //Bars
            xpbarLayer_1 =   content.Load<Texture2D>(@"Assets\Texture\UI\Bars\xpbarBorderLayer_1");
            xpbarLayer_2 =   content.Load<Texture2D>(@"Assets\Texture\UI\Bars\xpbarBorderLayer_2");
            //Icons
            basicIcons =     content.Load<Texture2D>(@"Assets\Texture\UI\Icons\BasicIcons");
            ItemIcons =      content.Load<Texture2D>(@"Assets\Texture\UI\Icons\ItemIcons");
            //Potraits
            heroPortraits =  content.Load<Texture2D>(@"Assets\Texture\UI\Portraits\Portraits");
            //Img
            imgStartScreen = content.Load<Texture2D>(@"Assets\Texture\UI\Img\StartScreen");
            imgDefeat =      content.Load<Texture2D>(@"Assets\Texture\UI\Img\Defeat_Img");
            //Other
            eye =            content.Load<Texture2D>(@"Assets\Texture\UI\Other\Eye");
            leftDoor =       content.Load<Texture2D>(@"Assets\Texture\UI\Other\Door_left");
            rightDoor =      content.Load<Texture2D>(@"Assets\Texture\UI\Other\Door_Right_2");
        }

        //----- Add texture -----//
        private void AddTextureRegionToDict()
        {
            //Exapmle:
            //textureRegion["night"] = new TextureRegion(standTiles, 0, 0, 1, 1);

            //Other
            textureRegion["BlackPixel"] = new TextureRegion(blackPixel, 0, 0, 1, 1);
            textureRegion["WhitePixel"] = new TextureRegion(whitePixel, 0, 0, 1, 1);
            textureRegion["Controlls"] = new TextureRegion(controlls, 0, 0, controlls.Width, controlls.Height);
            textureRegion["ImgOnDev"] = new TextureRegion(imgOnDev, 0, 0, imgOnDev.Width, imgOnDev.Height);


            //Debug
            AddSpriteSheetRegionsToDict(WALKEBLE_TILE_LAYER, debugWalkTile);
            textureRegion["DebugRange"] = new TextureRegion(debugRange, 0, 0, 64, 64);
            //UI
            UIToDict();
            //BG
            BackGroundImgToDict();
            //Tiles
            AddSpriteSheetRegionsToDict(BASE_LAYER_NAME, tileTextureSummer);

            //Enemies
            textureRegion["Troll_Thrower"] = new TextureRegion(trollTrowerSpriteSheet, 0, 0, trollTrowerSpriteSheet.Width, trollTrowerSpriteSheet.Height);
            textureRegion["Death_Knight"] = new TextureRegion(deathKnightSprite, 0, 0, deathKnightSprite.Width, deathKnightSprite.Height);
            textureRegion["Zeppelin"] = new TextureRegion(zeppelinSprite, 0, 0, zeppelinSprite.Width, zeppelinSprite.Height);
            textureRegion["Skeleton"] = new TextureRegion(skeletonSprite, 0, 0, skeletonSprite.Width, skeletonSprite.Height);
            textureRegion["Dragon"] = new TextureRegion(dragonSprite, 0, 0, dragonSprite.Width, dragonSprite.Height);
            textureRegion["Goblin"] = new TextureRegion(goblinSprite, 0, 0, goblinSprite.Width, goblinSprite.Height);
            textureRegion["Demon"] = new TextureRegion(demonSprite, 0, 0, demonSprite.Width, demonSprite.Height);
            textureRegion["Grunt"] = new TextureRegion(gruntSprite, 0, 0, gruntSprite.Width, gruntSprite.Height);
            textureRegion["Orge"] = new TextureRegion(orgeSprite, 0, 0, orgeSprite.Width, orgeSprite.Height);

            //Explosion
            textureRegion["Light_Magic_Explosion"] = new TextureRegion(explosionSprite, 192, 128, 192, 128);
            textureRegion["Dark_Magic_Explosion"] = new TextureRegion(explosionSprite, 0, 256, 384, 64);
            textureRegion["Medium_Explosion"] = new TextureRegion(explosionSprite, 0, 128, 192, 128);
            textureRegion["Big_Explosion"] = new TextureRegion(explosionSprite, 0, 0, 512, 128);
            textureRegion["Frost_Hit"] = new TextureRegion(explosionSprite, 256, 192, 128, 64);
            textureRegion["Brown_Hit"] = new TextureRegion(explosionSprite, 384, 128, 64, 64);
            textureRegion["Fire_Hit"] = new TextureRegion(explosionSprite, 448, 128, 64, 64);

            //Projectiles
            textureRegion["Medium_Canon_Bal"] = new TextureRegion(projectileSprite, 160, 208, 120, 24);
            textureRegion["small_Canon_Bal"] = new TextureRegion(projectileSprite, 160, 184, 120, 24);
            textureRegion["Fire_Canon_Bal"] = new TextureRegion(projectileSprite, 160, 264, 64, 16);
            textureRegion["Lightning_Axe"] = new TextureRegion(projectileSprite, 160, 120, 96, 32);
            textureRegion["Big_Canon_bal"] = new TextureRegion(projectileSprite, 0, 184, 160, 32);
            textureRegion["Soul_Tornado"] = new TextureRegion(projectileSprite, 0, 56, 256, 64);
            textureRegion["Lighing_bal"] = new TextureRegion(projectileSprite, 0, 152, 160, 32);
            textureRegion["Normal_Axe"] = new TextureRegion(projectileSprite, 160, 152, 96, 32);
            textureRegion["Canon_Ball"] = new TextureRegion(projectileSprite, 160, 280, 48, 16);
            textureRegion["Evil_Hand"] = new TextureRegion(projectileSprite, 0, 216, 160, 32);
            textureRegion["Fire_Bal"] = new TextureRegion(projectileSprite, 0, 120, 160, 32);
            textureRegion["Dark_Eye"] = new TextureRegion(projectileSprite, 0, 280, 160, 32);
            textureRegion["Blizzard"] = new TextureRegion(projectileSprite, 160, 232, 128, 32);
            textureRegion["Harpon"] = new TextureRegion(projectileSprite, 0, 0, 280, 56);
            textureRegion["Arrow"] = new TextureRegion(projectileSprite, 0, 248, 160, 32);

            //Effects
            textureRegion["Fire_Storm"] = new TextureRegion(effecSprite, 0, 0, 320, 128);
            textureRegion["Burning"] = new TextureRegion(effecSprite, 0, 128, 192, 32);
            textureRegion["Magic_Particle"] = new TextureRegion(effecSprite, 0, 160, 192, 32);
            textureRegion["Soul_Spin"] = new TextureRegion(effecSprite, 0, 192, 160, 32);
            textureRegion["Fire_Emit"] = new TextureRegion(effecSprite, 0, 224, 96, 32);

            //Player Heros
            textureRegion["MageSheet"] = new TextureRegion(heroMage, 0, 0, heroMage.Width, heroMage.Height);
            textureRegion["ArcherSheet"] = new TextureRegion(heroArcher, 0, 0, heroArcher.Width, heroArcher.Height);
            textureRegion["FootManSheet"] = new TextureRegion(heroFootMan, 0, 0, heroFootMan.Width, heroFootMan.Height);
            textureRegion["KnightSheet"] = new TextureRegion(heroKnight, 0, 0, heroKnight.Width, heroKnight.Height);
            textureRegion["GryponRiderSheet"] = new TextureRegion(heroGryponRider, 0, 0, heroGryponRider.Width, heroGryponRider.Height);
            textureRegion["Dwarven"] =          new TextureRegion(heroDwarven, 0, 0, heroDwarven.Width, heroDwarven.Height);
            textureRegion["Gnomish"] =          new TextureRegion(heroGnomish, 0, 0, heroGnomish.Width, heroGnomish.Height);

            //Hero Buildings
            textureRegion["Balista"] = new TextureRegion(heroBalista, 0, 0, heroBalista.Width, heroBalista.Height);

            textureRegion["Castle_lvl_1"] = new TextureRegion(heroBuildings, 0, 0, 128, 128);
            textureRegion["Castle_lvl_1_Broken"] = new TextureRegion(heroBuildings, 0, 128, 128, 128);
            textureRegion["Castle_lvl_2"] = new TextureRegion(heroBuildings, 128, 0, 128, 128);
            textureRegion["Castle_lvl_2_Broken"] = new TextureRegion(heroBuildings, 128, 128, 128, 128);

            textureRegion["Shop_1"] = new TextureRegion(heroBuildings, 0, 256, 96, 96);
            textureRegion["Shop_2"] = new TextureRegion(heroBuildings, 96, 256, 96, 96);
            textureRegion["Shop_3"] = new TextureRegion(heroBuildings, 192, 256, 96, 96);
            textureRegion["Church"] = new TextureRegion(heroBuildings, 288, 256, 96, 96);

            textureRegion["HTower_1"] = new TextureRegion(heroBuildings, 256, 0, 64, 64);
            textureRegion["HTower_2"] = new TextureRegion(heroBuildings, 320, 0, 64, 64);
            textureRegion["HTower_3"] = new TextureRegion(heroBuildings, 256, 64, 64, 64);

            //Enemie Buildings
            textureRegion["DarkPortal_1"] = new TextureRegion(enemieBuildnings, 0, 0, 128, 128);
            textureRegion["DarkPortal_2"] = new TextureRegion(enemieBuildnings, 128, 0, 128, 128);
            textureRegion["DarkPortal_Broken"] = new TextureRegion(enemieBuildnings, 256, 0, 128, 128);

            textureRegion["Spawn_Altar"] = new TextureRegion(enemieBuildnings, 0, 128, 98, 98);

            textureRegion["ETower"] = new TextureRegion(enemieBuildnings, 98, 128, 64, 64);
            textureRegion["ETower_Broken"] = new TextureRegion(enemieBuildnings, 162, 128, 64, 64);

            //ETC Buildings
            textureRegion["Portal"] = new TextureRegion(etcBuildings, 0, 0, 64, 64);

        }

        private void BackGroundImgToDict()
        {
            textureRegion["BG_1"] =  new TextureRegion(bg_1,  0, 0, bg_1.Width, bg_1.Height);
            textureRegion["BG_2"] =  new TextureRegion(bg_2,  0, 0, bg_2.Width, bg_2.Height);
            textureRegion["BG_3"] =  new TextureRegion(bg_3,  0, 0, bg_3.Width, bg_3.Height);
            textureRegion["BG_4"] =  new TextureRegion(bg_4,  0, 0, bg_4.Width, bg_4.Height);
            textureRegion["BG_5"] =  new TextureRegion(bg_5,  0, 0, bg_5.Width, bg_5.Height);
            textureRegion["BG_6"] =  new TextureRegion(bg_6,  0, 0, bg_6.Width, bg_6.Height);
            textureRegion["BG_7"] =  new TextureRegion(bg_7,  0, 0, bg_7.Width, bg_7.Height);
            textureRegion["BG_8"] =  new TextureRegion(bg_8,  0, 0, bg_8.Width, bg_8.Height);
            textureRegion["BG_9"] =  new TextureRegion(bg_9,  0, 0, bg_9.Width, bg_9.Height);
            textureRegion["BG_10"] = new TextureRegion(bg_10, 0, 0, bg_10.Width, bg_10.Height);

        }
        private void UIToDict()
        {
            //Console
            textureRegion["HUDTexture"] =     new TextureRegion(hudTexture,     0, 0, hudTexture.Width, hudTexture.Height);
            textureRegion["ItemInfoWindow"] = new TextureRegion(ItemInfoWindow, 0, 0, ItemInfoWindow.Width, ItemInfoWindow.Height);
            textureRegion["ShopWindow"] =     new TextureRegion(shopWindow,     0, 0, shopWindow.Width, shopWindow.Height);
            textureRegion["GoldUI"] =         new TextureRegion(goldUI,         0, 0, goldUI.Width, goldUI.Height);
            textureRegion["ScreenSplitter"] = new TextureRegion(screenSplitter, 0, 0, screenSplitter.Width, screenSplitter.Height);
            textureRegion["StartMenu"] =      new TextureRegion(startMenu,      0, 0, startMenu.Width, startMenu.Height);
            textureRegion["InGameMenu"] =      new TextureRegion(inGameMenu,     0, 0, inGameMenu.Width, inGameMenu.Height);
            //Buttons
            textureRegion["BigButton"] =      new TextureRegion(bigButton,      0, 0, bigButton.Width, bigButton.Height);
            //Bars
            textureRegion["XpBarLayer_1"] =   new TextureRegion(xpbarLayer_1,   0, 0, xpbarLayer_1.Width, xpbarLayer_1.Height);
            textureRegion["XpBarLayer_2"] =   new TextureRegion(xpbarLayer_2,   0, 0, xpbarLayer_2.Width, xpbarLayer_2.Height);
            //Icons
            textureRegion["StatsIcons"] =     new TextureRegion(basicIcons,     0, 0, basicIcons.Width, basicIcons.Height);
            textureRegion["ItemIcons"] =      new TextureRegion(ItemIcons,      0, 0, ItemIcons.Width, ItemIcons.Height);
            //Potraits
            textureRegion["SoldierPortraits"] = new TextureRegion(heroPortraits,   0,  0, 96, 96);
            textureRegion["KnightPortraits"] =  new TextureRegion(heroPortraits,  96,  0, 96, 96);
            textureRegion["MagePortraits"] =    new TextureRegion(heroPortraits,   0, 96, 96, 96);
            textureRegion["ArcherPortraits"] =  new TextureRegion(heroPortraits,  96, 96, 96, 96);
            textureRegion["DwarfPortraits"] =   new TextureRegion(heroPortraits, 192,  0, 96, 96);
            textureRegion["GryphonPortraits"] = new TextureRegion(heroPortraits, 288,  0, 96, 96);
            textureRegion["GnomePortraits"] =   new TextureRegion(heroPortraits, 192,  96, 96, 96);
            //Img  
            textureRegion["ImgStartScreen"] =   new TextureRegion(imgStartScreen, 0, 0, imgStartScreen.Width, imgStartScreen.Height);
            textureRegion["ImgDefeat"] =        new TextureRegion(imgDefeat,      0, 0, imgDefeat.Width, imgDefeat.Height);
            //Other
            textureRegion["LeftDoor"] =  new TextureRegion(leftDoor,  0, 0, leftDoor.Width, leftDoor.Height);
            textureRegion["RightDoor"] = new TextureRegion(rightDoor, 0, 0, rightDoor.Width, rightDoor.Height);
            textureRegion["Eye"] = new TextureRegion(eye, 0, 0, eye.Width, eye.Height);
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
