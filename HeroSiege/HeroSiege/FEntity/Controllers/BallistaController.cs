using HeroSiege.FEntity.Buildings;
using HeroSiege.FEntity.Buildings.HeroBuildings;
using HeroSiege.GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Controllers
{
    class BallistaController : Control
    {
        float timer;
        public BallistaController(World world, Building building)
            : base(world, building)
        {
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            timer += delta;

            if(timer > ((HeroBallista)building).AttackSpeed)
            {
                HeroBallista temp = (HeroBallista)building;
                temp.GetTargets(world.Enimies);
                if (temp.GetTargetCount > 0)
                {
                    temp.isAttaking = true;
                    temp.setAttackAnimation();
                }
                timer = 0;
            }

            if (((HeroBallista)building).isAttaking)
            {
                HeroBallista temp = (HeroBallista)building;
                if (temp.GetCurrentFrame == temp.GetAttackFrame)
                {
                    temp.CreateProjectilesTowardsTarget(world, FGameObject.ProjectileType.Harpon);
                    temp.ClearTargets();
                    temp.setIdleTexture();
                    temp.isAttaking = false;

                }
            }
        }

       
    }
}
