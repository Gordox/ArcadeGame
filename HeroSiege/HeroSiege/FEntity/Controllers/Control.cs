using HeroSiege.FEntity.Buildings;
using HeroSiege.FEntity.Enemies;
using HeroSiege.FEntity.Players;
using HeroSiege.GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Controllers
{
    class Control
    {
        public World world { get; private set; }
        public Entity entity;
        public Building building;
        public Hero player;
        public Enemy enemy;

        //----- Constructos -----//
        public Control(World world, Entity entity)
        {
            this.entity = entity;
            this.world = world;
        }
        public Control(World world, Hero player)
        {
            this.player = player;
            this.world = world;
        }
        public Control(World world, Enemy enemy)
        {
            this.enemy = enemy;
            this.world = world;
        }
        public Control(World world, Building building)
        {
            this.building = building;
            this.world = world;
        }

        public virtual void Update(float delta) { }

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        public void SetBuilding(Building building)
        {
            this.building = building;
        }
    }
}
