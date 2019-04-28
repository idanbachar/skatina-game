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
        private Button[] Buttons;
        private bool IsPressedLeftButton;


        public MainMenu()
        {
            Buttons = new Button[2];
            IsPressedLeftButton = false;
        }

        public void LoadContent(ContentManager content)
        {
            for(int i = 0; i < Buttons.Length; i++)
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
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                foreach(Button button in Buttons)
                {
                    if(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 15, 10).Intersects(button.Rectangle) && !IsPressedLeftButton)
                    {
                        IsPressedLeftButton = true;

                        switch (button.ButtonType)
                        {
                            case ButtonType.Play:
                                Skatina.GameState = GameState.Game;
                                break;
                            case ButtonType.Exit:
                                Skatina.ExitGame();
                                break;
                        }

                        break;
                    }
                }
            }

            if(Mouse.GetState().LeftButton == ButtonState.Released)
            {
                IsPressedLeftButton = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in Buttons)
                button.Draw(spriteBatch);
        }
    }
}
