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
    public class InputText
    {
        public const int Width = 362;
        public const int Height = 62;

        public Texture2D Texture;
        public Rectangle Rectangle;

        public InputText(Rectangle rectangle)
        {
            Rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/text/inputText");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.WhiteSmoke);
        }
    }
}
