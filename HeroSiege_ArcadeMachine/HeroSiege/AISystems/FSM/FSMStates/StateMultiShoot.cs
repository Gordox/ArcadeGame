using HeroSiege.FEntity.Controllers;
using HeroSiege.FEntity.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM.FSMStates
{
    class StateMultiShoot : FSMState
    {
        private const int COUNTS = 70;
        private int nrShoot;
        private float time, angleDir;
        public StateMultiShoot(Control parent)
            : base((int)FSMSTATES.FSM_STATE_MultiShoot, parent) { }

        public override void Enter()
        {
            base.Enter();
            this.time = 0;
            this.angleDir = 0;
            this.nrShoot = 0;
            this.isDone = false;
        }


        public override void Update(float delta)
        {
            base.Update(delta);

            BossController bossControl = (BossController)parent;
            time += delta;

            if (time > .12f && nrShoot <= COUNTS && bossControl.enemy.PlayerTarget != null)
            {
                angleDir = CalcDir(bossControl.enemy);
                bossControl.MultiShoot(angleDir);
                nrShoot++;
                time = 0;
            }

            if (nrShoot >= COUNTS)
                isDone = true;


            bossControl.debugText = "Multi shoot";
        }

        private float CalcDir(Enemy e)
        {
            Vector2 movingDirection = new Vector2(e.PlayerTarget.Position.X - e.Position.X, e.PlayerTarget.Position.Y - e.Position.Y);
            movingDirection.Normalize();
            float temp = (float)Math.Atan2(-movingDirection.Y, movingDirection.X);
            return MathHelper.ToDegrees(temp);
        }

        public override int CheckTransitions()
        {
            //Add chance for special attacks here
            if (isDone)
                return (int)FSMSTATES.FSM_STATE_NormalAttack;

            return (int)FSMSTATES.FSM_STATE_MultiShoot;
        }
    }
}
