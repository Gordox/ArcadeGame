using HeroSiege.FEntity.Controllers;
using HeroSiege.FEntity.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM.FSMStates
{
    class StateNormalAttack : FSMState
    {
        FSMMachine machine;
        private float interval, timer;
        public StateNormalAttack(Control parent, FSMMachine machine)
            : base((int)FSMSTATES.FSM_STATE_NormalAttack, parent)
        {
            this.machine = machine;
        }

        public override void Enter()
        {
            base.Enter();
            timer = 0;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            timer += delta;
            BossController bossControl = (BossController)parent;

            if (bossControl.enemy.PlayerTarget != null && !bossControl.IsWithInAttackRange()) //Walks towards player if it has a player target with in range
                bossControl.UpdatePathToTarget(delta);

            if (bossControl.IsWithInAttackRange()) //Enemy will attack if the player is within the attack range
            {
                bossControl.enemy.ClearWaypoints();
                bossControl.NormalAttack();
            }

            if (!bossControl.isPlayerInRange(new List<Hero>() { bossControl.world.PlayerOne, bossControl.world.PlayerTwo }))
            {
                bossControl.enemy.PlayerTarget = null;
                bossControl.SetTarget = false;
            }

            bossControl.debugText = "Normal attack";
        }


        public override int CheckTransitions()
        {
            BossController bossControl = (BossController)parent;

            if (bossControl.enemy.Stats.Health > bossControl.enemy.Stats.MaxHealth * .75)
                interval = 7;
            else if (bossControl.enemy.Stats.Health > bossControl.enemy.Stats.MaxHealth * .50)
                interval = 5;
            else if (bossControl.enemy.Stats.Health > bossControl.enemy.Stats.MaxHealth * .25)
                interval = 3;

            if(timer > interval)
            {
                FSMState temp = this;
                while(temp == this)
                    temp = machine.GetRandomState();

                return temp.type;
            }

            return (int)FSMSTATES.FSM_STATE_NormalAttack;
        }

    }
}
