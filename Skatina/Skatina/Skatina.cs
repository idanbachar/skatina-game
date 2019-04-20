using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Skatina
{
    public class Skatina : Game
    {
        private GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;
        public static ContentManager GameContent;
        private Map Map;
        private Player Player;
        private Camera Camera;

        public Skatina()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Graphics.PreferredBackBufferWidth = 600;
            Graphics.PreferredBackBufferHeight = 700;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            GameContent = Content;

            Camera = new Camera(GraphicsDevice.Viewport);

            Map = new Map();
            Player = new Player(new Vector2(0, 0));
            Player.LoadContent(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Camera.Focus(Player.Position, Map.Levels[Map.CurrentLevelIndex].GetWidth * 100, Map.Levels[Map.CurrentLevelIndex].GetHeight * 50);



            Player.Update(gameTime, Map);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);

            Map.Draw(SpriteBatch);
            Player.Draw(SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
