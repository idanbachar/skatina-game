using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skatina {
    public class Floor : Entity {

        public const int Width = 150; //Floor's width
        public const int Height = 35; //Floor's height

        public FloorType FloorType; //Floor's type
        public Direction MoveDirection; //Floor's move direction

        private float Speed; //Floor's speed

        private Vector2 MaxRightPosition; //Floor's max right position he can move
        private Vector2 MinLeftPosition; //Floor's min left position he can move

        /// <summary>
        /// Receives position, floor type and creates a floor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="floorType"></param>
        public Floor(Vector2 position, FloorType floorType) : base(position) {
            Gravity = false;
            FloorType = floorType;
            MoveDirection = Direction.Right;
            Speed = 2f;
        }

        /// <summary>
        /// Moves the floor by direction
        /// </summary>
        public void Move() {
            switch (MoveDirection) {
                case Direction.Right:
                    if (Position.X <= MaxRightPosition.X)
                        SetPosition(new Vector2(Position.X + Speed, Position.Y));
                    else {
                        SwapDirection();
                    }
                    break;
                case Direction.Left:
                    if (Position.X >= MinLeftPosition.X)
                        SetPosition(new Vector2(Position.X - Speed, Position.Y));
                    else {
                        SwapDirection();
                    }
                    break;
            }
        }

        /// <summary>
        /// Swap direction to oposite direction
        /// </summary>
        private void SwapDirection() {
            if (MoveDirection == Direction.Right) {
                MoveDirection = Direction.Left;
            }
            else if (MoveDirection == Direction.Left) {
                MoveDirection = Direction.Right;
            }
        }

        /// <summary>
        /// Load floor
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content) {
            Texture = content.Load<Texture2D>("images/map/floor");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            MaxRightPosition = new Vector2(Position.X + Width + Width / 2, Position.Y);
            MinLeftPosition = new Vector2(Position.X, Position.Y);
        }

        /// <summary>
        /// Update floor
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="map"></param>
        public override void Update(GameTime gametime, Map map) {

            base.Update(gametime, map);

            if (FloorType == FloorType.Moving)
                Move();
        }

        /// <summary>
        /// Draw floor
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            if (Visible) {
                switch (FloorType) {
                    case FloorType.Regular:
                    case FloorType.Moving:
                        spriteBatch.Draw(Texture, Rectangle, new Color(0, 44, 158));
                        break;
                    case FloorType.Finish:
                        spriteBatch.Draw(Texture, Rectangle, Color.LightGreen);
                        break;
                    case FloorType.Jump:
                        spriteBatch.Draw(Texture, Rectangle, Color.LightSkyBlue);
                        break;
                    case FloorType.Deadly:
                        spriteBatch.Draw(Texture, Rectangle, new Color(124, 0, 155));
                        break;
                }
            }
        }
    }
}
