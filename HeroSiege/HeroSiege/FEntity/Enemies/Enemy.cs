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
        public AttackType AttackType { get; protected set; }

        public Vector2 HeroCastle { get; set; }

        //--- A* ---//
        protected Queue<Vector2> waypoints;
        public bool havePath;
        protected Pathfinder pathFinder;

        //----- Constructors and Initiators -----//
        public Enemy(TextureRegion region, float x, float y, float width, float height, AttackType attackType)
            : base(region, x, y, width, height)
        {
            InitAStar();
            this.AttackType = attackType;
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

            UpdateAnimation();
            movment(delta);
        }

        //----- Draws -----//
        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            if (DevTools.DevDebugMode || DevTools.DevDrawAIPath)
                DrawPath(SB, Position, waypoints.ToList(), Color.Red, 5);
        }

        //----- Draw Line -----//
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
    }
}
