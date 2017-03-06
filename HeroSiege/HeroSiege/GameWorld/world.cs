using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.GameWorld.map;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FEntity;
using HeroSiege.FGameObject;
using Microsoft.Xna.Framework;
using HeroSiege.FEntity.Player;
using HeroSiege.Manager;
using HeroSiege.FEntity.Controllers;

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
  

        //----- Constructor -----//
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


            if (Map != null)
            {
                InitPlayerOne();
            }
        }

        public void InitPlayerOne()
        {
            PlayerOne = new TestPlayer(ResourceManager.GetTexture("BaseLayer", 320), 32*40,32*90, 32,32);
            PlayerOne.SetControl(new HumanControler(PlayerIndex.One, PlayerOne, this));
        }
        public void InitPlayerTwo() { }

        //----- Updates-----//
        public void Update(float delta)
        {
            //Player One
            PlayerOne.Update(delta);
            PlayerOne.UpdatePlayerMovement(delta, Map.Hitboxes);
            //Player Two

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
