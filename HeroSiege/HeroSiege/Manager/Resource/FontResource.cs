using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Manager.Resource
{
    public class FontResource
    {
        private Dictionary<string, SpriteFont> fonts;

        public void Load(ContentManager content)
        {
            this.fonts = new Dictionary<string, SpriteFont>();
            LoadAssets(content);
        }
        private void LoadAssets(ContentManager content)
        {
            //Exaple:
            //fonts["Arial_Font"] = content.Load<SpriteFont>(@"Assets\Fonts\Arial_Font");

            fonts["Arial_Font"] = content.Load<SpriteFont>(@"Assets\Fonts\Arial_Font");


        }

        public SpriteFont GetFont(string name)
        {
            try
            {
                return fonts[name];
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Remove all fonts
        /// DO Only before closing the program
        /// </summary>
        public void UnloadFonts()
        {
            fonts.Clear();
        }
    }
}
