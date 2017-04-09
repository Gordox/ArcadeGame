using HeroSiege.FEntity.Enemies;
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
        Random rnd;
        World gameWorld;
        GameSettings settings;
        List<Vector2> spawns;
        FrameAnimation nextwaveani;

        private const float START_SPAWN_DELAY = 1.5f;
        private const float START_WAVE_DELAY = 7.0f;
        private const float END_SPAWN_DELAY = 0.25f;
        private const float END_WAVE_DELAY = 4.0f;

        private int[] Wave = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90 };
        private float[] SpawnDelay;
        private float[] WaveDelay;
        private float Timer;
        private int WaveCount;
        private int CurrentWave;
        private int TotalSpawned;
        private bool NextWave;

        public SpawnController(World world, GameSettings gameSettings)
        {
            this.gameWorld = world;
            this.settings = gameSettings;
            spawns = new List<Vector2>();
            rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            foreach (Vector2 pos in world.Map.EnemieSpawnerPos)
            {
                spawns.Add(pos);
            }

            InitWaveSystem();

            //nextwaveani = new FrameAnimation(TextureManager.NextWave, 0, 0, 910, 512, 112, 1 / 60.0f, new Point(14, 8), false);
        }

        private void InitWaveSystem()
        {
            WaveCount = Wave.Length;
            SpawnDelay = new float[WaveCount];
            WaveDelay = new float[WaveCount];
            CurrentWave = 0;
            TotalSpawned = 0;
            Timer = 0;
            NextWave = false;

            // Räkna ut mellanskillnaden för varje wave
            float spawn_diff = START_SPAWN_DELAY - END_SPAWN_DELAY;
            float wave_diff = START_WAVE_DELAY - END_WAVE_DELAY;
            for (int i = 0; i < WaveCount; i++)
            {
                float percent = (float)i / (float)(WaveCount - 1.0f);
                SpawnDelay[i] = START_SPAWN_DELAY - spawn_diff * percent;
                WaveDelay[i] = START_WAVE_DELAY - wave_diff * percent;
            }
        }

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
        }

        private void WaveActive(float delta)
        {
            Timer += delta;
            if (Timer >= SpawnDelay[CurrentWave])
            {
                if (TotalSpawned < Wave[CurrentWave])
                {
                    SpawnEnemy();
                    Timer = 0;
                    TotalSpawned++;
                }
                if (TotalSpawned >= Wave[CurrentWave] && gameWorld.Enimies.Count == 0)
                {
                    NextWave = true;
                    Timer = 0;
                    Console.WriteLine("Next wave");
                }
            }
        }

        private void WaveInactive(float delta)
        {
            if (Timer >= 1.0f)
            {
                nextwaveani.Update(delta);
                ResetPlayer();
            }

            Timer += delta;
            if (Timer >= WaveDelay[CurrentWave])
            {
                Timer = 0;
                TotalSpawned = 0;
                NextWave = false;
                //nextwaveani = new FrameAnimation(TextureManager.NextWave, 0, 0, 910, 512, 112, 1 / 60.0f, new Point(14, 8), false);
                CurrentWave++;
                if (CurrentWave >= WaveCount)
                {
                    // Win
                    Console.WriteLine("Won first stage");
                }
            }
        }

        private void ResetPlayer()
        {
            // Return if 1 player mode
            //if (gameWorld.settings.playerTwo == CharacterType.None || gameWorld.settings.playerOne == CharacterType.None)
            //    return;

            //if (gameWorld.PlayerOne == null)
            //    gameWorld.InitPlayerOne(0.5f);
            //else if (gameWorld.PlayerTwo == null)
            //    gameWorld.InitPlayerTwo(0.5f);
        }

        public void Draw(SpriteBatch SB)
        {
            if (NextWave)
            {
                if (Timer >= 1.0f && nextwaveani.HasNext())
                    SB.Draw(ResourceManager.GetTexture(""), new Vector2(0, 0), Color.White);
            }
        }

        private void SpawnEnemy()
        {
            // Get spawn point
            List<Vector2> avaiable = new List<Vector2>();
            for (int i = 0; i < spawns.Count; i++)
            {
                //if (!spawn[i].Intersects(gameWorld.camera.ViewRect))
                //    avaiable.Add(spawn[i]);
            }

            int selected = rnd.Next(avaiable.Count);
            Vector2 spawnPos = new Vector2(avaiable[selected].X, avaiable[selected].Y);

            // hp multiplier for 2 player mode
            float hp_multiplier = 1.0f;
            if (settings.playerOne != CharacterType.None && settings.playerTwo != CharacterType.None)
                hp_multiplier = 2.5f;

            // Randomize enemy
            Enemy enemy = null;
            int type = rnd.Next(2);
            switch (type)
            {
                case 0:
                    //enemy = new RockGolom(spawnPos, hp_multiplier);
                    //enemy.SetControl(new AIControlMelee(enemy, gameWorld));
                    break;
                case 1:
                    //enemy = new WaterGolom(spawnPos, hp_multiplier);
                    //enemy.SetControl(new AIControlRange(enemy, gameWorld));
                    break;
            }
            //gameWorld.AddEnemy(enemy);
        }
    }
}
