﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.GameWorld.map;
using HeroSiege.FEntity;
using HeroSiege.FGameObject;
using Microsoft.Xna.Framework;
using HeroSiege.FEntity.Players;
using HeroSiege.Manager;
using HeroSiege.FEntity.Controllers;
using HeroSiege.Tools;
using HeroSiege.FEntity.Buildings;
using HeroSiege.FEntity.Buildings.HeroBuildings;
using HeroSiege.FEntity.Buildings.EnemyBuildings;
using HeroSiege.AISystems;
using HeroSiege.FEntity.Enemies;
using HeroSiege.FGameObject.Projectiles;
using HeroSiege.FTexture2D.FAnimation;
using HeroSiege.FTexture2D.SpriteEffect;
using HeroSiege.Systems;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FEntity.Enemies.Bosses;
using HeroSiege.FGameObject.Attacks;

namespace HeroSiege.GameWorld
{
    class World
    {
        public bool HeroCastleDestroyed { get; private set; }
        public bool DragonDefeated { get; private set; }

        public GameSettings gameSettings { get; private set; }

        public SpawnController spawnController { get; private set; }

        public SpatialHashGrid HashGrid { get; private set; }

        public TileMap Map { get; private set; }

        //----- Entities -----//
        public List<Entity> Enemies { get; private set; }
        public List<Entity> EnemieBosses { get; private set; }
        private List<Entity> deadEnimies;

        public Hero PlayerOne { get; private set; }
        public Hero PlayerTwo { get; private set; }

        //----- Buildings -----//
        public List<Building> HeroBuildings { get; private set; }
        public List<Building> EnemyBuildings { get; private set; }
        public List<Building> DeadBuildings { get; private set; }
        public List<Building> GeneralBuildings { get; private set; }


        //----- Game objects -----//
        public List<GameObject> GameObjects { get; private set; }
        public List<GameObject> EnemyObjects { get; private set; }
        public List<GameObject> DeadObjects { get; private set; }
        public FireWall FireWall { get; private set; }
        public List<Rectangle> Hitboxes { get; private set; }

        //----- Effects -----//
        public List<SpriteFX> Effects { get; private set; }
        public SpriteFXPool FXPool { get; private set; }

        //----- Constructor -----//
        public World(GameSettings gameSettings) 
        {
            this.gameSettings = gameSettings;
            this.Effects = new List<SpriteFX>();
            this.FXPool = new SpriteFXPool();

            Initmap(gameSettings.MapName);
            this.spawnController = new SpawnController(this, gameSettings);
            InitBuildings();
            InitEntitys();
            InitGameObjects();
        }

        //----- Initiators -----//
        public void Initmap(string mapName)
        {
            this.Map = new TileMap(mapName);
            Hitboxes = Map.Hitboxes;
        }

        public void InitEntitys()
        {
            DragonDefeated = false;
            Enemies = new List<Entity>();
            EnemieBosses = new List<Entity>();
            deadEnimies = new List<Entity>();
            GameObjects = new List<GameObject>();
            EnemyObjects = new List<GameObject>();
            DeadObjects = new List<GameObject>();


            if (Map != null)
            {
                InitPlayerOne();
                InitPlayerTwo();
                InitBosses();
            }
        }

        public void InitPlayerOne()
        {
            switch (gameSettings.playerOne)
            {
                case CharacterType.ElvenArcher:
                    PlayerOne = new ElvenArcher(32 * 38, 32 * 102, 64, 64);
                    break;
                case CharacterType.Mage:
                    PlayerOne = new Mage(32 * 38, 32 * 102, 64, 64);
                    break;
                case CharacterType.Gryphon_Rider:
                    PlayerOne = new GryphonRider(32 * 38, 32 * 102, 96, 96);
                    break;
                case CharacterType.FootMan:
                    PlayerOne = new FootMan(32 * 38, 32 * 102, 64, 64);
                    break;
                case CharacterType.Dwarven:
                    PlayerOne = new Dwarven(32 * 38, 32 * 102, 56, 56);
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    PlayerOne = new GnomishFlyingMachine(32 * 38, 32 * 102, 80, 80);
                    break;
                case CharacterType.Knight:
                    PlayerOne = new Knight(32 * 38, 32 * 102, 72, 72);
                    break;
                case CharacterType.None:
                    PlayerOne = null;
                    break;
                default:
                    break;
            }
            if(PlayerOne != null)
                PlayerOne.SetControl(new HumanControler(PlayerIndex.One, PlayerOne, this));
            
        }
        public void InitPlayerTwo()
        {
            switch (gameSettings.playerTwo)
            {
                case CharacterType.ElvenArcher:
                    PlayerTwo = new ElvenArcher(32 * 43, 32 * 102, 64, 64);
                    break;
                case CharacterType.Mage:
                    PlayerTwo = new Mage(32 * 43, 32 * 102, 64, 64);
                    break;
                case CharacterType.Gryphon_Rider:
                    PlayerTwo = new GryphonRider(32 * 43, 32 * 102, 96, 96);
                    break;
                case CharacterType.FootMan:
                    PlayerTwo = new FootMan(32 * 43, 32 * 102, 64, 64);
                    break;
                case CharacterType.Dwarven:
                    PlayerTwo = new Dwarven(32 * 43, 32 * 102, 56, 56);
                    break;
                case CharacterType.Gnomish_Flying_Machine:
                    PlayerTwo = new GnomishFlyingMachine(32 * 43, 32 * 102, 80, 80);
                    break;
                case CharacterType.Knight:
                    PlayerTwo = new Knight(32 * 43, 32 * 102, 72, 72);
                    break;
                case CharacterType.None:
                    PlayerTwo = null;
                    break;
                default:
                    break;
            }
            if (PlayerTwo != null)
                PlayerTwo.SetControl(new HumanControler(PlayerIndex.Two, PlayerTwo, this));
        }

        public void InitBosses()
        {
            float x = 0, y = 0;
            foreach (Rectangle area in Map.MiniBossArea)
            {
                x = area.X + area.Width / 2;
                y = area.Y + area.Height / 2;
                Demon de = new Demon(x, y, 64, 64);
                de.SetControl(new BossController(this, de, area));
                EnemieBosses.Add(de);
            }

            x = Map.FinalBossArea.X + Map.FinalBossArea.Width / 2;
            y = Map.FinalBossArea.Y + Map.FinalBossArea.Height / 2;
            Dragon dr = new Dragon(x, y, 96, 96);
            dr.SetControl(new BossController(this, dr, Map.FinalBossArea));
            EnemieBosses.Add(dr);

        }

        public void InitGameObjects()
        {
            FireWall = new FireWall(Map.FireWall.X, Map.FireWall.Y, Map.FireWall.Width, Map.FireWall.Height);
            Hitboxes.Add(FireWall.GetBounds());
            GameObjects.Add(new Portal(ResourceManager.GetTexture("Portal"), Map.Portal[0].X, Map.Portal[0].Y, 64, 64) { SetDestination = Map.Portal[1] });
            GameObjects.Add(new Portal(ResourceManager.GetTexture("Portal"), Map.Portal[1].X, Map.Portal[1].Y, 64, 64) { SetDestination = Map.Portal[0] });
        }

        public void InitBuildings()
        {
            HeroBuildings = new List<Building>();
            EnemyBuildings = new List<Building>();
            GeneralBuildings = new List<Building>();
            DeadBuildings = new List<Building>();

            if (Map != null)
            {
                InitHeroBuildings();
                InitEnemyBuildings();
                ShopBuildings();
            }

        }
        private void InitHeroBuildings()
        {
            for (int i = 0; i < Map.HeroTowerPos.Count; i++)
                HeroBuildings.Add(new HeroTower(Map.HeroTowerPos[i].X, Map.HeroTowerPos[i].Y));

            HeroBuildings.Add(new HeroCastle(Map.HeroCastle.X, Map.HeroCastle.Y));
            HeroCastleDestroyed = false;

            foreach (Building b in HeroBuildings)
            {
                Hitboxes.Add(b.GetHitbox());
            }

            for (int i = 0; i < Map.HeroBalista.Count; i++)
            {
                HeroBallista temp = new HeroBallista(Map.HeroBalista[i].X, Map.HeroBalista[i].Y);
                temp.SetControl(new TowerController(this, temp));
                HeroBuildings.Add(temp);

            }
        }
        private void InitEnemyBuildings()
        {
            for (int i = 0; i < Map.EnemieTowerPos.Count; i++)
            {
                EnemyTower temp = new EnemyTower(Map.EnemieTowerPos[i].X, Map.EnemieTowerPos[i].Y);
                temp.SetControl(new TowerController(this, temp));
                EnemyBuildings.Add(temp);
            }

            for (int i = 0; i < Map.EnemieSpawnerPos.Count; i++)
            {
                EnemySpawner temp = new EnemySpawner(Map.EnemieSpawnerPos[i].X, Map.EnemieSpawnerPos[i].Y);
                spawnController.AddSpawner(temp);
                EnemyBuildings.Add(temp);
            }

            foreach (Building b in EnemyBuildings)
            {
                Hitboxes.Add(b.GetHitbox());
            }
        }
        private void ShopBuildings()
        {
            for (int i = 0; i < Map.Shop.Count; i++)
                GeneralBuildings.Add(new Shop(Map.Shop[i].X, Map.Shop[i].Y, this));

            foreach (Building b in GeneralBuildings)
                Hitboxes.Add(b.GetHitbox());
        }

        //----- Updates-----//
        public void Update(float delta)
        {
            //Spawn controller
            spawnController.Update(delta);
           
            //Entitys
            UpdatePlayers(delta);
            UpdateEnemies(delta);

            //Buildings
            UpdateEnemyBuildings(delta);
            UpdateHeroBuildings(delta);
            UpdateGeneralBuildings(delta);

            //Game objects
            UpdateGameObjects(delta);

            //Attack collision
            UpdateAttackCollision(delta);

            //Effects
            UpdateEffects(delta);

            DeleteDeadThings();
        }

        //Players / hero buildings
        public void UpdatePlayers(float delta)
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
        private void UpdateHeroBuildings(float delta)
        {
            foreach (var build in HeroBuildings)
            {
                build.Update(delta);

                if (!build.IsAlive)
                    DeadBuildings.Add(build);
            }
        }
        // Enemy / enemys buildings
        private void UpdateEnemies(float delta)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Update(delta);

                if (!enemy.IsAlive)
                {
                    deadEnimies.Add(enemy);
                    EnemyReward(enemy);
                }
            }

            foreach (var boss in EnemieBosses)
            {
                boss.Update(delta);

                if (!boss.IsAlive)
                {
                    deadEnimies.Add(boss);
                    EnemyReward(boss);
                }
            }
        }
        private void UpdateEnemyBuildings(float delta)
        {
            foreach (var build in EnemyBuildings)
            {
                build.Update(delta);

                if (!build.IsAlive)
                    DeadBuildings.Add(build);
            }
        }
        //GeneralBuildings
        private void UpdateGeneralBuildings(float delta)
        {
            foreach (var build in GeneralBuildings)
            {
                build.Update(delta);

                if (build is Shop)
                    ((Shop)build).IsPlayersInRange(delta, new List<Hero>() { PlayerOne, PlayerTwo });
            }
        }

        //Game object
        private void UpdateGameObjects(float delta)
        {
            foreach (var obj in GameObjects)
            {
                if(obj != null)
                    obj.Update(delta);

                if(obj is Portal)
                    IntreactionPortal((Portal)obj, delta);

                if (obj != null && !obj.IsAlive)
                    DeadObjects.Add(obj);
            }

            foreach (var obj in EnemyObjects)
            {
                if (obj != null)
                    obj.Update(delta);

                if (obj != null && !obj.IsAlive)
                    DeadObjects.Add(obj);
            }

            if (FireWall != null)
                FireWall.Update(delta);
        }

        //Attack collision
        private void UpdateAttackCollision(float delta)
        {
            foreach (GameObject obj in GameObjects)
            {
                foreach (Entity e in Enemies)
                {
                    if (obj is Projectile)
                        UpdateProjectileCollision((Projectile)obj, e);
                }

                foreach (Entity e in EnemieBosses)
                {
                    if (obj is Projectile)
                        UpdateProjectileCollision((Projectile)obj, e);
                }

                foreach (Building b in EnemyBuildings)
                {
                    if(obj is Projectile)
                        UpdateProjectileCollision((Projectile)obj, b);
                }
            }

            foreach (GameObject obj in EnemyObjects)
            {

                if (obj is Projectile)
                {
                    Projectile p = (Projectile)obj;

                    if (p.target == null)
                        foreach (Hero h in new List<Hero> { PlayerOne, PlayerTwo})
                        {
                            if (h == null)
                                continue;
                            UpdateProjectileCollision(p, h);
                        }
                    else
                        UpdateProjectileCollision(p);
                }

                if(obj is LavaFloor)
                {
                    LavaFloor lava = (LavaFloor)obj;
                    foreach (Hero h in new List<Hero> { PlayerOne, PlayerTwo })
                    {
                        if (h == null)
                            continue;
                        if (h.GetBounds().Intersects(lava.GetBounds()))
                        {
                            h.Hit(lava.GetDamage(delta));
                        }
                    }
                }
            }
        }

        //Collision Enemy Entitys
        private void UpdateProjectileCollision(Projectile pro, Entity target = null)
        {
            //Set collision and hit damage
            if(pro.target == null)
            {
                if (pro.GetBounds().Intersects(target.GetBounds()) && !pro.Collision)
                {
                    target.Hit(pro.GetDamage);
                    pro.Collision = true;
                    pro.IsAlive = false;
                }
            }
            else
            {
                if (pro.AttackCollision())
                {
                    pro.target.Hit(pro.GetDamage);
                    pro.Collision = true;
                    pro.IsAlive = false;
                }
            }

            //Set collision effect
            if (pro.Collision)
            {
                if(pro.target != null)
                    SpawnEffect(pro.GetCollisionFX(), pro.target.Position);
                else
                    SpawnEffect(pro.GetCollisionFX(), pro.Position);
            }
        }
        //Collision Enemy buildings
        private void UpdateProjectileCollision(Projectile pro, Building eBuilding)
        {
            //Set collision and hit damage
            if (pro.target == null)
            {
                if (pro.GetBounds().Intersects(eBuilding.GetBounds()) && !pro.Collision)
                {
                    eBuilding.Hit(pro.GetDamage);
                    pro.Collision = true;
                    pro.IsAlive = false;
                }
            }
            else
            {
                if (pro.AttackCollision())
                {
                    pro.target.Hit(pro.GetDamage);
                    pro.Collision = true;
                    pro.IsAlive = false;
                }
            }

            //Set collision effect
            if (pro.Collision)
            {
                if (pro.target != null)
                    SpawnEffect(pro.GetCollisionFX(), pro.target.Position);
                else
                    SpawnEffect(pro.GetCollisionFX(), pro.Position);
            }
        }

        //Effects
        public void UpdateEffects(float delta)
        {
            foreach (var fx in Effects)
            {
                fx.Update(delta);

                if (fx.Done)
                    FXPool.ReleaseObject(fx);
            }
            Effects.RemoveAll(x => x.Done);
        }

        //Delete
        private void DeleteDeadThings()
        {
            foreach (GameObject ob in DeadObjects)
            {
                GameObjects.Remove(ob);
                EnemyObjects.Remove(ob);
            }
            DeadObjects.Clear();

            foreach (Entity e in deadEnimies)
            {
                Enemies.Remove(e);

                if (e is Demon)
                {
                    FireWall.RemoveLive();
                    if (!FireWall.IsAlive)
                    {
                        Hitboxes.Remove(new Rectangle((int)FireWall.Position.X, (int)FireWall.Position.Y,
                                                      FireWall.GetBounds().Width, FireWall.GetBounds().Height));
                        FireWall = null;
                    }      
                }
                if (e is Dragon)
                    DragonDefeated = true;

                EnemieBosses.Remove(e);
            }
            deadEnimies.Clear();

            foreach (Building b in DeadBuildings)
            {
                SpawnEffect(b.GetDeathFX(), b.Position);

                Hitboxes.Remove(b.GetHitbox());

                HeroBuildings.Remove(b);
                EnemyBuildings.Remove(b);

                if (b is EnemySpawner)
                    spawnController.RemoveSpawner((EnemySpawner)b);
                if(b is HeroCastle)
                    HeroCastleDestroyed = true;
            }
            DeadBuildings.Clear();
        }

        private void EnemyReward(Entity e)
        {
            if (PlayerOne != null && PlayerOne.IsAlive)
            {
                if (e is Enemy)
                {
                    PlayerOne.IncreaseXP(((Enemy)e).GetXPWorth);
                    PlayerOne.IncreaseGold(((Enemy)e).GetGold);
                }
                else
                {
                    //Building
                }
            }
            if (PlayerTwo != null && PlayerTwo.IsAlive)
            {
                if (e is Enemy)
                {
                    PlayerTwo.IncreaseXP(((Enemy)e).GetXPWorth);
                    PlayerTwo.IncreaseGold(((Enemy)e).GetGold);
                }
                else
                {
                    //Building
                }
            }
        }

        private void IntreactionPortal(Portal p, float delta)
        {
            if (PlayerOne != null)
                p.PlayerOnTeleporter(delta, PlayerOne);

            if (PlayerTwo != null)
                p.PlayerOnTeleporter(delta, PlayerTwo);
        }

        //----- Functions-----//
        public void SpawnEffect(EffectType type, Vector2 pos, float r = -1, float g = -1, float b = -1)
        {
            var fx = FXPool.GetObject();
            fx.SetPosition(pos);
            fx.ZIndex = 0.023f;
            fx.Color = Color.White;
            if (r >= 0 && g >= 0 && b >= 0)
                fx.Color = new Color(r, g, b);

            switch (type)
            {
                case EffectType.Mega_Explosion:
                    fx.SetSize(128, 128);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Big_Explosion"), 0, 0, 64, 64, 16, 0.08f, new Point(8, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Big_Explosion:
                    fx.SetSize(64, 64);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Big_Explosion"), 0, 0, 64, 64, 16, 0.08f, new Point(8, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Medium_Explosion:
                    fx.SetSize(64, 64);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Medium_Explosion"), 0, 128, 62, 62, 6, 0.08f, new Point(3, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Light_Magic_Explosion:
                    fx.SetSize(64, 64);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Light_Magic_Explosion"), 196, 128, 62, 62, 6, 0.08f, new Point(3, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Dark_Magic_Explosion:
                    fx.SetSize(64, 64);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Dark_Magic_Explosion"), 0, 256, 62, 62, 6, 0.08f, new Point(6, 1), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Frost_Hit:
                    fx.SetSize(32, 32);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Frost_Hit"), 384, 196, 32, 32, 5, 0.08f, new Point(2, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Brown_Hit:
                    fx.SetSize(32, 32);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Brown_Hit"), 384, 128, 32, 32, 4, 0.08f, new Point(2, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Fire_Hit:
                    fx.SetSize(32, 32);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Fire_Hit"), 448, 128, 32, 32, 4, 0.08f, new Point(2, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Fire_Storm:
                    fx.SetSize(64, 64);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Fire_Storm"), 0, 0, 64, 64, 10, 0.08f, new Point(5, 2), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Burning:
                    fx.SetSize(32, 32);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Burning"), 0, 128, 32, 32, 6, 0.08f, new Point(6, 1), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Magic_Particle:
                    fx.SetSize(64, 64);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Magic_Particle"), 0, 160, 32, 32, 6, 0.08f, new Point(6, 1), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Soul_Spin:
                    fx.SetSize(32, 32);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Soul_Spin"), 0, 192, 32, 32, 5, 0.08f, new Point(5, 1), false, false)).SetAnimation("fx");
                    break;
                case EffectType.Fire_Emit:
                    fx.SetSize(32, 32);
                    fx.AddAnimation("fx", new FrameAnimation(ResourceManager.GetTexture("Fire_Emit"), 0, 224, 32, 32, 3, 0.08f, new Point(3, 1), false, false)).SetAnimation("fx");
                    break;
                case EffectType.NONE:
                    FXPool.ReleaseObject(fx);
                    return;
                default:
                    FXPool.ReleaseObject(fx);
                    return;
            }

            var sx = fx.Size * 0.5f;
            fx.DrawOffset = new Vector2(sx.X, sx.Y);
            Effects.Add(fx);
        }
        public void SpawnEffect(string name, FrameAnimation animation, SpriteEffects effect, Vector2 pos, Point size)
        {
            var fx = FXPool.GetObject();
            fx.SetPosition(pos);
            fx.ZIndex = 0.023f;
            fx.Color = Color.White;

            fx.Effect = effect;
            fx.SetSize(size.X, size.Y);
            fx.AddAnimation(name, animation).SetAnimation(name);

            var sx = fx.Size * 0.5f;
            fx.DrawOffset = new Vector2(sx.X, sx.Y);
            Effects.Add(fx);
        }

        public void Restart()
        {
            this.Effects = new List<SpriteFX>();
            this.FXPool = new SpriteFXPool();
            Initmap(gameSettings.MapName);
            this.spawnController = new SpawnController(this, gameSettings);
            InitBuildings();
            InitEntitys();
        }
    }
}