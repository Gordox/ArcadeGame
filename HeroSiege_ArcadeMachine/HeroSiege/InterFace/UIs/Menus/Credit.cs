using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroSiege.Manager;

namespace HeroSiege.InterFace.UIs.Menus
{
    class Credit : Menu
    {

        public Credit(Viewport viewPort)
            : base(viewPort)
        {
            Init();
        }

        private void Init()
        {
           
        }

        //----- Updates -----//
        public override void Update(float delta)
        {

        }
        protected override void UpdateSelected()
        {
           
        }
        protected override void UpdateSelectIndex(int i)
        {
            
        }

        //----- Draws -----//
        public override void Draw(SpriteBatch SB)
        {
            SB.Draw(ResourceManager.GetTexture("BG_2"), Vector2.Zero, Color.White);

            SB.Draw(ResourceManager.GetTexture("ImgOnDev"), new Vector2(50, 50),
               null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), "Credit",
                new Vector2(centerViewPort.X, 50), Color.Gold, 1);
            //centerViewPort
            string creditInfo = "Thank you for playing my game!\n" +
                "Develober name: Anton Svensson\nTwitter: @DsGordox\n"+
                "You can find the source code for the game here:\n"+
                "https://github.com/Gordox/ArcadeGame \n\n"+
                "Big disclaimer, I do not own any imeges, sprites, fonts and more exept for my own picture\n\n" +
                "All Warcraft 2 textures were taken form here:\nhttps://www.spriters-resource.com/pc_computer/warcraft2/\n\n"+
                "All Warcraft 3 texture I got my hand on did I get from using mod tools from:\nhttps://www.hiveworkshop.com/ \n\n"+
                "All other texures and more was found in the dark web\n\n\n\n\n"+
                "When I got the time, I will make a pc version of it so you can play it at home\nand have the rest of the option avalible too";

            DrawString(SB, ResourceManager.GetFont("WarFont_22"), creditInfo,
             new Vector2(350, 70), Color.Gold, 1);
        }

    }
}
