using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems.FSM
{

    public enum FSMSTATES
    {
        FSM_STATE_None,
        FSM_STATE_NormalAttack,
        FSM_STATE_BulletHell,
        FSM_STATE_BigFireBal,
        FSM_STATE_Teleport,
        FSM_STATE_Wall,
        FSM_STATE_Lava,
        FSM_STATE_MultiShoot
    }
}
