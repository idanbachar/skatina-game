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

        private float Speed;

        private Vector2 MaxRightPosition;
        private Vector2 MinLeftPosition;

        public Floor(Vector2 position, FloorType floorType) : base(position)
        {
            Gravity = false;
            FloorType = floorType;
            MoveDirection = Direction.Right;
            Speed = 1f;
        }

        public void Move()
        {
            switch (MoveDirection)
            {
                case Direction.Right:
                    if (Position.X <= MaxRightPosition.X)
                        SetPosition(new Vector2(Position.X + Speed, Position.Y));
                    else
                    {
                        SwapDirection();
                    }
                    break;
                case Direction.Left:
                    if (Position.X >= MinLeftPosition.X)
                        SetPosition(new Vector2(Position.X - Speed, Position.Y));
                    else
                    {
                        SwapDirection();
                    }
                    break;
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
            MaxRightPosition = new Vector2(Position.X + Width + Width / 2, Position.Y);
            MinLeftPosition = new Vector2(Position.X, Position.Y);
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
            if (Visible)
            {
                switch (FloorType)
                {
                    case FloorType.Regular:
                    case FloorType.Moving:
                        spriteBatch.Draw(Texture, Rectangle, Color.White);
                        break;
                    case FloorType.Finish:
                        spriteBatch.Draw(Texture, Rectangle, Color.LightGreen);
                        break;
                    case FloorType.Jump:
                        spriteBatch.Draw(Texture, Rectangle, Color.LightSkyBlue);
                        break;
                    case FloorType.Deadly:
                        spriteBatch.Draw(Texture, Rectangle, Color.Red);
                        break;
                }                    
            }
        }
    }
}
