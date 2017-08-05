using HeroSiege.AISystems.FSM.FSMStates;
using HeroSiege.FEntity.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM
{
    class FSMMachine : FSMState
    {
        protected List<FSMState> states;
        protected FSMState currentState;
        protected FSMState defaultSate;
        protected FSMState goalState;
        protected int goalID;


        public FSMMachine(int type = (int)FSMSTATES.FSM_STATE_None, Control parent = null)
            : base(type, parent)
        {
            this.states = new List<FSMState>();
            this.currentState = null;
            this.defaultSate = null;
            this.goalState = null;
        }

        public virtual void AddState(FSMState state) { states.Add(state); }
        public virtual FSMState GetRandomState() { return states[new Random().Next(states.Count)]; }

        public virtual void SetDefaultState(FSMState state) { defaultSate = state; }
        public virtual void SetGoalID(int goal) { goalID = goal; }

        public virtual bool TransitionState(int goal)
        {
            if (states.Count == 0)
                return false;

            for (int i = 0; i < states.Count; i++)
            {
                if (states[i].type == goal)
                {
                    goalState = states[i];
                    return true;
                }
            }
            return false;
        }

        public virtual void UpdateMachine(float delta)
        {
            if (states.Count == 0)
                return;

            //don’t do anything if there’s no current
            //state, and no default state
            if (currentState == null)
                currentState = defaultSate;
            if (currentState == null)
                return;

            //update current state, and check for a transition
            int oldStateID = currentState.type;
            goalID = currentState.CheckTransitions();

            //switch if there was a transition
            if (goalID != oldStateID)
            {
                if (TransitionState(goalID))
                {
                    currentState.Exit();
                    currentState = goalState;
                    currentState.Enter();
                }
            }
            currentState.Update(delta);
        }

        public virtual void Reset()
        {
            Exit();
            if (currentState != null)
                currentState.Exit();
            currentState = defaultSate;

            //init all the states
            for (int i = 0; i < states.Count; i++)
                states[i].Init();

            //and now enter the m_defaultState, if any
            if (currentState != null)
                currentState.Enter();
        }
    }
}
