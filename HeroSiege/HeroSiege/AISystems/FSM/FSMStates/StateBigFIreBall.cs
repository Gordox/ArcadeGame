using HeroSiege.FEntity.Controllers;
using HeroSiege.FEntity.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM.FSMStates
{
    class StateBigFIreBall : FSMState
    {
        const float BUILD_UP_TIME = 1.2f;
        float timer;

        public StateBigFIreBall(Control parent)
            : base((int)FSMSTATES.FSM_STATE_BigFireBal, parent)
        {
        }

        public override void Enter()
        {
            base.Enter();
            timer = 0;
            isDone = false;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            BossController bossControl = (BossController)parent;  

            if ((int)(timer * 10 % 4) == 0)
                bossControl.world.SpawnEffect(FTexture2D.SpriteEffect.EffectType.Fire_Emit, bossControl.enemy.Position);

            timer += delta;

            if (timer >= BUILD_UP_TIME)
            {
                bossControl.BigFireBallAttack(CalcDir(bossControl.enemy));
                isDone = true;
            }

            bossControl.debugText = "Big fire ball";
        }
        private float CalcDir(Enemy e)
        {
            if (e.PlayerTarget == null)
                return 0;

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

            return (int)FSMSTATES.FSM_STATE_BigFireBal;
        }
    }
}
