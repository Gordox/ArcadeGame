using HeroSiege.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Controllers
{
    class HumanControler : Control
    {
        public PlayerIndex playerIndex { get; private set; }
        private bool[] keysactive = new bool[4];

        public HumanControler(PlayerIndex playerIndex, Entity entity, World world) : base(world, entity) { this.playerIndex = playerIndex; }

        public override void Update(float delta)
        {
            if (entity == null)
                return;

            UpdateJoystick(delta);
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (PressedButton(PlayerInput.Green)) //Key G or numpad 4
                entity.GreenButton(world);

            if (PressedButton(PlayerInput.Blue))  //Key H or numpad 5
                entity.BlueButton(world);

            if (PressedButton(PlayerInput.Yellow))  //Key B or numpad 1
                entity.YellowButton(world);

            if (PressedButton(PlayerInput.Red))  //Key N or numpad 2
                entity.RedButton(world);

            if (PressedButton(PlayerInput.A)) //Key M or numpad 3
                entity.AButton(world);

            if (PressedButton(PlayerInput.B))  //Key J or numpad 6
                entity.BButton(world);
        }

        private void UpdateJoystick(float delta)
        {
            if (ButtonDown(PlayerInput.Up))
            {
                keysactive[0] = true;
                entity.MoveUp(delta);
            }
            else
                keysactive[0] = false;

            if (ButtonDown(PlayerInput.Down))
            {
                keysactive[1] = true;
                entity.MoveDown(delta);
            }
            else
                keysactive[1] = false;

            if (ButtonDown(PlayerInput.Left))
            {
                keysactive[2] = true;
                entity.MoveLeft(delta);
            }
            else
                keysactive[2] = false;

            if (ButtonDown(PlayerInput.Right))
            {
                keysactive[3] = true;
                entity.MoveRight(delta);
            }
            else
                keysactive[3] = false;

            if (!keysactive[0] && !keysactive[1])
                entity.NoMovementY();
            if (!keysactive[2] && !keysactive[3])
                entity.NoMovementX();
        }

        public bool ButtonDown(PlayerInput button)
        {
            return InputHandler.GetButtonState(playerIndex, button) == InputState.Down;
        }

        public bool PressedButton(PlayerInput button)
        {
            return InputHandler.GetButtonState(playerIndex, button) == InputState.Released;
        }
    }
}
