using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.GameWorld.map;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FEntity;
using HeroSiege.FGameObject;
using Microsoft.Xna.Framework;

namespace HeroSiege.GameWorld
{
    class World
    {
        public TileMap Map { get; private set; }

        //----- Entities -----//
        public List<Entity> Enimies { get; private set; }
        private List<Entity> deadEnimies;
        public Entity PlayerOne { get; private set; }
        public Entity PlayerTwo { get; private set; }

        //----- Game objects -----//
        public List<GameObject> GameObjects { get; private set; }
        public List<GameObject> EnemyObjects { get; private set; }
        public List<GameObject> DeadObjects { get; private set; }


        public List<Rectangle> Hitboxes { get; private set; }


        public World(string mapName) 
        {
            Initmap(mapName);
            InitEntitys();
        }

        //----- Initiators -----//
        public void Initmap(string mapName)
        {
            this.Map = new TileMap(mapName);
        }
        public void InitEntitys()
        {
            Enimies = new List<Entity>();
            deadEnimies = new List<Entity>();
            GameObjects = new List<GameObject>();
            EnemyObjects = new List<GameObject>();
            DeadObjects = new List<GameObject>();
            Hitboxes = new List<Rectangle>();

            if (Map != null)
            {
                
            }
        }

        public void InitPlayerOne() { }
        public void InitPlayerTwo() { }


        //----- Updates-----//
        public void Update(float delta)
        {

            UpdateEnemies(delta);
        }
        private void UpdateEnemies(float delta)
        {
            foreach (var enemy in Enimies)
            {
                enemy.Update(delta);

                if(!enemy.IsAlive)
                    deadEnimies.Add(enemy);
            }

            foreach (var deadEntity in deadEnimies)
            {
                Enimies.Remove(deadEntity);
            }
            deadEnimies.Clear();
        }

        //----- Functions-----//


    }
}
