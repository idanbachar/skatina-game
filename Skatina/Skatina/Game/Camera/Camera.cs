using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Skatina {
    public class Camera {
        public Matrix Transform { get; set; } //Camera's transform
        public Vector2 Position { get; set; } //Camera's position
        private Vector2 Center; //Camera's center screen
        private Viewport Viewport; //Camera's viewport

        /// <summary>
        /// Receives viewport and creates camrea
        /// </summary>
        /// <param name="viewport"></param>
        public Camera(Viewport viewport) {
            Viewport = viewport;
        }

        /// <summary>
        /// Receives position and x,y offsets and focusing object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        public void Focus(Vector2 position, int xOffset, int yOffset) {
            if (position.X < Viewport.Width / 2)
                Center.X = Viewport.Width / 2;
            else if (position.X > xOffset - (Viewport.Width / 2))
                Center.X = xOffset - (Viewport.Width / 2);
            else
                Center.X = position.X;

            if (position.Y < Viewport.Height / 2)
                Center.Y = Viewport.Height / 2;
            else if (position.Y > yOffset - (Viewport.Height / 2))
                Center.Y = yOffset - (Viewport.Height / 2);
            else
                Center.Y = position.Y;

            Transform = Matrix.CreateTranslation(new Vector3(-Center.X + (Viewport.Width / 2), -Center.Y + (Viewport.Height / 2), 0));
        }
    }
}
