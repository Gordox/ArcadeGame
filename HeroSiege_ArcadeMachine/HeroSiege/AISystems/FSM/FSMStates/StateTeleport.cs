using HeroSiege.FEntity.Controllers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM.FSMStates
{
    class StateTeleport : FSMState
    {
        const float DELAYTIME = 0.8f;
        float timer;
        Vector2 teleportTo;
        bool canTeleport, isGone, canAppear, canDisapear;

        public StateTeleport(Control parent)
            : base((int)FSMSTATES.FSM_STATE_Teleport, parent) { }

        public override void Enter()
        {
            base.Enter();
            isDone = false;
            canTeleport = isGone = canAppear = false;
            canDisapear = true;
            timer = 0;
            teleportTo = Vector2.Zero;
        }


        public override void Update(float delta)
        {
            base.Update(delta);
            BossController bossControl = (BossController)parent;

            if (canDisapear)
            {
                bossControl.world.SpawnEffect(FTexture2D.SpriteEffect.EffectType.Fire_Storm, bossControl.enemy.Position);
                canDisapear = false;
            }

            if (!canDisapear)
                timer += delta;

            if(!canTeleport)
            {
                Rectangle area = bossControl.activateArea;
                teleportTo.X = new Random(Guid.NewGuid().GetHashCode()).Next(area.X + 20, area.X + area.Width - 20);
                teleportTo.Y = new Random(Guid.NewGuid().GetHashCode()).Next(area.Y + 20, area.Y + area.Height - 20);
                canTeleport = true;
            }

            if(canTeleport && !canAppear)
            {
                bossControl.world.SpawnEffect(FTexture2D.SpriteEffect.EffectType.Fire_Storm, teleportTo);
                canAppear = true;
            }

            if (canAppear && timer >= DELAYTIME)
            {
                bossControl.enemy.SetPosition(teleportTo);
                bossControl.TeleportAttack();
                isDone = true;
            }

            bossControl.debugText = "Teleport";
        }

        public override int CheckTransitions()
        {
            //Add chance for special attacks here
            if (isDone)
                return (int)FSMSTATES.FSM_STATE_NormalAttack;

            return (int)FSMSTATES.FSM_STATE_Teleport;
        }
    }
}
