using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.GameWorld;
using HeroSiege.FEntity.Enemies;
using Microsoft.Xna.Framework;
using HeroSiege.FEntity.Players;
using HeroSiege.FEntity.Buildings;

namespace HeroSiege.FEntity.Controllers
{
    class AIController : Control
    {

        const float UPDATE_PATH_TIMER = 0.5f;
        const float UPDATE_Target_TIMER = 0.5f;
        float pathTimer, targetTimer;

        public float NearestDist { get; private set; }
        public bool hasTarget {get; private set; }


        public AIController(World world, Enemy enemy)
            : base(world, enemy)
        {
            hasTarget = false;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            targetTimer += delta;
            pathTimer += delta;


            if (pathTimer > UPDATE_PATH_TIMER && enemy.BuildingTarget == null || pathTimer > UPDATE_PATH_TIMER && enemy.PlayerTarget == null)
            {
                hasTarget = isPlayerInRange(new List<Hero>() { world.PlayerOne, world.PlayerTwo });
                if(!hasTarget)
                    hasTarget = isBuildingInRange(world.HeroBuildings);

                pathTimer = 0;
            }

            UpdateBehavior(delta);
        }

        private void UpdateBehavior(float delta)
        {
            if (enemy.PlayerTarget != null && !IsWithInAttackRange()) //Walks towards player if it has a player target with in range
                UpdatePathToTarget(delta);

            if (IsWithInAttackRange()) //Enemy will attack if the player is within the attack range
            {
                enemy.ClearWaypoints();

                if (enemy.AttackType == AttackType.Range)
                    RangeAttack();
                else
                    MeleeAttack();
            }
            else
                SetDestinationToCastle();

            if(!isPlayerInRange(new List<Hero>() { world.PlayerOne, world.PlayerTwo }))
            {
                enemy.PlayerTarget = null;
            }
        }



        //----- Destinatins -----//
        private void UpdatePathToTarget(float delta)
        {
            pathTimer += delta;
            if(pathTimer > UPDATE_PATH_TIMER)
            {
                enemy.Astar(world.Map, new Point((int)enemy.PlayerTarget.Position.X, (int)enemy.PlayerTarget.Position.Y));
                pathTimer = 0;
            }
        }
        private void SetDestinationToCastle()
        {


            if(enemy.havePath == false)
            {
                enemy.Astar(world.Map, new Point((int)world.Map.HeroCastle.X,
                                                           (int)world.Map.HeroCastle.Y));
            }
        }

        //----- Range -----//
        private bool IsWithInAttackRange()
        {
            if (enemy.PlayerTarget != null)
            {
                float lenght = Vector2.Distance(enemy.PlayerTarget.Position, enemy.Position);
                if (lenght <= enemy.Stats.Radius)
                    return true;
            }
            else if (enemy.BuildingTarget != null)
            {
                float lenght = Vector2.Distance(enemy.BuildingTarget.Position, enemy.Position);
                if (lenght <= enemy.Stats.Radius)
                    return true;
            }


            return false;
        }
        private bool isPlayerInRange(List<Hero> players)
        {
            enemy.PlayerTarget = null;
            NearestDist = 100000;
            bool foundTarget = false;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == null || !players[i].IsAlive)
                    continue;

                if(enemy.Stats.visibilityRadius > Vector2.Distance(players[i].Position, enemy.Position))
                {
                    float lenght = Vector2.Distance(players[i].Position, enemy.Position);
                    if (lenght < NearestDist)
                    {
                        NearestDist = lenght;
                        enemy.PlayerTarget = players[i];
                        foundTarget = true;
                    }
                }             
            }

            if (foundTarget)
                return true;
            else
                return false;
        }
        private bool isBuildingInRange(List<Building> heroBuildings)
        {
            enemy.BuildingTarget = null;
            NearestDist = 100000;
            bool foundTarget = false;
            for (int i = 0; i < heroBuildings.Count; i++)
            {
                if (heroBuildings[i] == null || !heroBuildings[i].IsAlive)
                    continue;

                if (enemy.Stats.visibilityRadius > Vector2.Distance(heroBuildings[i].Position, enemy.Position))
                {
                    float lenght = Vector2.Distance(heroBuildings[i].Position, enemy.Position);
                    if (lenght < NearestDist)
                    {
                        NearestDist = lenght;
                        enemy.BuildingTarget = heroBuildings[i];
                        foundTarget = true;
                    }
                }
            }

            if (foundTarget)
                return true;
            else
                return false;
        }
        //----- Attack -----//
        private void RangeAttack()
        {
            if (targetTimer > enemy.AttackSpeed && !enemy.isAttaking)
            {
                enemy.isAttaking = true;
                enemy.SetAttackAnimation();
            }
            if (enemy.isAttaking && enemy.GetCurrentFrame == enemy.GetAttackFrame)
            {
                enemy.CreateProjectilesTowardsTarget(world, enemy.ProjectType);
                enemy.isAttaking = false;
                enemy.SetMovmentAnimation();
                hasTarget = false;
                enemy.PlayerTarget = null;
                targetTimer = 0;
            }


        }
        private void MeleeAttack() { }
    }
}
