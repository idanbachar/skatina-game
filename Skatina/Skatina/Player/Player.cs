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

        public Player(Vector2 position): base(position)
        {
            IsJump = false;
            JumpTimer = 0;
            IsPressedSpace = false;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/player/player");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public void MoveRight()
        {
            SetPosition(new Vector2(Position.X + 3, Position.Y));
        }

        public void MoveLeft()
        {
            SetPosition(new Vector2(Position.X - 3, Position.Y));
        }

        public void Jump()
        {
            if (JumpTimer < 45)
            {
                Gravity = false;
                JumpTimer++;
                SetPosition(new Vector2(Position.X, Position.Y - 2.5f));
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
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                MoveRight();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                MoveLeft();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !IsPressedSpace)
            {
                IsPressedSpace = true;
                if (!IsJump && IsCollisionEntitiy(map.Levels[map.CurrentLevelIndex].LevelEntities))
                {
                    IsJump = true;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                IsPressedSpace = false;
            }
        }

        public override void Update(GameTime gametime, Map map)
        {
            CheckKeyBoard(map);

            if (IsJump)
            {
                Jump();
            }

            base.Update(gametime, map);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
