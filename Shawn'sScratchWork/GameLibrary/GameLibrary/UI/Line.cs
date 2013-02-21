using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class Line
    {
        private Texture2D line;
        private Vector2 location;
        private float width;
        private float length;
        private float angle;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="width"></param>
        /// <param name="color"></param>
        /// <param name="device"></param>
        public Line(Vector2 start, Vector2 end, float width, Color color, GraphicsDevice device)
        {
            line = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            line.SetData(new[] { color });

            location = start;

            this.width = width;

            angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            length = Vector2.Distance(start, end);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(line, location, null, Color.White, angle, Vector2.Zero, new Vector2(length, width), SpriteEffects.None, 0);
        }
    }
}
