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
    public class InputText {

        public const int Width = 362; //Input text's width
        public const int Height = 62; //Input text's height

        public Texture2D Texture; //Input text's texture
        public Rectangle Rectangle; //Input text's rectangle

        /// <summary>
        /// Receives rectangle and creats input text
        /// </summary>
        /// <param name="rectangle"></param>
        public InputText(Rectangle rectangle) {
            Rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// Load input text
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content) {
            Texture = content.Load<Texture2D>("images/text/inputText");
        }

        /// <summary>
        /// Update input text
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) {

        }

        /// <summary>
        /// Draw input text
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Rectangle, Color.WhiteSmoke);
        }
    }
}
