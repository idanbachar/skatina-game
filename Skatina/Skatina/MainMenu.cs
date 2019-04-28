using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatina
{
    public class MainMenu
    {
        private Keys[] lastPressedKeys = new Keys[1];
        public string Name;
        private Button[] Buttons;
        private Button OkButton;
        private InputText InputText;
        private bool IsPressedLeftButton;
        private bool caps;

        private SpriteFont Font;

        public MainMenu()
        {
            Buttons = new Button[2];
            IsPressedLeftButton = false;
            Name = string.Empty;
        }

        public void LoadContent(ContentManager content)
        {
            Font = content.Load<SpriteFont>("fonts/TitleFont");

            InputText = new InputText(new Rectangle(Skatina.Graphics.PreferredBackBufferWidth / 2 - InputText.Width / 2,
                                        Skatina.Graphics.PreferredBackBufferHeight / 2 - InputText.Height / 2,
                                        InputText.Width,
                                        InputText.Height
                                        ));
            InputText.LoadContent(content);


            OkButton = new Button(ButtonType.Ok, new Rectangle(InputText.Rectangle.Left + InputText.Rectangle.Width / 2,
                                                               InputText.Rectangle.Bottom + 20,
                                                               Button.Width,
                                                               Button.Height
                                                               ));
            OkButton.LoadContent(content);

            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i] = new Button((ButtonType)i, new Rectangle(Skatina.Graphics.PreferredBackBufferWidth / 2 - Button.Width / 2,
                                                                     Skatina.Graphics.PreferredBackBufferHeight / 2 - Button.Height + (i * Button.Height) + 10 * i,
                                                                     Button.Width,
                                                                     Button.Height
                                                                     ));
                Buttons[i].LoadContent(content);
            }

        }

        public void Update(GameTime gameTime)
        {
            switch (Skatina.GameState)
            {
                case GameState.MainMenu:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        foreach (Button button in Buttons)
                        {
                            if (new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 15, 10).Intersects(button.Rectangle) && !IsPressedLeftButton)
                            {
                                IsPressedLeftButton = true;

                                switch (button.ButtonType)
                                {
                                    case ButtonType.Play:
                                        Skatina.GameState = GameState.EnterName;
                                        break;
                                    case ButtonType.Exit:
                                        Skatina.ExitGame();
                                        break;
                                }

                                break;
                            }
                        }

                    }

                    break;
                case GameState.EnterName:
                    GetKeys();
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 15, 10).Intersects(OkButton.Rectangle) && !IsPressedLeftButton)
                        {
                            IsPressedLeftButton = true;
                            Skatina.GameState = GameState.Game;
                            Skatina.PlayerName = Name;
                        }
                    }
                    break;
            }


            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                IsPressedLeftButton = false;
            }
        }

        private void GetKeys()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            //check if any of the previous update's keys are no longer pressed
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                    OnKeyUp(key);
            }

            //check if the currently pressed keys were already pressed
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                    OnKeyDown(key);
            }
            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

        private void OnKeyDown(Keys key)
        {
            //do stuff
            if (key == Keys.Back && Name.Length > 0) //Removes a letter from the name if there is a letter to remove
            {
                Name = Name.Remove(Name.Length - 1);
            }
            else if (key == Keys.LeftShift || key == Keys.RightShift)//Sets caps to true if a shift key is pressed
            {
                caps = true;
            }
            else if (!caps && Name.Length < 16) //If the name isn't too long, and !caps the letter will be added without caps
            {
                if (key == Keys.Space)
                {
                    Name += " ";
                }
                else
                {
                    Name += key.ToString().ToLower();
                }
            }
            else if (Name.Length < 16) //Adds the letter to the name in CAPS
            {
                Name += key.ToString();
            }
        }

        private void OnKeyUp(Keys key)
        {
            //Sets caps to false if one of the shift keys goes up
            if (key == Keys.LeftShift || key == Keys.RightShift)
            {
                caps = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Skatina.GameState == GameState.MainMenu)
            {
                spriteBatch.DrawString(Font, "Skatina!", new Vector2(Buttons[0].Rectangle.Left + 30, Buttons[0].Rectangle.Top - 40), Color.Black);

                foreach (Button button in Buttons)
                    button.Draw(spriteBatch);
            }
            else if (Skatina.GameState == GameState.EnterName) {

                spriteBatch.DrawString(Font, "Enter Name:", new Vector2(InputText.Rectangle.Left + 30, InputText.Rectangle.Top - 40), Color.Black);
                InputText.Draw(spriteBatch);
                spriteBatch.DrawString(Font, Name, new Vector2(InputText.Rectangle.Left + 20, InputText.Rectangle.Top + 18), Color.Red);
                OkButton.Draw(spriteBatch);
            }
        }
    }
}
