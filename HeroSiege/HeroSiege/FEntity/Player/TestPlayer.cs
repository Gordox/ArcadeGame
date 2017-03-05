using HeroSiege.FTexture2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.FEntity.Player
{
    class TestPlayer : Entity
    {
        public TestPlayer(TextureRegion region, float x, float y, float width, float height)
            : base(region, x, y, width, height)
        {
            InitStats();
        }


        public override void Update(float delta)
        {
            base.Update(delta);
        }

        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxSpeed = 400;
            Stats.Speed = 200;
            Stats.Health = 1;
            Stats.Mana = 1;
            base.InitStats();

           
        }


        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            DrawBoundingBox(SB);
        }
    }
}
