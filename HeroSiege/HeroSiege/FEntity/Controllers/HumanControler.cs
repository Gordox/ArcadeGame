using HeroSiege.FEntity.Players;
using HeroSiege.FGameObject;
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

        public HumanControler(PlayerIndex playerIndex, Hero player, World world) : base(world, player) { this.playerIndex = playerIndex; }

        public override void Update(float delta)
        {
            if (player == null)
                return;

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (PressedButton(PlayerInput.A)) //Key M or numpad 3
                player.AButton(world);

            if (PressedButton(PlayerInput.B))  //Key J or numpad 6
                player.BButton(world);

            if (player.isBuying)
                return;

            if (PressedButton(PlayerInput.Green)) //Key G or numpad 4
                player.GreenButton(world);

            if (PressedButton(PlayerInput.Blue))  //Key H or numpad 5
                player.BlueButton(world);

            if (PressedButton(PlayerInput.Yellow))  //Key B or numpad 1
                player.YellowButton(world);

            if (PressedButton(PlayerInput.Red))  //Key N or numpad 2
                player.RedButton(world);

            
        }

        public void UpdateJoystick(float delta)
        {
            if (ButtonDown(PlayerInput.Up))
            {
                keysactive[0] = true;
                player.MoveUp(delta);
            }
            else
                keysactive[0] = false;

            if (ButtonDown(PlayerInput.Down))
            {
                keysactive[1] = true;
                player.MoveDown(delta);
            }
            else
                keysactive[1] = false;

            if (ButtonDown(PlayerInput.Left))
            {
                keysactive[2] = true;
                player.MoveLeft(delta);
            }
            else
                keysactive[2] = false;

            if (ButtonDown(PlayerInput.Right))
            {
                keysactive[3] = true;
                player.MoveRight(delta);
            }
            else
                keysactive[3] = false;

            UpdateMovingDirecion();

            if (!keysactive[0] && !keysactive[1] && !keysactive[2] && !keysactive[3] && !player.isAttaking &&
                !(player is GryphonRider) && !(player is GnomishFlyingMachine))
                player.SetPauseAnimation = true;
            else
                player.SetPauseAnimation = false;

        }

        public void UpdateMovingDirecion()
        {
            if (keysactive[0] && keysactive[3])
                player.MovingDirection = Direction.North_East;
            else if (keysactive[0] && keysactive[2])
                player.MovingDirection = Direction.North_West;
            else if (keysactive[1] && keysactive[3])
                player.MovingDirection = Direction.South_East;
            else if (keysactive[1] && keysactive[2])
                player.MovingDirection = Direction.South_West;

            else if (keysactive[0])
                player.MovingDirection = Direction.North;
            else if (keysactive[3])
                player.MovingDirection = Direction.East;
            else if (keysactive[1])
                player.MovingDirection = Direction.South;
            else if (keysactive[2])
                player.MovingDirection = Direction.West;


            
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
