using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace BackendDevelopment.BackEnd
{
    class DrawNumber
    {
        public DrawNumber(string numberPassed, Rectangle rectanglePassed, Color colorPassed)
        {
            number = numberPassed;
            rectangle = rectanglePassed;
            color = colorPassed;
        }

        public Texture2D drawNumber(GraphicsDeviceManager graphics, ContentManager Content)
        {
            Texture2D texture;
            string numberToGrab = System.String.Format("Numbers\\number{0}", number);
            texture = Content.Load<Texture2D>(numberToGrab);

            // Assume you have a Texture2D called texture

            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData(data);

            // You now have a packed array of Colors. 
            // So, change the 3rd pixel from the right which is the 4th pixel from the top do:

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == Color.White)
                {
                    data[i] = Color.Transparent;
                }
                else
                {
                    data[i] = color;
                }
            }

            // Once you have finished changing data, set it back to the texture:

            texture.SetData(data);
            //SpriteBatch spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            //spriteBatch.Begin();
            //spriteBatch.Draw(texture, rectangle, Color.Green);
            return texture;
        }

        public static string number { get; set; }
        public static Rectangle rectangle { get; set; }
        public static Color color { get; set; }
    }
}
