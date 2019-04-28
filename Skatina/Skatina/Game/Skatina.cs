using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private MainMenu MainMenu;
        private SpriteFont TitleFont;
        public static GameState GameState;
        public static string PlayerName;

        private bool IsGameFinished;

        public Skatina()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Graphics.PreferredBackBufferWidth = 600;
            Graphics.PreferredBackBufferHeight = 700;
            IsGameFinished = false;
            GameState = GameState.MainMenu;
            PlayerName = string.Empty;
        }

        public static void ExitGame()
        {
            Environment.Exit(Environment.ExitCode);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            GameContent = Content;

            MainMenu = new MainMenu();
            MainMenu.LoadContent(Content);

            Camera = new Camera(GraphicsDevice.Viewport);
            TitleFont = GameContent.Load<SpriteFont>("fonts/TitleFont");
            Map = new Map();

            Player = new Player(Map.Levels[Map.CurrentLevelIndex].PlayerRespawnPosition);
            Player.LoadContent(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (GameState)
            {
                case GameState.Game:

                    if (!IsGameFinished)
                    {
                        Camera.Focus(Player.Position,
                                     Map.Levels[Map.CurrentLevelIndex].GetWidth(),
                                     Map.Levels[Map.CurrentLevelIndex].GetHeight()
                                     );

                        Player.Update(gameTime, Map);


                        if (Player.CurrentFinishFloor != null)
                        {
                            if (Map.CurrentLevelIndex + 1 == Map.Levels.Length)
                            {
                                IsGameFinished = true;
                            }
                            else
                            {
                                Map.NextLevel();
                                Player.RespawnNewLevel(Map);
                            }
                        }

                        foreach (Entity entity in Map.Levels[Map.CurrentLevelIndex].LevelEntities)
                            entity.Update(gameTime, Map);
                    }
                    break;
                case GameState.MainMenu:
                case GameState.EnterName:
                    MainMenu.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            switch (GameState)
            {
                case GameState.Game:
                    if (!IsGameFinished)
                    {
                        SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);

                        Map.Draw(SpriteBatch);
                        Player.Draw(SpriteBatch);

                        SpriteBatch.End();

                        SpriteBatch.Begin();


                        SpriteBatch.DrawString(TitleFont, "Tries: " + Map.Levels[Map.CurrentLevelIndex].Tries, new Vector2(0, 0), Color.Black);
                        SpriteBatch.DrawString(TitleFont, PlayerName, new Vector2(0, 25), Color.Red);

                        SpriteBatch.DrawString(TitleFont, "Skatina", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 40, 25), Color.Black);

                        SpriteBatch.DrawString(TitleFont, "Level (" + (Map.CurrentLevelIndex + 1) + "/" + Map.Levels.Length + ")", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 50, 50), Color.Black);

                        SpriteBatch.End();

                    }
                    else
                    {
                        SpriteBatch.Begin();
                        SpriteBatch.DrawString(TitleFont, "Skatina", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 40, 25), Color.Black);
                        SpriteBatch.DrawString(TitleFont, "Game Over!", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 60, 150), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "*You Tried " + Map.GetTotalTries() + " times in total.", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 120, 180), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "*You Finished in level " + (Map.CurrentLevelIndex + 1) + "/" + Map.Levels.Length + ".", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 120, 210), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "Hope you enjoyed!\nAll rights reserved to Idan Bachar.", new Vector2(0, Graphics.PreferredBackBufferHeight - 70), Color.Black);
                        SpriteBatch.End();
                    }
                    break;
                case GameState.MainMenu:
                case GameState.EnterName:
                    SpriteBatch.Begin();
                    MainMenu.Draw(SpriteBatch);
                    SpriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
