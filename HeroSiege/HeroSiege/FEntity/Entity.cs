using HeroSiege.FGameObject;
using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.FEntity
{
    class Entity : GameObject
    {
        //public Control Control { get; protected set; }
        public StatsData Stats { get; protected set; }
        protected bool IsAlive { get; set; }

        public Entity(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            this.IsAlive = true;
        }

        public override void Update(float delta)
        {
            base.Update(delta);

           /*
            * if (Stats.Speed < 1.0f)
                 sprite.PauseAnimation = true;
            else
                sprite.PauseAnimation = false;
           */
        }
    }
}
