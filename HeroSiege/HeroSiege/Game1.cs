using HeroSiege.GameWorld.map;
using HeroSiege.Manager;
using HeroSiege.Scenes;
using HeroSiege.Scenes.SceneSystem;
using HeroSiege.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeroSiege
{
   
    public class Game1 : Game
    {
#if (!ARCADE)
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
#else
        FPS_Counter fpsCounter;
        GameSettings settings;
        public override string GameDisplayName { get { return "HeroSiege"; } }
#endif

        public Game1()
        {
#if (!ARCADE)
            graphics = new GraphicsDeviceManager(this);
#endif
        }

        protected override void Initialize()
        {
            Content.RootDirectory = "Content";
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
#if (!ARCADE)
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
#endif


            fpsCounter = new FPS_Counter();
            ResourceManager.LoadResouces(Content);
            settings = new GameSettings();


            SceneManager.Initialize(this);
            SceneManager.AddScene(new StartScene(settings, GraphicsDevice));
        }

      
        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
#if (!ARCADE)
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
#endif

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            SceneManager.Update(delta);

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(40, 84, 12));

            // ----- Draw scene -----//
            SceneManager.Draw(spriteBatch);
            
            // ----- Draw stats info ONLY! -----//
            spriteBatch.Begin();
            if (DevTools.DevDebugMode || DevTools.DevTopBarInfo || DevTools.DevShowFPS)
            {
                fpsCounter.DrawFpsCount(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
