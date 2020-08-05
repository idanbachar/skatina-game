using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Skatina {
    public class Skatina : Game {

        public static GraphicsDeviceManager Graphics; //Graphics
        private SpriteBatch SpriteBatch; //SpriteBatch
        public static ContentManager GameContent; //ContentManager
        private Map Map; //Map
        private Player Player; //Player
        private Camera Camera; //Camera
        private MainMenu MainMenu; //Main Menu
        private SpriteFont TitleFont; //Title font
        public static GameState GameState; //Gamestate
        public static string PlayerName; //Player's name

        private bool IsGameFinished; //Game finished indication

        /// <summary>
        /// Creates Skatina
        /// </summary>
        public Skatina() {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Graphics.PreferredBackBufferWidth = 1000;
            Graphics.PreferredBackBufferHeight = 700;
            IsGameFinished = false;
            GameState = GameState.MainMenu;
            PlayerName = string.Empty;
        }

        /// <summary>
        /// Exit game
        /// </summary>
        public static void ExitGame() {
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        protected override void Initialize() {
            base.Initialize();
        }

        /// <summary>
        /// Load Skatina
        /// </summary>
        protected override void LoadContent() {
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

        /// <summary>
        /// Unload Skatina
        /// </summary>
        protected override void UnloadContent() {

        }

        /// <summary>
        /// Update Skatina
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (GameState) {
                case GameState.Game:

                    if (!IsGameFinished) {
                        Camera.Focus(Player.Position,
                                     Map.Levels[Map.CurrentLevelIndex].GetWidth(),
                                     Map.Levels[Map.CurrentLevelIndex].GetHeight()
                                     );

                        Player.Update(gameTime, Map);


                        if (Player.CurrentFinishFloor != null) {
                            if (Map.CurrentLevelIndex + 1 == Map.Levels.Length) {
                                IsGameFinished = true;
                            }
                            else {
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

        /// <summary>
        /// Draw Skatina
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            switch (GameState) {
                case GameState.Game:
                    if (!IsGameFinished) {
                        SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);

                        Map.Draw(SpriteBatch);
                        Player.Draw(SpriteBatch);

                        SpriteBatch.End();

                        SpriteBatch.Begin();


                        SpriteBatch.DrawString(TitleFont, "-Fails: " + Map.Levels[Map.CurrentLevelIndex].Fails, new Vector2(10, 0), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "-" + PlayerName, new Vector2(10, 25), Color.Red);

                        SpriteBatch.DrawString(TitleFont, "Level " + (Map.CurrentLevelIndex + 1) + "/" + Map.Levels.Length, new Vector2(Graphics.PreferredBackBufferWidth / 2 - 50, 50), Color.Red);

                        SpriteBatch.End();

                    }
                    else {
                        SpriteBatch.Begin();
                        SpriteBatch.DrawString(TitleFont, "Skatina", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 40, 25), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "Game Over!", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 60, 150), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "*You Tried " + Map.GetTotalTries() + " times in total.", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 120, 180), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "*You Finished in level " + (Map.CurrentLevelIndex + 1) + "/" + Map.Levels.Length + ".", new Vector2(Graphics.PreferredBackBufferWidth / 2 - 120, 210), Color.Red);
                        SpriteBatch.DrawString(TitleFont, "Hope you enjoyed!\nAll rights reserved to Idan Bachar.", new Vector2(0, Graphics.PreferredBackBufferHeight - 70), Color.Red);
                        SpriteBatch.End();
                    }
                    SpriteBatch.Begin();
                    SpriteBatch.DrawString(TitleFont, "Made by Idan Bachar.", new Vector2(10, Graphics.PreferredBackBufferHeight - 30), Color.Red);
                    SpriteBatch.End();

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
