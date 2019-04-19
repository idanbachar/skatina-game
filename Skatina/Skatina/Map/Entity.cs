using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatina
{
    public class Entity
    {
        public Texture2D Texture;
        public Rectangle Rectangle;
        public Vector2 Position;

        public bool Gravity;

        public Entity(Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);

            Gravity = true;
        }

        public virtual void SetRectangle(Rectangle rectangle)
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            Position = new Vector2((int)rectangle.X, (int)rectangle.Y);
        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void Update(GameTime gametime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
