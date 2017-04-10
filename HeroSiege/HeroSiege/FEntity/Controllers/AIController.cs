using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.GameWorld;
using HeroSiege.FEntity.Enemies;
using Microsoft.Xna.Framework;

namespace HeroSiege.FEntity.Controllers
{
    class AIController : Control
    {

        const float UPDATE_PATH_TIMER = 0.5f;
        const float UPDATE_Target_TIMER = 0.5f;
        float pathTimer, targetTimer;

        public Entity Target { get; private set; }
        public float NearestDist { get; private set; }
        public bool hasTarget {get; private set; }


        public AIController(World world, Enemy entity)
            : base(world, entity)
        {
            Target = null;
            SetDestinationToCastle();
            //Init FuSM here
            switch (entity.AttackType)
            {
                case AttackType.Range:
                    break;
                case AttackType.Melee:
                    break;
                case AttackType.NONE:
                    break;
                default:
                    break;
            }


        }

        public override void Update(float delta)
        {
            base.Update(delta);

            pathTimer += delta;
            if (pathTimer > UPDATE_PATH_TIMER && hasTarget == false)
            {
                //isPlayerInRange(players);
                pathTimer = 0;
            }    

            if (Target != null)
                UpdatePathToTarget(delta);
            else
                SetDestinationToCastle();
        }

        private void UpdatePathToTarget(float delta)
        {
            pathTimer += delta;
            if(pathTimer > UPDATE_PATH_TIMER)
            {
                ((Enemy)entity).Astar(world.Map, new Point((int)Target.Position.X, (int)Target.Position.Y));
                pathTimer = 0;
            }
        }
        private void SetDestinationToCastle()
        {
            if(((Enemy)entity).havePath == false)
            {
                ((Enemy)entity).Astar(world.Map, new Point((int)world.Map.HeroCastle.X,
                                                           (int)world.Map.HeroCastle.Y));
            }
        }

        private void isPlayerInRange(List<Entity> players)
        {
            Target = null;
            NearestDist = 100000;
            for (int i = 0; i < players.Count; i++)
            {
                if(entity.Stats.Radius > Vector2.Distance(players[i].Position, entity.Position))
                {
                    float lenght = Vector2.Distance(players[i].Position, entity.Position);
                    if (lenght < NearestDist)
                    {
                        NearestDist = lenght;
                        Target = players[i];
                        hasTarget = true;
                    }
                }
                
            }
        }
    }
}
