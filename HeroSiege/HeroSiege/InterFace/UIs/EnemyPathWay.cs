using HeroSiege.AISystems.PathFinding;
using HeroSiege.FTexture2D;
using HeroSiege.GameWorld.map;
using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.InterFace.UIs
{
    class EnemyPathWay
    {
        //----- Feilds -----//
        private int tileSize;

        public Vector2 SpawnPos { get { return spawnPos; } }
        private Vector2 spawnPos;

        public Vector2 CastlePos { get { return castlePos; } }
        private Vector2 castlePos;

        private List<Dot> lineList;

        protected Queue<Vector2> waypoints;
        protected Pathfinder pathFinder;

        //----- Constructor -----//
        public EnemyPathWay() { }

        public void Init(TileMap map)
        {
            pathFinder = new Pathfinder(map, map.Width, map.Height);
            tileSize = map.TileSize;
            lineList = new List<Dot>();
            waypoints = new Queue<Vector2>();
            castlePos = map.HeroCastle;
        }

        //----- Updates -----//
        public void Update(float delta)
        {
            foreach (Dot d in lineList)
            {
                d.Update(delta);

                if (!d.HavePath)
                {
                    d.SetStartPos(waypoints.Peek());
                    d.SetWaypoints(waypoints);
                }
            }
        }


        //----- Draw -----//
        public void Draw(SpriteBatch SB)
        {
            foreach (Dot d in lineList)
                d.Draw(SB);
        }

        
        //----- A* -----//
        public void CalcNewWayPoint()
        {
            waypoints = pathFinder.FindPath(new Point((int)SpawnPos.X / tileSize, (int)SpawnPos.Y / tileSize),
                                               new Point((int)CastlePos.X / tileSize, (int)CastlePos.Y / tileSize));
        }

        //----- Other -----//
        public void ClearList()
        {
            lineList.Clear();
        }
        public void PopulateList()
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                Dot d = new Dot();
                d.SetStartPos(waypoints.ToList()[i]);
                d.SetWaypoints(new Queue<Vector2>(waypoints.ToList().GetRange(i, waypoints.ToArray().Length - i)));
                lineList.Add(d);
            }
        }

        
        //----- Setters -----//
        public void SetSpawnPos(Vector2 spawnPos)
        {
            this.spawnPos = spawnPos;
        }
    }

    class Dot : Sprite
    {
        const float SPEED = 20;
        Vector2 velocity;
        public bool HavePath { get; private set; }

        private Queue<Vector2> path;

        public Dot()
            : base(null, 0, 0, 0, 0)
        {
            this.SetRegion(ResourceManager.GetTexture("WhitePixel"));
            this.SetSize(4, 4);
            HavePath = false;
            this.Color = Color.Red * 0.4f;
            path = new Queue<Vector2>();
        }

        public override void Update(float delta)
        {
            if (HavePath)
                movment(delta);
        }

        protected void movment(float delta)
        {
            if (path.Count > 0)
            {
                if (DistanceToDestination < 1f)
                {
                    position = path.Dequeue();

                }
                else
                {
                    Vector2 direction = path.Peek() - position;
                    direction.Normalize();

                    velocity = Vector2.Multiply(direction, SPEED);

                    position += velocity * delta;               
                }
            }

            if (path.Count <= 0)
                HavePath = false;
        }
        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, path.Peek()); }
        }

        public void SetWaypoints(Queue<Vector2> path)
        {
            if (this.path.Count != 0)
            {
                var temppoint = this.path.Peek();
                this.path.Clear();
                this.path.Enqueue(temppoint);
                var tempPoints = path.ToArray();

                foreach (var p in tempPoints)
                    this.path.Enqueue(p);
            }
            else
                this.path = new Queue<Vector2>(path);

            HavePath = true;
        }
        public void SetStartPos(Vector2 pos)
        {
            position = pos;
        }
    }
}
