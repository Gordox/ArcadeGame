using HeroSiege.GameWorld.map;
using HeroSiege.Manager;
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
        //Tile test;
        TileMap Test;
        FPS_Counter fpsCounter;
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

            Test = new TileMap();
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


            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //test.Draw(spriteBatch);

            Test.DrawMapTexture(spriteBatch);

            spriteBatch.End();

            //Draw stats info ONLY!
            spriteBatch.Begin();
            if (DevTools.DevDebugMode)
            {
                fpsCounter.DrawFpsCount(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
