using HeroSiege.FEntity;
using HeroSiege.FEntity.Buildings.EnemyBuildings;
using HeroSiege.FEntity.Controllers;
using HeroSiege.FEntity.Enemies;
using HeroSiege.FEntity.Enemies.Bosses;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.GameWorld;
using HeroSiege.Manager;
using HeroSiege.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.AISystems
{
    class SpawnController
    {
        //----- Feilds -----//
        Random rnd;
        World gameWorld;
        GameSettings settings;

        List<EnemySpawner> spawners;
        EnemySpawner currentSpawner;

        private const float START_WAVE_DELAY  = 7.0f;
        private const float SPAWN_DELAY       = 0.8f;

        private float timer;
        private int WaveCount;
        private int CurrentWave;
        private int TotalSpawned;
        private bool NextWave;
        int type = 0;

        bool allSpawnersDead;

        int deathKnightLevel = 1;
        int enemiesRemainingToSpawn;
        int enemiesRemainingAlive;

        //-----  -----//
        public SpawnController(World world, GameSettings gameSettings)
        {
            this.gameWorld = world;
            this.settings = gameSettings;
            this.rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            this.spawners = new List<EnemySpawner>();
            this.currentSpawner = null;
            InitWaveSystem();
        }

        private void InitWaveSystem()
        {
            CurrentWave = 0;
            TotalSpawned = 0;
            timer = 0;
            NextWave = true;
            allSpawnersDead = false;
            EnemyType();
        }

        //----- Updates -----//
        public void Update(float delta)
        {
            if (!NextWave)
            {
                WaveActive(delta);
            }
            else
            {
                WaveInactive(delta);
            }
            if (spawners.Count == 0)
                allSpawnersDead = true;
        }

        private void WaveActive(float delta)
        {
            timer += delta;
            if (timer >= SPAWN_DELAY && !allSpawnersDead)
            {
                if (TotalSpawned < enemiesRemainingToSpawn)
                {
                    SpawnEnemy();
                    timer = 0;
                    TotalSpawned++;
                }
                if (TotalSpawned >= WaveCount && gameWorld.Enemies.Count == 0)
                {
                    NextWave = true;
                    timer = 0;
                    Console.WriteLine("Next wave");
                }
            }
        }

        private void WaveInactive(float delta)
        {
            timer += delta;
            if (timer >= START_WAVE_DELAY && !allSpawnersDead)
            {
                timer = 0;
                TotalSpawned = 0;
                NextWave = false;
                CurrentWave++;
                WaveCount = enemiesRemainingToSpawn = EnemysToSpawn();
                if(spawners.Count > 0)
                    currentSpawner = spawners[rnd.Next(spawners.Count - 1)];
                 EnemyType();
            }
        }
        //-----  -----//
        public void Draw(SpriteBatch SB)
        {

        }

        //----- Function / Methods -----//
        private void SpawnEnemy()
        {
            if (currentSpawner == null)
                return;
            int lvl = 1 + CurrentWave / 4;
            // Randomize enemy
            Enemy enemy = null;
           
            switch (type)
            {
                case 0:
                    enemy = new Troll_Axe_Thrower(currentSpawner.Position.X, currentSpawner.Position.Y, 64, 64, AttackType.Range, lvl);
                    break;
                case 1:
                    enemy = new Orge(currentSpawner.Position.X, currentSpawner.Position.Y, 64, 64, AttackType.Range, lvl);
                    break;
                case 2:
                    enemy = new Zeppelin(currentSpawner.Position.X, currentSpawner.Position.Y, 64, 64, AttackType.Range, lvl);
                    break;
                case 3:
                    //enemy = new WaterGolom(spawnPos, hp_multiplier);
                    break;
            }
            enemy.SetControl(new AIController(gameWorld, enemy));
            gameWorld.Enemies.Add(enemy);
        }

        private void EnemyType()
        {
            type = rnd.Next(0,3); //Change later to 4
        }

        private int EnemysToSpawn()
        {
            return (int)(CurrentWave * 2 + ((CurrentWave * CurrentWave) / (2 * CurrentWave))); ;
        }

        public void AddSpawner(EnemySpawner spawner)
        {
            spawners.Add(spawner);
        }
        public void RemoveSpawner(EnemySpawner spawner)
        {
            spawners.Remove(spawner);
            if(currentSpawner == spawner)
            {
                currentSpawner = null;
                NextWave = true;
                timer = 0;
                Console.WriteLine("Next wave");
            }

            DeathKnight deathKnight = new DeathKnight(spawner.Position.X, spawner.Position.Y, 64, 64, AttackType.Range, deathKnightLevel);
            deathKnight.SetControl(new AIController(gameWorld, deathKnight));
            gameWorld.Enemies.Add(deathKnight);

            deathKnightLevel++;
        }
    }
}
