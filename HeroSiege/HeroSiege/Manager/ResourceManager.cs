using HeroSiege.FTexture2D;
using HeroSiege.Manager.Resource;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Manager
{
    static class ResourceManager
    {
        private static TextureResource textures;
        private static FontResource fonts;
        /*
         * Sound
         * Audio
         * etc
         */

        public static void LoadResouces(ContentManager content)
        {
            textures = new TextureResource();
            fonts = new FontResource();
            textures.Load(content);
            fonts.Load(content);

        }


        public static TextureRegion GetTexture(string name)
        {
            return textures.GetTextureRegion(name);
        }

        public static TextureRegion GetTexture(string name, int id)
        {
            return textures.GetTextureRegion(name);
        }

        public static SpriteFont GetFont(string name)
        {
            return fonts.GetFont(name);
        }

        /// <summary>
        /// DO only when closing the programe
        /// </summary>
        public static void UnloadResources()
        {
            textures.UnloadTextures();
            fonts.UnloadFonts();
        }
    }
}
