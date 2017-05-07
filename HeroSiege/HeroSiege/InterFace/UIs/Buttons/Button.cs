using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeroSiege.FTexture2D;

namespace HeroSiege.InterFace.UIs.Buttons
{
    public enum ButtonState
    {
        Active,
        pressed,
        inActive
    }

    abstract class Button : UI
    {
        public bool ButtonActive { get; set; }
        public bool Selected { get; set; }

        public ButtonState ButtonState { get; set; }

        public float Size { get; set; }
        public Color color { get; set; }

        protected TextureRegion activeButton, buttonPressed, inActiveButton, selected, checkedButton;

        public Vector2 Position { get { return position; } }
        protected Vector2 position;

        public Button(Vector2 pos, bool buttonActive = true)
        {
            this.position = pos;
            this.ButtonActive = buttonActive;
            this.ButtonState = ButtonState.Active;
            Init();
        }

        public void ButtonDown()
        {
            ButtonState = ButtonState.pressed;
        }
        public void ButtonUp()
        {
            ButtonState = ButtonState.Active;
        }

        protected virtual void Init()
        {
            InitTexture();
            Size = 1;
            color = Color.White;
        }


        protected abstract void InitTexture();


    }
}
