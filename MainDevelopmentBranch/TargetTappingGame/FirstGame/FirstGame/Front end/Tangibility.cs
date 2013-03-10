using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Front_end
{
    /// <summary>
    /// Tangibility provides a texture, a dimension, and a colour. 
    /// </summary>
    public class Tangibility
    {
        public Color Color { get; private set; }
        public Rectangle Dimensions { get; private set; }
        public Texture2D Texture { get; private set; }

        public Point Position
        {
            get { return Dimensions.Location; }
            set
            {
                Dimensions = new Rectangle(value.X, value.Y,
                    Dimensions.Width, Dimensions.Height);
            }
        }


	public Tangibility(int x, int y, Texture2D tex)
	{
           Color = new Color(255, 255, 255, 255);
           Texture = tex;
           Dimensions = new Rectangle(x, y, tex.Width, tex.Height);
	}

        public bool Intersects(Point p)
        {
            return Dimensions.Contains(p);
        }

        public bool Intersects(Rectangle r)
        {
            return Dimensions.Intersects(r);
        }
	
    }
}
