using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FTexture2D;
using HeroSiege.Manager;

namespace HeroSiege.InterFace.UIs.Buttons
{
    class BigButton : Button
    {

        string text = string.Empty;

        public BigButton(Vector2 pos, string text = "", bool buttonActive = true)
            : base(pos, buttonActive)
        {
            this.text = text;
        }
        protected override void InitTexture()
        {
            activeButton = new TextureRegion(ResourceManager.GetTexture("BigButton"),   0,   0, 162, 32);
            buttonPressed = new TextureRegion(ResourceManager.GetTexture("BigButton"),  0,  32, 162, 32);
            inActiveButton = new TextureRegion(ResourceManager.GetTexture("BigButton"), 0,  64, 162, 32);
            selected = new TextureRegion(ResourceManager.GetTexture("BigButton"),       0,  96, 162, 32);
        }

        public override void Update(float delta)
        {
            
        }

        public override void Draw(SpriteBatch SB)
        {
            switch (ButtonState)
            {
                case ButtonState.Active:
                    SB.Draw(activeButton, Position, activeButton, color, 0, Vector2.Zero, Size, SpriteEffects.None, 0);
                    break;
                case ButtonState.pressed:
                    SB.Draw(buttonPressed, Position, buttonPressed, color, 0, Vector2.Zero, Size, SpriteEffects.None, 0);
                    break;
                case ButtonState.inActive:
                    SB.Draw(inActiveButton, Position, inActiveButton, color, 0, Vector2.Zero, Size, SpriteEffects.None, 0);
                    break;
                default:
                    break;
            }

            if(Selected)
                SB.Draw(selected, Position, selected, color * .7f, 0, Vector2.Zero, Size, SpriteEffects.None, 0);


        }

        public void DrawCenter(SpriteBatch SB)
        {
            switch (ButtonState)
            {
                case ButtonState.Active:
                    SB.Draw(activeButton, Position, activeButton, color, 0, new Vector2(activeButton.region.Width / 2, 0), Size, SpriteEffects.None, 0);
                    break;
                case ButtonState.pressed:
                    SB.Draw(buttonPressed, Position, buttonPressed, color, 0, new Vector2(buttonPressed.region.Width / 2, 0), Size, SpriteEffects.None, 0);
                    break;
                case ButtonState.inActive:
                    SB.Draw(inActiveButton, Position, inActiveButton, color, 0, new Vector2(inActiveButton.region.Width / 2, 0), Size, SpriteEffects.None, 0);
                    break;
                default:
                    break;
            }

            if (Selected)
                SB.Draw(selected, Position, selected, color * .5f, 0, new Vector2(selected.region.Width / 2, 0), Size, SpriteEffects.None, 0);

            DrawCenterString(SB, ResourceManager.GetFont("WarFont_32"), text, Position + new Vector2(0, activeButton.region.Height + 15), Color.Gold, 1);
        }


    }
}
