using HeroSiege.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D.SpriteEffect;
using HeroSiege.GameWorld;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.FEntity.Players;

namespace HeroSiege.FEntity.Buildings
{
    class Shop : Building
    {
        World world;

        public bool playerInRange { get; private set; }
        float timeInterval;
        public Shop(float x, float y, World world)
            : base(ResourceManager.GetTexture("Shop_1"), x, y)
        {
            SetHitbox = new Rectangle((int)x - 10, (int)y - 20, 58, 58);
            this.world = world;
        }

        public override void Init()
        {
            base.Init();
        }
        protected override void InitStats()
        {
            Stats = new StatsData();
            Stats.MaxHealth = 1;
            Stats.Armor = 1;
            Stats.Radius = 80;
        }

        public override bool LevelUp(float delta)
        {
            throw new NotImplementedException();
        }
        public override EffectType GetDeathFX()
        {
            return EffectType.NONE;
        }


        public override void Update(float delta)
        {
            base.Update(delta);
           
           
                //IsPlayersInRange();

        }

        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            if (playerInRange) //UI
            {
                string text = "Press A to shop!";
                Vector2 orgin = ResourceManager.GetFont("Arial_Font").MeasureString(text);
                SB.DrawString(ResourceManager.GetFont("Arial_Font"), text, Position, Color.Black, 0, new Vector2(orgin.X / 2, orgin.Y / 2 + 50), 1f, SpriteEffects.None, 1);
            }
        }


        public void IsPlayersInRange(float delta, List<Hero> players)
        {
            timeInterval += delta;
            if (timeInterval < 0.2)
                return;
            foreach (Hero p in players)
            {
                if (p != null && Vector2.Distance(Position, p.Position) <= Stats.Radius)
                {
                    playerInRange = true;
                    return;
                }
                else
                    playerInRange = false;
            }
        }

        private double Dist(Vector2 p1, Vector2 p2)
        {
            float a = (p2.X - p1.X) * (p2.X - p1.X);
            float b = (p2.Y - p2.Y) * (p2.Y - p1.Y);
            double s = Math.Sqrt((a + b));
            return s;
        }


    }
}
