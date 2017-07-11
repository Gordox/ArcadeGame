using HeroSiege.FEntity.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM.FSMStates
{
    class FSMState
    {
        public Control parent;
        public int type;
        public bool isDone;

        public FSMState(int type = (int)FSMSTATES.FSM_STATE_None, Control parent = null)
        {
            this.type = type;
            this.parent = parent;
        }

        public virtual void Init() { }
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual int CheckTransitions() { return (int)FSMSTATES.FSM_STATE_None; }
        public virtual void Update(float delta) { }


    }
}
