using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework;
using HeroSiege.AISystems.PathFinding;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.Manager;
using HeroSiege.GameWorld.map;
using HeroSiege.Tools;
using HeroSiege.FGameObject;
using HeroSiege.GameWorld;
using HeroSiege.FGameObject.Projectiles;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.FEntity.Players;
using HeroSiege.FEntity.Buildings;

namespace HeroSiege.FEntity.Enemies
{

    public enum AttackType
    {
        Range,
        Melee,
        NONE
    }

    class Enemy : Entity
    {
        public Hero PlayerTarget { get; set; }
        public Building BuildingTarget { get; set; }

        public AttackType AttackType { get; protected set; }
        public ProjectileType ProjectType { get; protected set; }
        public float AttackSpeed { get; protected set; }

        public Vector2 HeroCastle { get; set; }
        protected Vector2 oldPos;

        //--- A* ---//
        protected Queue<Vector2> waypoints;
        public bool havePath;
        protected Pathfinder pathFinder;

        //----- Constructors and Initiators -----//
        public Enemy(TextureRegion region, float x, float y, float width, float height, AttackType attackType)
            : base(region, x, y, width, height)
        {
            this.PlayerTarget = null;
            this.BuildingTarget = null;
            InitAStar();
            this.AttackType = attackType;
            oldPos = position;
        }

        protected void InitAStar()
        {
            waypoints = new Queue<Vector2>();
            havePath = false;
        }

        //----- Updates -----//
        public override void Update(float delta)
        {
            base.Update(delta);     
                 
            movment(delta);
            UpdateMovmentDirection();
            UpdateAnimation();

            oldPos = position;
        }
        protected override void UpdateMovmentDirection()
        {
            base.UpdateMovmentDirection();

            if (position.X > oldPos.X && position.Y < oldPos.Y)
            {
                MovingDirection = Direction.North_East;
            }
            else if (position.X < oldPos.X && position.Y < oldPos.Y)
            {
                MovingDirection = Direction.North_West;
            }
            else if (position.X < oldPos.X && position.Y > oldPos.Y)
            {
                MovingDirection = Direction.South_West;
            }
            else if (position.X > oldPos.X && position.Y > oldPos.Y)
            {
                MovingDirection = Direction.South_East;
            }

            else if (position.Y < oldPos.Y)
            {
                MovingDirection = Direction.North;
            }
            else if (position.X > oldPos.X)
            {
                MovingDirection = Direction.East;
            }
            else if (position.Y > oldPos.Y)
            {
                MovingDirection = Direction.South;
            }
            else if (position.X < oldPos.X)
            {
                MovingDirection = Direction.West;
            }


        }

        //----- Draws -----//
        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            if (DevTools.DevDebugMode || DevTools.DevDrawAIPath)
                DrawPath(SB, Position, waypoints.ToList(), Color.Red, 5);
            if(Stats.Health < Stats.MaxHealth)
                DrawHealtBar(SB);
        }

        //Health bar
        protected void DrawHealtBar(SpriteBatch SB)
        {
            //Background
            SB.Draw(ResourceManager.GetTexture("WhitePixel"), new Vector2(Position.X - 25, Position.Y - 30), new Rectangle(0, 0, 50, 8), Color.Black);
            SB.Draw(ResourceManager.GetTexture("WhitePixel"), new Vector2(Position.X - 24, Position.Y - 29),
                                                              GenerateBar(Stats.Health, Stats.MaxHealth, 48, 6),
                                                              LerpHealthColor(Stats.Health, Stats.MaxHealth));
        }

        //Draw Line
        public void DrawPath(SpriteBatch SB, Vector2 position, List<Vector2> points, Color color, int thickness)
        {
            if (points.Count < 2)
                return;

            DrawLine(SB, position, points[0], color, thickness);

            for (int i = 1; i < points.Count; i++)
            {
                DrawLine(SB, points[i - 1], points[i], color, thickness);
            }
        }
        public void DrawLine(SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 5)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y + 16, (int)(end - begin).Length(), width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.Draw(ResourceManager.GetTexture("WhitePixel"), r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        //----- Methos and functions ----//
        public void SetAttackAnimation() { SetAttckAnimations(); }
        public void SetMovmentAnimation() { SetMovmentAnimations(); }
        public int GetCurrentFrame
        {
            get { return sprite.Animations.CurrentAnimation.currentFrame; }
        }
        public int GetAttackFrame
        {
            get { return AttackFrame; }
        }

        //----- A Star -----//
        public void Astar(TileMap map, Point destination)
        {
            pathFinder = new Pathfinder(map, map.Width, map.Height);

            if (pathFinder != null)
            {
                SetWaypoints(pathFinder.FindPath(new Point((int)Position.X / map.TileSize, (int)Position.Y / map.TileSize),
                                               new Point((int)destination.X / map.TileSize, (int)destination.Y / map.TileSize)));
            }
        }

        public Queue<Vector2> GetAstarPath(TileMap map, Point destination)
        {
            pathFinder = new Pathfinder(map, map.Width, map.Height);

            if (pathFinder != null)
            {
                return pathFinder.FindPath(new Point((int)Position.X / map.TileSize, (int)Position.Y / map.TileSize),
                                               new Point((int)destination.X / map.TileSize, (int)destination.Y / map.TileSize));
            }

            return null;
        }
        public Queue<Vector2> GetAstarPath(TileMap map, Point start, Point destination)
        {
            pathFinder = new Pathfinder(map, map.Width, map.Height);

            if (pathFinder != null)
            {
                return pathFinder.FindPath(new Point((int)start.X / map.TileSize, (int)start.Y / map.TileSize),
                                               new Point((int)destination.X / map.TileSize, (int)destination.Y / map.TileSize));
            }

            return null;
        }

        protected void movment(float delta)
        {
            if (waypoints.Count > 0)
            {
                if (DistanceToDestination < 1f)
                {
                    position = waypoints.Dequeue();

                }
                else
                {
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();

                    velocity = Vector2.Multiply(direction, Stats.Speed);

                    position += velocity * delta;
                }
            }

            if (waypoints.Count <= 0)
                havePath = false;
        }
        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }
        public void SetWaypoints(Queue<Vector2> points)
        {
            if (this.waypoints.Count != 0)
            {
                var temppoint = this.waypoints.Peek();
                this.waypoints.Clear();
                this.waypoints.Enqueue(temppoint);
                var tempPoints = points.ToArray();

                foreach (var point in tempPoints)
                    this.waypoints.Enqueue(point);
            }
            else
                this.waypoints = points;

            havePath = true;
        }
        public void SetWaypoints(List<Vector2> points)
        {
            if (this.waypoints.Count != 0)
            {
                var temppoint = this.waypoints.Peek();
                this.waypoints.Clear();
                this.waypoints.Enqueue(temppoint);
                var tempPoints = points.ToArray();

                foreach (var point in tempPoints)
                    this.waypoints.Enqueue(point);
            }
            else
                this.waypoints = new Queue<Vector2>(points);

            havePath = true;
        }
        public int WaypointCount()
        {
            return waypoints.Count;
        }
        public void ClearWaypoints()
        {
            waypoints.Clear();
        }

        //----- Attacking method and functions -----//
        public void GetTargets(List<Entity> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector2.Distance(Position, enemies[i].Position) <= Stats.Radius)
                {
                    //if (Targets.Count < totalTargets)
                    //    Targets.Add(enemies[i]);
                }
            }
        }

        public void CreateProjectilesTowardsTarget(World parent, ProjectileType type)
        {
            Projectile temp = null;
            switch (type)
            {
                case ProjectileType.Fire_Bal:
                    temp = new FireBal(ResourceManager.GetTexture("Fire_Bal"), Position.X, Position.Y, 32, 32, MovingDirection);
                    break;
                case ProjectileType.Lighing_bal:
                    break;
                case ProjectileType.Evil_Hand:
                    break;
                case ProjectileType.Dark_Eye:
                    break;
                case ProjectileType.Lightning_Axe:
                    break;
                case ProjectileType.Normal_Axe:
                    int x = ResourceManager.GetTexture("Normal_Axe").region.X;
                    int y = ResourceManager.GetTexture("Normal_Axe").region.Y;
                    if(PlayerTarget != null)
                        temp = new Axe("AxeAnimation", new FrameAnimation(ResourceManager.GetTexture("Normal_Axe"), x, y, 32, 32, 3, 0.08f, new Point(3, 1)), Position.X, Position.Y, 32, 32, PlayerTarget);
                    else
                        temp = new Axe("AxeAnimation", new FrameAnimation(ResourceManager.GetTexture("Normal_Axe"), x, y, 32, 32, 3, 0.08f, new Point(3, 1)), Position.X, Position.Y, 32, 32, BuildingTarget);
                    break;
                default:
                    break;      
            }
            parent.EnemyObjects.Add(temp);
        }

    }
}
