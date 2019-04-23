using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skatina
{
    public class Bullet : Entity
    {
        public const int Width = 36;
        public const int Height = 21;

        private int MoveTimer;
        private Direction MoveDirection;

        public Bullet(Vector2 position, Direction moveDirection) : base(position)
        {
            Gravity = false;
            MoveTimer = 0;
            MoveDirection = moveDirection;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/map/bullet");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public override void Update(GameTime gametime, Map map)
        {
            base.Update(gametime, map);

            Move();
        }

        public void Move()
        {
            if (MoveDirection == Direction.Right)
            {
                SetPosition(new Vector2(Position.X + 3, Position.Y));
            }

            if (MoveDirection == Direction.Left)
            {
                SetPosition(new Vector2(Position.X - 3, Position.Y));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
