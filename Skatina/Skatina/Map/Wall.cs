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
    public class Wall : Entity
    {
        public const int Width = 18;
        public const int Height = 88;
        public bool IsMove;
        private int MoveTimer;
        private Direction MoveDirection;

        public Wall(Vector2 position) : base(position)
        {
            Gravity = false;
            IsMove = false;
            MoveTimer = 0;
            MoveDirection = Direction.Up;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/map/wall");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public override void Update(GameTime gametime, Map map)
        {
            base.Update(gametime, map);

            if (IsMove)
            {
                Move();
            }
        }

        public void Move()
        {
            if (MoveTimer < 50)
            {
                MoveTimer++;

                if (MoveDirection == Direction.Up)
                    SetPosition(new Vector2(Position.X, Position.Y - 3));

                if (MoveDirection == Direction.Down)
                    SetPosition(new Vector2(Position.X, Position.Y + 3));
            }
            else
            {
                MoveTimer = 0;
                SwapDirection();
            }
        }

        private void SwapDirection()
        {
            if (MoveDirection == Direction.Up)
                MoveDirection = Direction.Down;
            else if (MoveDirection == Direction.Down)
                MoveDirection = Direction.Up;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
