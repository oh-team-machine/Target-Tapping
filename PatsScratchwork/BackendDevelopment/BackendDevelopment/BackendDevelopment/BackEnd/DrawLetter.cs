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
    class DrawLetter
    {
        public DrawLetter(string letterPassed, Rectangle rectanglePassed, Color colorPassed)
        {
            letter = letterPassed;
            rectangle = rectanglePassed;
            color = colorPassed;
        }

        public Texture2D drawLetter(GraphicsDeviceManager graphics, ContentManager Content)
        {
            Texture2D texture;
            string letterToGrab = System.String.Format("Letters\\letter{0}", letter);
            texture = Content.Load<Texture2D>(letterToGrab);

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

        public static string letter { get; set; }
        public static Rectangle rectangle { get; set; }
        public static Color color { get; set; }
    }
}
