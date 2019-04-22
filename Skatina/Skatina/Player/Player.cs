using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Skatina
{
    public class Player : Entity
    {
        public const int Width = 38;
        public const int Height = 38;
        public bool IsJump;
        private int JumpTimer;
        private bool IsPressedSpace;
        private bool IsDead;
        private float Speed;

        private Floor CurrentMovingFloor;

        public Player(Vector2 position): base(position)
        {
            IsJump = false;
            JumpTimer = 0;
            IsPressedSpace = false;
            IsDead = false;
            CurrentMovingFloor = null;
            Speed = 2.5f;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/player/player");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public void MoveRight()
        {
            SetPosition(new Vector2(Position.X + Speed, Position.Y));
        }

        public void MoveLeft()
        {
            SetPosition(new Vector2(Position.X - Speed, Position.Y));
        }

        public override bool IsOnRightSideWall(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Wall)
                {
                    if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Bottom)
                    {
                        if (Rectangle.Right >= entity.Rectangle.Left && Rectangle.Left <= entity.Rectangle.Left)
                        {
                            IsOnRightOfEntity = true;

                            if (((Wall)entity).WallType == WallType.Deadly || ((Wall)entity).WallType == WallType.DeadlyMoving)
                                IsDead = true;

                            return true;
                        }
                    }
                }
            }
            IsOnRightOfEntity = false;
            return false;
        }

        public override bool IsOnLeftSideWall(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Wall)
                {
                    if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Bottom)
                    {
                        if (Rectangle.Left <= entity.Rectangle.Right && Rectangle.Right >= entity.Rectangle.Right)
                        {
                            IsOnLeftOfEntity = true;

                            if (((Wall)entity).WallType == WallType.Deadly || ((Wall)entity).WallType == WallType.DeadlyMoving)
                                IsDead = true;

                            return true;
                        }
                    }
                }
            }
            IsOnLeftOfEntity = false;
            return false;
        }

        public override bool IsOnTopFloor(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Floor)
                {
                    if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Top)
                    {
                        if ((Rectangle.Left >= entity.Rectangle.Left && Rectangle.Left <= entity.Rectangle.Right) ||
                            (Rectangle.Right <= entity.Rectangle.Right && Rectangle.Right >= entity.Rectangle.Left))
                        {
                            IsOnTopOfEntity = true;
                            CurrentMovingFloor = ((Floor)entity).FloorType == FloorType.Moving ? (Floor)entity : null;
                            return true;
                        }
                    }
                }
            }
            CurrentMovingFloor = null;
            IsOnTopOfEntity = false;
            return false;
        }

        public void CheckBulletsCollision(List<Bullet> bullets)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                if(bullets[i] is Bullet)
                {
                    if (Rectangle.Intersects(bullets[i].Rectangle))
                    {
                        bullets.RemoveAt(i);
                        IsDead = true;
                    }
                }
            }
        }

        public void Jump()
        {
            if (JumpTimer < 20)
            {
                Gravity = false;
                JumpTimer++;
                SetPosition(new Vector2(Position.X, Position.Y - 7));
            }
            else
            {
                Gravity = true;
                JumpTimer = 0;
                IsJump = false;
            }
        }

        private void CheckKeyBoard(Map map)
        {
 
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (!IsOnRightSideWall(map.Levels[map.CurrentLevelIndex].LevelEntities))
                    MoveRight();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (!IsOnLeftSideWall(map.Levels[map.CurrentLevelIndex].LevelEntities))
                    MoveLeft();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !IsPressedSpace)
            {
                IsPressedSpace = true;
                if (!IsJump && IsOnTopFloor(map.Levels[map.CurrentLevelIndex].LevelEntities))
                {
                    IsJump = true;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                IsPressedSpace = false;
            }
        }

        private bool IsBelowMap(Map map)
        {
            return Rectangle.Top >= map.Levels[map.CurrentLevelIndex].GetHeight();
        }

        private void Respawn()
        {
            IsDead = false;
            IsColide = true;
            SetPosition(new Vector2(0, 0));
        }

        public override void Update(GameTime gametime, Map map)
        {
            if (!IsDead)
            {
                CheckKeyBoard(map);

                foreach (Entity entity in map.Levels[map.CurrentLevelIndex].LevelEntities)
                {
                    if (entity is Wall)
                    {
                        Wall wall = entity as Wall;
                        CheckBulletsCollision(wall.Bullets);
                    }
                }
            }
            else
            {
                IsColide = false;
            }

            if (IsJump)
            {
                Jump();
            }

            if (IsBelowMap(map))
            {
                Respawn();
            }

            if(CurrentMovingFloor != null)
            {
                if (CurrentMovingFloor.MoveDirection == Direction.Right)
                    SetPosition(new Vector2(Position.X + 1, Position.Y));
                else if (CurrentMovingFloor.MoveDirection == Direction.Left)
                    SetPosition(new Vector2(Position.X - 1, Position.Y));
            }
 

            base.Update(gametime, map);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
