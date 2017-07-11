using HeroSiege.AISystems.FSM;
using HeroSiege.AISystems.FSM.FSMStates;
using HeroSiege.FEntity.Enemies;
using HeroSiege.FEntity.Players;
using HeroSiege.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Controllers
{
    class BossController : Control
    {

        public float NearestDist { get; private set; }
        public bool hasTarget { get; private set; }

        public Rectangle activateArea { get; private set; }
        private bool active;

        public const float UPDATE_PATH_TIMER = 0.5f;
        public const float UPDATE_Target_TIMER = 0.5f;
        public float pathTimer, targetTimer;

        public string debugText;

        //FSM
        FSMMachine machine;

        public BossController(World world, Enemy enemy, Rectangle activateArea)
            : base(world, enemy)
        {
            this.activateArea = activateArea;
            active = false;
            hasTarget = false;
            init();
        }

        private void init()
        {
            InitAttackStates();
        }

        private void InitAttackStates()
        {
            machine = new FSMMachine(1, this);
            List<FSMState> attackList = new List<FSMState>();

            attackList.Add(new StateBulletHell(this));
            attackList.Add(new StateMultiShoot(this));
            attackList.Add(new StateTeleport(this));
            attackList.Add(new StateBigFIreBall(this));
            attackList.Add(new StateLava(this));


            StateNormalAttack normAttack = new StateNormalAttack(this, machine);
            machine.AddState(normAttack);
            machine.SetDefaultState(normAttack);

            if (enemy is Enemies.Bosses.Demon)
                SetDemonAttacks(attackList);
            else
                SetFinalBossAttack(attackList);

            machine.Reset();
        }

        private void SetDemonAttacks(List<FSMState> attackList)
        {
            for (int i = 0; i < 3; i++)
            {
                int rnd = new Random(Guid.NewGuid().GetHashCode()).Next(attackList.Count);
                machine.AddState(attackList[rnd]);
                attackList.RemoveAt(rnd);
            }
        }
        private void SetFinalBossAttack(List<FSMState> attackList)
        {
            for (int i = 0; i < attackList.Count; i++)
            {
                attackList.RemoveAt(i);
            }
        }

        //----- Update -----//
        public override void Update(float delta)
        {
            if (active == false) { checkPlayer(delta); return; }
            base.Update(delta);

            UpdatePerceptions(delta);
            machine.UpdateMachine(delta);
        }

        private void UpdatePerceptions(float delta)
        {
            targetTimer += delta;
            pathTimer += delta;

            if (pathTimer > UPDATE_PATH_TIMER && enemy.PlayerTarget == null)
            {
                hasTarget = isPlayerInRange(new List<Hero>() { world.PlayerOne, world.PlayerTwo });
                pathTimer = 0;
            }

        }

        float checktimer = .2f, timer;
        /// <summary>
        /// Check if the player/s is inside the activation bounds
        /// or damage the demon to activate it
        /// </summary>
        private void checkPlayer(float delta)
        {
            timer += delta;
            if (timer > checktimer)
            {
                if (world.PlayerOne != null && world.PlayerOne.GetBounds().Intersects(activateArea) ||
                   world.PlayerTwo != null && world.PlayerTwo.GetBounds().Intersects(activateArea))
                    active = true;

                if (enemy.Stats.MaxHealth > enemy.Stats.Health)
                    active = true;

                timer = 0;
            }

        }

        //----- Destinatins -----//
        public void UpdatePathToTarget(float delta)
        {
            pathTimer += delta;
            if (pathTimer > UPDATE_PATH_TIMER)
            {
                enemy.Astar(world.Map, new Point((int)enemy.PlayerTarget.Position.X, (int)enemy.PlayerTarget.Position.Y));
                pathTimer = 0;
            }
            enemy.SetPauseAnimation = false;
        }

        //----- Range -----//
        public bool IsWithInAttackRange()
        {
            if (enemy.PlayerTarget != null)
            {
                float lenght = Vector2.Distance(enemy.PlayerTarget.Position, enemy.Position);
                if (lenght <= enemy.Stats.Radius)
                    return true;
            }
          
            return false;
        }
        public bool isPlayerInRange(List<Hero> players)
        {
            enemy.PlayerTarget = null;
            NearestDist = 100000;
            bool foundTarget = false;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == null || !players[i].IsAlive)
                    continue;

                if (enemy.Stats.visibilityRadius > Vector2.Distance(players[i].Position, enemy.Position))
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

        //----- Attacks -----//
        public void NormalAttack()
        {
            if (targetTimer > enemy.AttackSpeed && !enemy.isAttaking)
            {
                enemy.isAttaking = true;
                if (enemy.PlayerTarget != null)
                    enemy.CalculateDirection(enemy.PlayerTarget);
                else
                    return;
                enemy.SetAttackAnimation();
                enemy.ResetAnimation();
            }
            if (enemy.isAttaking)
                enemy.SetPauseAnimation = false;

            if (enemy.isAttaking && enemy.GetCurrentFrame == enemy.GetAttackFrame)
            {
                enemy.CreateProjectilesTowardsTarget(world, enemy.ProjectType);
                enemy.isAttaking = false;
                enemy.SetPauseAnimation = true;
                hasTarget = false;
                enemy.PlayerTarget = null;
                targetTimer = 0;
            }
        }

        public void BulletHellAttack(float degree)
        {
            if (targetTimer > enemy.AttackSpeed && !enemy.isAttaking)
            {
                enemy.isAttaking = true;
                enemy.SetAttackAnimation();
                enemy.ResetAnimation();
            }
            if (enemy.isAttaking)
                enemy.SetPauseAnimation = false;

            if (enemy.isAttaking && enemy.GetCurrentFrame == enemy.GetAttackFrame)
            {              
                enemy.isAttaking = false;
                enemy.SetPauseAnimation = true;
                hasTarget = false;
                enemy.PlayerTarget = null;
                targetTimer = 0;
            }

            enemy.CreateProjectiles(world, enemy.ProjectType, degree);
        }

        public void MultiShoot(float degree)
        {
            if (targetTimer > enemy.AttackSpeed && !enemy.isAttaking)
            {
                enemy.isAttaking = true;
                if (enemy.PlayerTarget != null)
                    enemy.CalculateDirection(enemy.PlayerTarget);
                else
                    return;
                enemy.SetAttackAnimation();
                enemy.ResetAnimation();
            }
            if (enemy.isAttaking)
                enemy.SetPauseAnimation = false;

            if (enemy.isAttaking && enemy.GetCurrentFrame == enemy.GetAttackFrame)
            {
                enemy.isAttaking = false;
                enemy.SetPauseAnimation = true;
                hasTarget = false;
                enemy.PlayerTarget = null;
                targetTimer = 0;

                enemy.CreateProjectiles(world, enemy.ProjectType, degree + 10);
                enemy.CreateProjectiles(world, enemy.ProjectType, degree + 5);
                enemy.CreateProjectiles(world, enemy.ProjectType, degree);
                enemy.CreateProjectiles(world, enemy.ProjectType, degree - 5);
                enemy.CreateProjectiles(world, enemy.ProjectType, degree - 10);
            }

           

        }

        public void TeleportAttack()
        {
            if (targetTimer > enemy.AttackSpeed && !enemy.isAttaking)
            {
                enemy.isAttaking = true;
                if (enemy.PlayerTarget != null)
                    enemy.CalculateDirection(enemy.PlayerTarget);
                else
                    return;
                enemy.SetAttackAnimation();
                enemy.ResetAnimation();
            }
            if (enemy.isAttaking)
                enemy.SetPauseAnimation = false;

            if (enemy.isAttaking && enemy.GetCurrentFrame == enemy.GetAttackFrame)
            {
                enemy.isAttaking = false;
                enemy.SetPauseAnimation = true;
                hasTarget = false;
                enemy.PlayerTarget = null;
                targetTimer = 0;
            }

            for (int i = 0; i < 360; i += 20)
            {
                enemy.CreateProjectiles(world, enemy.ProjectType, i);
            }

        }

        public void BigFireBallAttack(float degree)
        {
            if (targetTimer > enemy.AttackSpeed && !enemy.isAttaking)
            {
                enemy.isAttaking = true;
                if (enemy.PlayerTarget != null)
                    enemy.CalculateDirection(enemy.PlayerTarget);
                else
                    return;
                enemy.SetAttackAnimation();
                enemy.ResetAnimation();
            }

            if (enemy.isAttaking)
                enemy.SetPauseAnimation = false;

            if (enemy.isAttaking && enemy.GetCurrentFrame == enemy.GetAttackFrame)
            {
                enemy.isAttaking = false;
                enemy.SetPauseAnimation = true;
                hasTarget = false;
                enemy.PlayerTarget = null;
                targetTimer = 0;

            }
                enemy.CreateProjectiles(world, FGameObject.ProjectileType.Big_Fire_Bal, degree);
        }

        public void LavaAttack(Vector2 pos)
        {
            if (targetTimer > enemy.AttackSpeed && !enemy.isAttaking)
            {
                enemy.isAttaking = true;
                enemy.SetAttackAnimation();
                enemy.ResetAnimation();
            }
            if (enemy.isAttaking)
                enemy.SetPauseAnimation = false;

            if (enemy.isAttaking && enemy.GetCurrentFrame == enemy.GetAttackFrame)
            {
                enemy.isAttaking = false;
                enemy.SetPauseAnimation = true;
                hasTarget = false;
                enemy.PlayerTarget = null;
                targetTimer = 0;
            }

            enemy.CreateLavaFloor(world, pos);
        }

        public void WallAttack() { }

        //----- Setters -----//
        public bool SetTarget
        {
            set { hasTarget = value; }
        }

    }
}
