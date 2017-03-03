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

        public Control(World world, Entity entity)
        {
            this.entity = entity;
            this.world = world;
        }

        public virtual void Update(float delta) { }

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }
    }
}
