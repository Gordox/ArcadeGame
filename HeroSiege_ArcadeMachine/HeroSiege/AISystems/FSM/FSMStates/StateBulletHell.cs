using HeroSiege.FEntity.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM.FSMStates
{
    class StateBulletHell : FSMState
    {
        private const int COUNTS = 80;
        private const float INTERVAL = .12f;
        private int nrShoot;
        private float time, angleDir;
        
        public StateBulletHell(Control parent)
            : base((int)FSMSTATES.FSM_STATE_BulletHell, parent) { }

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

            if (time > INTERVAL && nrShoot <= COUNTS)
            {
                angleDir += 18;
                time = 0;
                bossControl.BulletHellAttack(angleDir);
                nrShoot++;
            }

            if (nrShoot >= COUNTS)
                isDone = true;

            bossControl.debugText = "Bullet Hell";
        }

        public override int CheckTransitions()
        {
            //Add chance for special attacks here
            if(isDone)
                return (int)FSMSTATES.FSM_STATE_NormalAttack;

            return (int)FSMSTATES.FSM_STATE_BulletHell;
        }
    }
}
