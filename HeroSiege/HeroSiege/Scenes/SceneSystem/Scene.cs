using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HeroSiege.Scenes.SceneSystem
{
     // current state the scene is in
    public abstract class Scene
    {
        public GraphicsDeviceArcade Graphics { get; set; }

        public Game1 Game { get; set; }

        public abstract void Init();
        public abstract void Update(float delta);
        public abstract void Draw(SpriteBatch SB);
    }
        
}
