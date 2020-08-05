using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skatina {
    public class Wall : Entity {

        public const int Width = 35; //Wall's width
        public const int Height = 150; //Wall's height

        public WallType WallType; //Wall's type

        private Vector2 MaxUpPosition; //Wall's max up position he can move
        private Vector2 MinDownPosition; //Wall's min down position he can move

        private int StartShootTimer; //Wall's start shooting timer

        private float Speed; //Wall's speed

        private Direction MoveDirection; //Wall's move direction
        private Direction ShootDirection; //Wall's shoot direction

        public List<Bullet> Bullets; //Wall's bullets list

        /// <summary>
        /// Receives position, wall type, shoot direction and creates wall
        /// </summary>
        /// <param name="position"></param>
        /// <param name="wallType"></param>
        /// <param name="shootDirection"></param>
        public Wall(Vector2 position, WallType wallType, Direction shootDirection) : base(position) {
            Gravity = false;
            MoveDirection = Direction.Up;
            ShootDirection = shootDirection;
            WallType = wallType;
            Bullets = new List<Bullet>();
            StartShootTimer = 0;
            Speed = 3f;
        }

        /// <summary>
        /// Load wall
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content) {
            Texture = content.Load<Texture2D>("images/map/wall");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            MaxUpPosition = new Vector2(Position.X, Position.Y - Height / 2 * 4);
            MinDownPosition = new Vector2(Position.X, Position.Y + Height / 2 * 4);
        }

        /// <summary>
        /// Update wall
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="map"></param>
        public override void Update(GameTime gametime, Map map) {
            base.Update(gametime, map);

            if (WallType == WallType.Moving || WallType == WallType.DeadlyMoving)
                Move();

            if (WallType == WallType.DeadlyMoving) {
                if (StartShootTimer < 70)
                    StartShootTimer++;
                else {
                    Shoot();
                    StartShootTimer = 0;
                }
            }

            for (int i = 0; i < Bullets.Count; i++) {
                if (!Bullets[i].IsOnLeftSideWall(map.Levels[map.CurrentLevelIndex].LevelEntities))
                    Bullets[i].Update(gametime, map);
                else {
                    Bullets.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Shoot bullets from the wall
        /// </summary>
        public void Shoot() {
            int xPos = ShootDirection == Direction.Left ? Rectangle.Left - Bullet.Width : Rectangle.Right + Bullet.Width;
            int yPos = Rectangle.Top + Rectangle.Height / 2;

            Bullet bullet = new Bullet(new Vector2(xPos, yPos), ShootDirection);
            bullet.LoadContent(Skatina.GameContent);
            Bullets.Add(bullet);
        }

        /// <summary>
        /// Wall moves by direction
        /// </summary>
        public void Move() {
            switch (MoveDirection) {
                case Direction.Up:
                    if (Position.Y >= MaxUpPosition.Y)
                        SetPosition(new Vector2(Position.X, Position.Y - Speed));
                    else {
                        SwapDirection();
                    }
                    break;
                case Direction.Down:
                    if (Position.Y <= MinDownPosition.Y)
                        SetPosition(new Vector2(Position.X, Position.Y + Speed));
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
            if (MoveDirection == Direction.Up) {
                MoveDirection = Direction.Down;
            }
            else if (MoveDirection == Direction.Down) {
                MoveDirection = Direction.Up;
            }
        }

        /// <summary>
        /// Draw wall
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            if (Visible) {
                if (WallType == WallType.Moving || WallType == WallType.Regular)
                    spriteBatch.Draw(Texture, Rectangle, new Color(0, 44, 158));
                else if (WallType == WallType.Deadly || WallType == WallType.DeadlyMoving)
                    spriteBatch.Draw(Texture, Rectangle, new Color(124, 0, 155));


                foreach (Bullet bullet in Bullets)
                    bullet.Draw(spriteBatch);
            }
        }
    }
}
