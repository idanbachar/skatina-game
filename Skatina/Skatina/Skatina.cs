using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Skatina
{
    public class Skatina : Game
    {
        public static GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;
        public static ContentManager GameContent;
        private Map Map;
        private Player Player;
        private Camera Camera;

        private SpriteFont TitleFont;

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
            TitleFont = GameContent.Load<SpriteFont>("fonts/TitleFont");
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

            Camera.Focus(Player.Position,
                         Map.Levels[Map.CurrentLevelIndex].GetWidth(),
                         Map.Levels[Map.CurrentLevelIndex].GetHeight()
                         );

            Player.Update(gameTime, Map);

            foreach (Entity entity in Map.Levels[Map.CurrentLevelIndex].LevelEntities)
                entity.Update(gameTime, Map);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);

            Map.Draw(SpriteBatch);
            Player.Draw(SpriteBatch);

            SpriteBatch.End();

            SpriteBatch.Begin();


            SpriteBatch.DrawString(TitleFont, "Tries: " + Map.Levels[Map.CurrentLevelIndex].Tries, new Vector2(0, 0), Color.Black);

            SpriteBatch.DrawString(TitleFont, "Level (" + (Map.CurrentLevelIndex + 1) + "/" + Map.Levels.Length + ")", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 50, 50), Color.Black);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
