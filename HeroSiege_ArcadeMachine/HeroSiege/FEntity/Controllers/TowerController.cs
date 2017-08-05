using HeroSiege.FEntity.Buildings;
using HeroSiege.FEntity.Buildings.EnemyBuildings;
using HeroSiege.FEntity.Buildings.HeroBuildings;
using HeroSiege.GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Controllers
{
    class TowerController : Control
    {
        float timer;
        public TowerController(World world, Building building)
            : base(world, building)
        {
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            timer += delta;

            if (building is HeroBallista)
                BallistaUpdate();

            else if (building is EnemyTower)
                EnemyTowerUpdate();
        }

        private void BallistaUpdate()
        {
            HeroBallista ballista = (HeroBallista)building;

            if (timer > ballista.AttackSpeed)
            {
                ballista.GetTargets(world.Enemies);
                if (ballista.GetTargetCount > 0)
                {
                    ballista.isAttaking = true;
                    ballista.SetPauseAnimation = false;
                    ballista.setAttackAnimation();
                }
                timer = 0;
            }
            if (ballista.isAttaking)
            {
                if (ballista.GetCurrentFrame == ballista.GetAttackFrame)
                {
                    ballista.CreateProjectilesTowardsTarget(world, FGameObject.ProjectileType.Harpon);
                    ballista.ClearTargets();
                    ballista.SetPauseAnimation = true;
                    ballista.setAttackAnimation();
                    ballista.isAttaking = false;

                }
            }
        }

        private void EnemyTowerUpdate()
        {
            EnemyTower tower = (EnemyTower)building;

            if (timer > tower.AttackSpeed)
            {
                tower.GetTargets(new List<Entity>() { world.PlayerOne, world.PlayerTwo });
                if (tower.GetTargetCount > 0)
                {
                    tower.isAttaking = true;
                }
                timer = 0;
            }
            if (tower.isAttaking)
            {
                tower.CreateProjectilesTowardsTarget(world, FGameObject.ProjectileType.Big_Canon_bal);
                tower.ClearTargets();
                tower.isAttaking = false;
            }

        }
    }


}
