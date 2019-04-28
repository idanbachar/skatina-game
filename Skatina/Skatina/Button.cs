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
    public class Button
    {
        public const int Width = 160;
        public const int Height = 57;

        public ButtonType ButtonType;
        public Texture2D Texture;
        public Rectangle Rectangle;

        public Button(ButtonType buttonType, Rectangle rectangle)
        {
            ButtonType = buttonType;
            Rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/buttons/" + ButtonType.ToString());
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 15, 10).Intersects(Rectangle) ? Color.WhiteSmoke : Color.White);
        }
    }
}
