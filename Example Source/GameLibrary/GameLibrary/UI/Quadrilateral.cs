using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class Quadrilateral
    {
        private Line top;
        private Line right;
        private Line bottom;
        private Line left;

        private GraphicsDevice device;

        private Vector2 upperLeft;
        private Vector2 upperRight;
        private Vector2 lowerLeft;
        private Vector2 lowerRight;

        /// <summary>
        /// 
        /// </summary>
        public Vector2 UpperLeft
        { 
            get { return upperLeft; } 
            set 
            {
                upperLeft = value;
                top = new Line(UpperLeft, UpperRight, LineWidth, Color, device);
                left = new Line(UpperLeft, LowerLeft, LineWidth, Color, device); 
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Vector2 UpperRight
        {
            get { return upperRight; }
            set
            {
                upperRight = value;
                top = new Line(UpperLeft, UpperRight, LineWidth, Color, device);
                right = new Line(UpperRight, LowerRight, LineWidth, Color, device);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Vector2 LowerLeft 
        { 
            get { return lowerLeft; }
            set
            {
                lowerLeft = value;
                bottom = new Line(LowerLeft, LowerRight, LineWidth, Color, device);
                left = new Line(UpperLeft, LowerLeft, LineWidth, Color, device);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Vector2 LowerRight
        {
            get { return lowerRight; }
            set 
            {
                lowerRight = value;
                right = new Line(UpperRight, LowerRight, LineWidth, Color, device); 
                bottom = new Line(LowerLeft, LowerRight, LineWidth, Color, device); 
            } 
        }

        private float lineWidth;

        /// <summary>
        /// 
        /// </summary>
        public float LineWidth
        {
            get { return lineWidth; }
            set
            {
                lineWidth = value;
                top = new Line(UpperLeft, UpperRight, LineWidth, Color, device);
                right = new Line(UpperRight, LowerRight, LineWidth, Color, device);
                bottom = new Line(LowerLeft, LowerRight, LineWidth, Color, device);
                left = new Line(UpperLeft, LowerLeft, LineWidth, Color, device);
            }
        }

        private Color color;

        /// <summary>
        /// 
        /// </summary>
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                top = new Line(UpperLeft, UpperRight, LineWidth, Color, device);
                right = new Line(UpperRight, LowerRight, LineWidth, Color, device);
                bottom = new Line(LowerLeft, LowerRight, LineWidth, Color, device);
                left = new Line(UpperLeft, LowerLeft, LineWidth, Color, device);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upperLeft"></param>
        /// <param name="upperRight"></param>
        /// <param name="lowerLeft"></param>
        /// <param name="lowerRight"></param>
        /// <param name="lineWidth"></param>
        /// <param name="color"></param>
        /// <param name="device"></param>
        public Quadrilateral(Vector2 upperLeft, Vector2 upperRight, Vector2 lowerLeft, Vector2 lowerRight, float lineWidth, Color color, GraphicsDevice device)
        {
            this.upperLeft = upperLeft;
            this.upperRight = upperRight;
            this.lowerLeft = lowerLeft;
            this.lowerRight = lowerRight;

            this.lineWidth = lineWidth;
            this.color = color;

            this.device = device;

            top = new Line(UpperLeft, UpperRight, LineWidth, Color, device);
            right = new Line(UpperRight, LowerRight, LineWidth, Color, device);
            bottom = new Line(LowerLeft, LowerRight, LineWidth, Color, device);
            left = new Line(UpperLeft, LowerLeft, LineWidth, Color, device);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            top.Draw(spriteBatch);
            right.Draw(spriteBatch);
            bottom.Draw(spriteBatch);
            left.Draw(spriteBatch);
        }
    }
}
