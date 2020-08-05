using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatina {
    public class Entity {

        public Texture2D Texture; //Entity's texture
        public Rectangle Rectangle; //Entity's rectangle
        public Vector2 Position; //Entity's position

        public bool Gravity; //Entity's gravity indication
        public bool IsOnTopOfEntity; //Entity's on top of entity indication
        public bool IsOnRightOfEntity; //Entity's on right of entity indication
        public bool IsOnLeftOfEntity; //Entity's on left of entity indication
        public bool Visible; //Entity's visible indication
        public bool IsColide; //Entity's colide indication
        public float FallSpeed; //Entity's fall speed

        /// <summary>
        /// Receives position and creates an entity
        /// </summary>
        /// <param name="position"></param>
        public Entity(Vector2 position) {
            Position = new Vector2(position.X, position.Y);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);

            Gravity = true;
            IsOnTopOfEntity = false;
            IsOnRightOfEntity = false;
            IsOnLeftOfEntity = false;
            Visible = true;
            IsColide = true;
            FallSpeed = 5f;
        }

        /// <summary>
        /// Receives a position and updates it
        /// </summary>
        /// <param name="position"></param>
        public virtual void SetPosition(Vector2 position) {
            Position = new Vector2(position.X, position.Y);
        }

        /// <summary>
        /// Makes the entity fall
        /// </summary>
        public virtual void Fall() {
            SetPosition(new Vector2(Position.X, Position.Y + FallSpeed));
        }

        /// <summary>
        /// Receives list of entities and checks if on top floor, return true else false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual bool IsOnTopFloor(List<Entity> entities) {
            foreach (Entity entity in entities) {
                if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Floor) {
                    if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Top) {
                        if ((Rectangle.Left >= entity.Rectangle.Left && Rectangle.Left <= entity.Rectangle.Right) ||
                            (Rectangle.Right <= entity.Rectangle.Right && Rectangle.Right >= entity.Rectangle.Left)) {
                            IsOnTopOfEntity = true;
                            return true;
                        }
                    }
                }
            }
            IsOnTopOfEntity = false;
            return false;
        }

        /// <summary>
        /// Receives list of entities and checks if on right wall, return true else false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual bool IsOnRightSideWall(List<Entity> entities) {
            foreach (Entity entity in entities) {
                if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Wall) {
                    if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Bottom) {
                        if (Rectangle.Right >= entity.Rectangle.Left && Rectangle.Left <= entity.Rectangle.Left) {
                            IsOnRightOfEntity = true;
                            return true;
                        }
                    }
                }
            }
            IsOnRightOfEntity = false;
            return false;
        }

        /// <summary>
        /// Receives list of entities and checks if on left wall, return true else false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual bool IsOnLeftSideWall(List<Entity> entities) {
            foreach (Entity entity in entities) {
                if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Wall) {
                    if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Bottom) {
                        if (Rectangle.Left <= entity.Rectangle.Right && Rectangle.Right >= entity.Rectangle.Right) {
                            IsOnLeftOfEntity = true;
                            return true;
                        }
                    }
                }
            }
            IsOnLeftOfEntity = false;
            return false;
        }

        /// <summary>
        /// Load entity
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content) {

        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="map"></param>
        public virtual void Update(GameTime gametime, Map map) {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);

            if (Gravity) {
                if (!IsOnTopFloor(map.Levels[map.CurrentLevelIndex].LevelEntities) || !IsColide)
                    Fall();
            }
        }

        /// <summary>
        /// Draw entity
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch) {
            if (Visible)
                spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
