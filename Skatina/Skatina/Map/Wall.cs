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

        public Wall(Vector2 position) : base(position)
        {
            Gravity = false;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("images/map/wall");
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public override void Update(GameTime gametime, Map map)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
