using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skatina {
    public class Bullet : Entity {

        public const int Width = 50; //Bullet's width
        public const int Height = 25; //Bullet's height
        private Direction MoveDirection; //Bullet's move direction
        private float Speed; //Bullet's move speed

        /// <summary>
        /// Receives position, direction and creates a bullet
        /// </summary>
        /// <param name="position"></param>
        /// <param name="moveDirection"></param>
        public Bullet(Vector2 position, Direction moveDirection) : base(position) {
            Gravity = false;
            MoveDirection = moveDirection;
            Speed = 4.5f;
        }

        /// <summary>
        /// Load bullet
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content) {
            Texture = content.Load<Texture2D>("images/map/bullet");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        /// <summary>
        /// Update bullet
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="map"></param>
        public override void Update(GameTime gametime, Map map) {
            base.Update(gametime, map);

            //Move bullet:
            Move();
        }

        /// <summary>
        /// Moves the bullet by direction
        /// </summary>
        public void Move() {
            if (MoveDirection == Direction.Right) {
                SetPosition(new Vector2(Position.X + Speed, Position.Y));
            }

            if (MoveDirection == Direction.Left) {
                SetPosition(new Vector2(Position.X - Speed, Position.Y));
            }
        }

        /// <summary>
        /// Draw bullet
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Rectangle, Color.DarkRed);
        }
    }
}
