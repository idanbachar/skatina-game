using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatina {
    public class Button {

        public const int Width = 160; //Button's width
        public const int Height = 57; //Button's height

        public ButtonType ButtonType; //Button's type
        public Texture2D Texture; //Button's texture
        public Rectangle Rectangle; //Button's rectangle

        /// <summary>
        /// Receives button type, rectangle and creates a button
        /// </summary>
        /// <param name="buttonType"></param>
        /// <param name="rectangle"></param>
        public Button(ButtonType buttonType, Rectangle rectangle) {
            ButtonType = buttonType;
            Rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// Load button
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content) {
            Texture = content.Load<Texture2D>("images/buttons/" + ButtonType.ToString());
        }

        /// <summary>
        /// Update button
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) {

        }

        /// <summary>
        /// Draw button
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Rectangle, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 15, 10).Intersects(Rectangle) ? Color.WhiteSmoke : Color.White);
        }
    }
}
