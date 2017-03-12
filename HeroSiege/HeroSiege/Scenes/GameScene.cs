using HeroSiege.Scenes.SceneSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using HeroSiege.GameWorld;
using HeroSiege.Render;
using HeroSiege.Tools;

namespace HeroSiege.Scenes
{
    class GameScene : Scene
    {

        public World World { get; private set; }
        public WorldRender Renderer { get; private set; }
        public GameSettings GameSettings { get; private set; }

        public GameScene(GameSettings gameSettings, GraphicsDeviceArcade graphics) : base() { this.GameSettings = gameSettings; Graphics = graphics; Init();  }

        public override void Init()
        {
            this.World = new World(GameSettings);
            this.Renderer = new WorldRender(World, Graphics);
        }

        public override void Update(float delta, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            Renderer.Update(delta);
            World.Update(delta);

            base.Update(delta, otherSceneHasFocus, coveredByOtherScene);
        }

        public override void Draw(SpriteBatch SB)
        {
            Renderer.Draw(SB);
        }

    }
}
