using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.InterFace.UIs.Menus
{
    abstract class Menu : UI
    {
        protected Vector2 centerViewPort;
        protected Viewport viewPort;
        protected int currentIndex, oldIndex;
        public Menu() { }
        public Menu(Viewport viewPort) { centerViewPort = new Vector2(viewPort.Width / 2, viewPort.Height / 2); this.viewPort = viewPort; }

        protected abstract void UpdateSelected();
        protected abstract void UpdateSelectIndex(int i);

        protected virtual void UpdateJoystick()
        {
            if (ButtonPress(PlayerIndex.One, PlayerInput.Down) || ButtonPress(PlayerIndex.Two, PlayerInput.Down))
                UpdateSelectIndex(1);
            else if (ButtonPress(PlayerIndex.One, PlayerInput.Up) || ButtonPress(PlayerIndex.Two, PlayerInput.Up))
                UpdateSelectIndex(-1);
        }

        protected bool ButtonDown(PlayerIndex index, PlayerInput b)
        {
            return InputHandler.GetButtonState(index, b) == InputState.Down;
        }
        protected bool ButtonPress(PlayerIndex index, PlayerInput b)
        {
            return InputHandler.GetButtonState(index, b) == InputState.Released;
        }
    }
}
