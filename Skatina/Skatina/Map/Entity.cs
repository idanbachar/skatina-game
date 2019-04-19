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
        public bool IsOnTopOfEntity;
        public bool Visible;

        public Entity(Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);

            Gravity = true;
            IsOnTopOfEntity = false;
            Visible = true;
        }

        public virtual void SetPosition(Vector2 position)
        {
            Position = new Vector2(position.X, position.Y);
        }

        public virtual void Fall()
        {
            SetPosition(new Vector2(Position.X, Position.Y + 2f));
        }

        public virtual void CheckIntersectsWithEntities(Entity [,] entities)
        {
            foreach (Entity entity in entities)
            {
                if (Rectangle.Intersects(entity.Rectangle) && entity.Visible)
                {
                    if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top + 50 < entity.Rectangle.Bottom)
                        if ((Rectangle.Left + 25 >= entity.Rectangle.Left && Rectangle.Left + 125 <= entity.Rectangle.Right) &&
                            (Rectangle.Right <= entity.Rectangle.Right && Rectangle.Right >= entity.Rectangle.Left))
                            IsOnTopOfEntity = true;
                        else
                        {
                            IsOnTopOfEntity = false;
                        }
                }
            }
        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void Update(GameTime gametime)
        {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);

            if (Gravity)
            {
                if (!IsOnTopOfEntity)
                    Fall();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Visible)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
