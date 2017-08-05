using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroSiege.FTexture2D;
using Microsoft.Xna.Framework;
using HeroSiege.FEntity;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.Tools;

namespace HeroSiege.FGameObject
{
    class Portal : GameObject
    {
        const float TIME_TO_TELEPORT = 2;
        Vector2 destination;
        float timer;
        public Portal(TextureRegion region, float x, float y, float width, float height) 
            : base(region, x, y, width, height)
        {
            IsAlive = true;
            boundingBox = new Rectangle((int)x - region.region.Width / 2, (int)y - region.region.Height / 2, region.region.Width, region.region.Height);
        }

        public void PlayerOnTeleporter(float delta, Entity player)
        {
            if (boundingBox.Contains(player.GetBounds()) || boundingBox.Intersects(player.GetBounds()))
            {
                //PARTICLE EFFECT HERES

                timer += delta;
                if (timer >= TIME_TO_TELEPORT)
                {
                    player.SetPosition(destination);
                    timer = 0;
                }
            }
        }

        public override void Draw(SpriteBatch SB)
        {
            base.Draw(SB);

            if(DevTools.DevDebugMode == true)
                DrawBoundingBox(SB);
        }

        public Vector2 SetDestination
        {
            set { destination = value; }
        }
    }
}
