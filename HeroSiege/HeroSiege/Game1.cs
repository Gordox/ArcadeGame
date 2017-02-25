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
           
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            base.Draw(gameTime);
        }
    }
}
