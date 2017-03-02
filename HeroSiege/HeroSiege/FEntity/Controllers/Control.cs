using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity.Controllers
{
    public class Control
    {
        public GameScene scene { get; private set; }
        public Entity entity;

        public Control(GameScene scene, Entity entity)
        {
            this.entity = entity;
            this.scene = scene;
        }

        public virtual void Update(float delta) { }

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }
    }
}
