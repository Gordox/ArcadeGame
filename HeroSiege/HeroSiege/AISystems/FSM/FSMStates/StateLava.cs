using HeroSiege.FEntity.Controllers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM.FSMStates
{
    class StateLava : FSMState
    {
        List<Vector2> lavaPosList;
        private const int LAVA_TO_SPAWN = 10;
        private const float INTERVAL = .6f;
        private float timer;

        private int nrLavaSpawned;

        public StateLava(Control parent)
            : base((int)FSMSTATES.FSM_STATE_Lava, parent) { lavaPosList = new List<Vector2>(); }

        public override void Enter()
        {
            base.Enter();
            isDone = false;
            nrLavaSpawned = 0;

            Rectangle area = ((BossController)parent).activateArea;

            for (int i = 0; i < LAVA_TO_SPAWN; i++)
                lavaPosList.Add(new Vector2(new Random(Guid.NewGuid().GetHashCode()).Next(area.X + 20, area.X + area.Width - 20), 
                                            new Random(Guid.NewGuid().GetHashCode()).Next(area.Y + 20, area.Y + area.Height - 20)));
        }

        public override void Exit()
        {
            base.Exit();
            if (lavaPosList != null)
                lavaPosList.Clear();
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            BossController bossControl = (BossController)parent;

            if ((int)(timer * 10 % 4) == 0 && lavaPosList.Count != 0)
                bossControl.world.SpawnEffect(FTexture2D.SpriteEffect.EffectType.Fire_Emit, lavaPosList[0]);

            timer += delta;

            if(timer >= INTERVAL)
            {
                timer = 0;
                bossControl.LavaAttack(lavaPosList[0]);
                nrLavaSpawned++;
                if (lavaPosList.Count != 0)
                    lavaPosList.RemoveAt(0);
            }

            if (nrLavaSpawned >= LAVA_TO_SPAWN)
                isDone = true;

        }



        public override int CheckTransitions()
        {
            //Add chance for special attacks here
            if (isDone)
                return (int)FSMSTATES.FSM_STATE_NormalAttack;

            return (int)FSMSTATES.FSM_STATE_Lava;
        }
    }
}
