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
using HeroSiege.Tools;
using HeroSiege.FEntity.Buildings;
using HeroSiege.FEntity.Buildings.HeroBuildings;
using HeroSiege.FEntity.Buildings.EnemyBuildings;

namespace HeroSiege.GameWorld
{
    class World
    {
        GameSettings gameSettings;

        public TileMap Map { get; private set; }

        //----- Entities -----//
        public List<Entity> Enimies { get; private set; }
        private List<Entity> deadEnimies;

        public Entity PlayerOne { get; private set; }
        public Entity PlayerTwo { get; private set; }

        //----- Buildings -----//
        public List<Building> HeroBuildings { get; private set; }
        public List<Building> EnemyBuildings { get; private set; }

        //----- Game objects -----//
        public List<GameObject> GameObjects { get; private set; }
        public List<GameObject> EnemyObjects { get; private set; }
        public List<GameObject> DeadObjects { get; private set; }

        public List<Rectangle> Hitboxes { get; private set; }


        //----- Constructor -----//
        public World(GameSettings gameSettings) 
        {
            this.gameSettings = gameSettings;

            Initmap(gameSettings.MapName);
            InitBuildings();

            InitEntitys();
        }

        //----- Initiators -----//
        public void Initmap(string mapName)
        {
            this.Map = new TileMap(mapName);
            Hitboxes = Map.Hitboxes;
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
                //InitPlayerTwo();
            }
        }

        public void InitPlayerOne()
        {
            switch (gameSettings.playerOne)
            {
                case CharacterType.ElvenArcher:
                    break;
                case CharacterType.Mage:
                    break;
                case CharacterType.Gryphon_Rider:
                    break;
                case CharacterType.FootMan:
                    break;
                case CharacterType.Dwarven:
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    break;
                case CharacterType.Knight:
                    break;
                case CharacterType.None:
                    break;
                default:
                    break;
            }
            PlayerOne = new TestPlayer(32 * 40, 32 * 90, 64, 64);
            PlayerOne.SetControl(new HumanControler(PlayerIndex.One, PlayerOne, this));
            
        }
        public void InitPlayerTwo()
        {
            switch (gameSettings.playerTwo)
            {
                case CharacterType.ElvenArcher:
                    break;
                case CharacterType.Mage:
                    break;
                case CharacterType.Gryphon_Rider:
                    break;
                case CharacterType.FootMan:
                    break;
                case CharacterType.Dwarven:
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    break;
                case CharacterType.Knight:
                    break;
                case CharacterType.None:
                    break;
                default:
                    break;
            }

            PlayerTwo = new TestPlayer(32 * 40, 32 * 105, 64, 64);
            PlayerTwo.SetControl(new HumanControler(PlayerIndex.Two, PlayerTwo, this));
        }

        public void InitBuildings()
        {
            HeroBuildings = new List<Building>();
            EnemyBuildings = new List<Building>();

            if (Map != null)
            {
                InitHeroBuildings();
                InitEnemyBuildings();
            }

        }

        private void InitHeroBuildings()
        {
            for (int i = 0; i < Map.HeroTowerPos.Count; i++)
                HeroBuildings.Add(new HeroTower(Map.HeroTowerPos[i].X, Map.HeroTowerPos[i].Y));

            HeroBuildings.Add(new HeroCastle(Map.HeroCastle.X, Map.HeroCastle.Y));

            foreach (Building b in HeroBuildings)
            {
                Hitboxes.Add(b.GetHitbox());
            }
        }
        private void InitEnemyBuildings()
        {
            for (int i = 0; i < Map.EnemieTowerPos.Count; i++)
                EnemyBuildings.Add(new EnemyTower(Map.EnemieTowerPos[i].X, Map.EnemieTowerPos[i].Y));

            for (int i = 0; i < Map.EnemieSpawnerPos.Count; i++)
                EnemyBuildings.Add(new EnemySpawnerTower(Map.EnemieSpawnerPos[i].X, Map.EnemieSpawnerPos[i].Y));

            foreach (Building b in EnemyBuildings)
            {
                Hitboxes.Add(b.GetHitbox());
            }
        }

        //----- Updates-----//
        public void Update(float delta)
        {
            UpdatePlayers(delta);


            //Enemies
            UpdateEnemies(delta);
        }

        private void UpdatePlayers(float delta)
        {

            //Player One
            if (PlayerOne != null)
            {
                PlayerOne.Update(delta);
                PlayerOne.UpdatePlayerMovement(delta, Hitboxes);
            }
            //Player Two
            if (PlayerTwo != null)
            {
                PlayerTwo.Update(delta);
                PlayerTwo.UpdatePlayerMovement(delta, Map.Hitboxes);
            }
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
