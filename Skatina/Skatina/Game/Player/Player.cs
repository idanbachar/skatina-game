using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Skatina {
    public class Player : Entity {

        public const int Width = 60; //Player's width
        public const int Height = 60; //Player's height
        public bool IsJump; //Player's jump indication
        private int JumpTimer; //Player's jump timer
        private int JumpTimerEndTime; //Player's jump end timer
        private bool IsPressedSpace; //Press space indication
        private bool IsDead; //Player's dead indication
        private float Speed; //Player's speed

        private Texture2D DeadTexture; //Player's dead texture

        public Floor CurrentMovingFloor; //Current moving floor
        public Floor CurrentFinishFloor; //Current finish floor

        /// <summary>
        /// Receives position and creates a player
        /// </summary>
        /// <param name="position"></param>
        public Player(Vector2 position) : base(position) {
            IsJump = false;
            JumpTimer = 0;
            JumpTimerEndTime = 25;
            IsPressedSpace = false;
            IsDead = false;
            CurrentMovingFloor = null;
            CurrentFinishFloor = null;
            Speed = 4.5f;
            FallSpeed = 8f;
        }

        /// <summary>
        /// Load player
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content) {
            Texture = content.Load<Texture2D>("images/player/player");
            DeadTexture = content.Load<Texture2D>("images/player/player_dead");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        /// <summary>
        /// Move player right
        /// </summary>
        public void MoveRight() {
            SetPosition(new Vector2(Position.X + Speed, Position.Y));
        }

        /// <summary>
        /// Move player left
        /// </summary>
        public void MoveLeft() {
            SetPosition(new Vector2(Position.X - Speed, Position.Y));
        }

        /// <summary>
        /// Receives list of entities and checks if player on right wall, return true else false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public override bool IsOnRightSideWall(List<Entity> entities) {
            if (!IsDead) {
                foreach (Entity entity in entities) {
                    if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Wall) {
                        if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Bottom) {
                            if (Rectangle.Right >= entity.Rectangle.Left && Rectangle.Left <= entity.Rectangle.Left) {
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

            return false;
        }

        /// <summary>
        /// Receives list of entities and checks if player on left wall, return true else false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public override bool IsOnLeftSideWall(List<Entity> entities) {
            if (!IsDead) {
                foreach (Entity entity in entities) {
                    if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Wall) {
                        if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Bottom) {
                            if (Rectangle.Left <= entity.Rectangle.Right && Rectangle.Right >= entity.Rectangle.Right) {
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

            return false;
        }

        /// <summary>
        /// Receives list of entities and checks if player on top floor, return true else false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public override bool IsOnTopFloor(List<Entity> entities) {
            if (!IsDead) {
                foreach (Entity entity in entities) {
                    if (Rectangle.Intersects(entity.Rectangle) && entity.Visible && entity is Floor) {
                        if (Rectangle.Bottom >= entity.Rectangle.Top && Rectangle.Top <= entity.Rectangle.Top) {
                            if ((Rectangle.Left >= entity.Rectangle.Left && Rectangle.Left <= entity.Rectangle.Right) ||
                                (Rectangle.Right <= entity.Rectangle.Right && Rectangle.Right >= entity.Rectangle.Left)) {
                                IsOnTopOfEntity = true;
                                CurrentMovingFloor = ((Floor)entity).FloorType == FloorType.Moving ? (Floor)entity : null;
                                CurrentFinishFloor = ((Floor)entity).FloorType == FloorType.Finish ? (Floor)entity : null;

                                if (((Floor)entity).FloorType == FloorType.Jump) {
                                    IsJump = true;
                                    JumpTimerEndTime = 70;
                                }
                                else if (((Floor)entity).FloorType == FloorType.Deadly) {
                                    IsDead = true;
                                }

                                return true;
                            }
                        }
                    }
                }
                CurrentMovingFloor = null;
                CurrentFinishFloor = null;
                IsOnTopOfEntity = false;
                return false;
            }

            return false;
        }

        /// <summary>
        ///  Checks if player is on left map, return true else false
        /// </summary>
        /// <returns></returns>
        public bool IsOnLeftMap() {
            return Rectangle.Left <= 0;
        }

        /// <summary>
        ///  Checks if player is on right map, return true else false
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool IsOnRightMap(Map map) {
            return Rectangle.Right >= map.Levels[map.CurrentLevelIndex].GetWidth();
        }

        /// <summary>
        /// Receives map, and checks if player is below map, return true else false
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private bool IsBelowMap(Map map) {
            return Rectangle.Top >= map.Levels[map.CurrentLevelIndex].GetHeight();
        }

        /// <summary>
        /// Receives list of bullets and checks if player hit by them, return true else false
        /// </summary>
        /// <param name="bullets"></param>
        public void CheckBulletsCollision(List<Bullet> bullets) {
            for (int i = 0; i < bullets.Count; i++) {
                if (bullets[i] is Bullet) {
                    if (Rectangle.Intersects(bullets[i].Rectangle)) {
                        bullets.RemoveAt(i);
                        IsDead = true;
                    }
                }
            }
        }

        /// <summary>
        /// Start player's jump
        /// </summary>
        public void Jump() {
            if (JumpTimer < JumpTimerEndTime) {
                Gravity = false;
                JumpTimer++;
                SetPosition(new Vector2(Position.X, Position.Y - Speed * 2));
            }
            else {
                Gravity = true;
                JumpTimer = 0;
                IsJump = false;
            }
        }

        /// <summary>
        /// Checks pressing in keyboard
        /// </summary>
        /// <param name="map"></param>
        private void CheckKeyBoard(Map map) {

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)) {
                if (!IsOnRightSideWall(map.Levels[map.CurrentLevelIndex].LevelEntities) && !IsOnRightMap(map))
                    MoveRight();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) {
                if (!IsOnLeftSideWall(map.Levels[map.CurrentLevelIndex].LevelEntities) && !IsOnLeftMap())
                    MoveLeft();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !IsPressedSpace) {
                IsPressedSpace = true;
                if (!IsJump && IsOnTopFloor(map.Levels[map.CurrentLevelIndex].LevelEntities)) {
                    IsJump = true;
                    JumpTimerEndTime = 25;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space)) {
                IsPressedSpace = false;
            }
        }

        /// <summary>
        /// Receives map and respawn player
        /// </summary>
        /// <param name="map"></param>
        public void Respawn(Map map) {
            map.Levels[map.CurrentLevelIndex].AddFail();
            FallSpeed = 10f;
            IsDead = false;
            IsColide = true;
            SetPosition(map.Levels[map.CurrentLevelIndex].PlayerRespawnPosition);
        }

        /// <summary>
        /// Receives map and respawn on new level
        /// </summary>
        /// <param name="map"></param>
        public void RespawnNewLevel(Map map) {
            FallSpeed = 10f;
            IsDead = false;
            IsColide = true;
            SetPosition(map.Levels[map.CurrentLevelIndex].PlayerRespawnPosition);
        }

        /// <summary>
        /// Updates player
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="map"></param>
        public override void Update(GameTime gametime, Map map) {
            if (!IsDead) {
                CheckKeyBoard(map);

                foreach (Entity entity in map.Levels[map.CurrentLevelIndex].LevelEntities) {
                    if (entity is Wall) {
                        Wall wall = entity as Wall;
                        CheckBulletsCollision(wall.Bullets);
                    }
                }
            }
            else {
                IsColide = false;
                FallSpeed = 10f;
            }

            if (IsJump) {
                Jump();
            }

            if (IsBelowMap(map)) {
                Respawn(map);
            }

            if (CurrentMovingFloor != null) {
                if (CurrentMovingFloor.MoveDirection == Direction.Right)
                    SetPosition(new Vector2(Position.X + 1, Position.Y));
                else if (CurrentMovingFloor.MoveDirection == Direction.Left)
                    SetPosition(new Vector2(Position.X - 1, Position.Y));
            }

            base.Update(gametime, map);

        }

        /// <summary>
        /// Draws player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            if (Visible)
                spriteBatch.Draw(!IsDead ? Texture : DeadTexture, Rectangle, Color.White);
        }
    }
}
