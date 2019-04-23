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

        private int MoveTimer;
        private int MaxMoveTimer;

        private int StartShootTimer;

        private Direction MoveDirection;
        private Direction ShootDirection;
        public WallType WallType;
        public List<Bullet> Bullets;

        public Wall(Vector2 position, WallType wallType, Direction shootDirection) : base(position)
        {
            Gravity = false;
            MoveTimer = 0;
            MaxMoveTimer = 50;
            MoveDirection = Direction.Up;
            ShootDirection = shootDirection;
            WallType = wallType;
            Bullets = new List<Bullet>();
            StartShootTimer = 0;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/map/wall");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public override void Update(GameTime gametime, Map map)
        {
            base.Update(gametime, map);

            if (WallType == WallType.Moving || WallType == WallType.DeadlyMoving)
            {
                Move();
            }

            if(WallType == WallType.DeadlyMoving)
            {
                if (StartShootTimer < 50)
                    StartShootTimer++;
                else
                {
                    Shoot();
                    StartShootTimer = 0;
                }
            }

            for (int i = 0; i < Bullets.Count; i++)
            {
                if (!Bullets[i].IsOnLeftSideWall(map.Levels[map.CurrentLevelIndex].LevelEntities))
                    Bullets[i].Update(gametime, map);
                else
                {
                    Bullets.RemoveAt(i);
                }
            }
        }

        public void Shoot()
        {
            int xPos = ShootDirection == Direction.Left ? Rectangle.Left - Bullet.Width : Rectangle.Right + Bullet.Width;
            int yPos = Rectangle.Top + Rectangle.Height / 2;

            Bullet bullet = new Bullet(new Vector2(xPos, yPos), ShootDirection);
            bullet.LoadContent(Skatina.GameContent);
            Bullets.Add(bullet);
        }

        public void Move()
        {
            if (MoveTimer < MaxMoveTimer)
            {
                MoveTimer++;

                if (MoveDirection == Direction.Up)
                {
                    SetPosition(new Vector2(Position.X, Position.Y - 3));
                }

                if (MoveDirection == Direction.Down)
                {
                    SetPosition(new Vector2(Position.X, Position.Y + 3));
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
            if (MoveDirection == Direction.Up)
            {
                MoveDirection = Direction.Down;
            }
            else if (MoveDirection == Direction.Down)
            {
                MoveDirection = Direction.Up;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (WallType == WallType.Moving || WallType == WallType.Regular)
                    spriteBatch.Draw(Texture, Rectangle, Color.White);
                else if (WallType == WallType.Deadly || WallType == WallType.DeadlyMoving)
                    spriteBatch.Draw(Texture, Rectangle, Color.Red);


                foreach (Bullet bullet in Bullets)
                    bullet.Draw(spriteBatch);
            }
        }
    }
}
