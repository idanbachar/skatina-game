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
    public class Floor : Entity
    {
        public const int Width = 88;
        public const int Height = 18;

        public FloorType FloorType;
        public Direction MoveDirection;
        private int MoveTimer;
        private int MaxMoveTimer;

        public Floor(Vector2 position, FloorType floorType) : base(position)
        {
            Gravity = false;
            FloorType = floorType;
            MoveTimer = 0;
            MaxMoveTimer = 60;
            MoveDirection = Direction.Right;
        }

        public void Move()
        {
            if (MoveTimer < MaxMoveTimer)
            {
                MoveTimer++;

                if (MoveDirection == Direction.Right)
                {
                    SetPosition(new Vector2(Position.X + 1, Position.Y));
                }

                if (MoveDirection == Direction.Left)
                {
                    SetPosition(new Vector2(Position.X - 1, Position.Y));
                }
            }
            else
            {
                MoveTimer = 0;
                SwapDirection();
            }
        }

        private void SwapDirection()
        {
            if (MoveDirection == Direction.Right)
            {
                MoveDirection = Direction.Left;
            }
            else if (MoveDirection == Direction.Left)
            {
                MoveDirection = Direction.Right;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/map/floor");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public override void Update(GameTime gametime, Map map)
        {
            base.Update(gametime, map);

            if (FloorType == FloorType.Moving)
            {
                Move();
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
