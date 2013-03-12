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
    class CreateLetter
    {
        public CreateLetter(string letterPassed, string sizePassed, Rectangle rectanglePassed, Color colorPassed)
        {
            letter = letterPassed;
            size = sizePassed;
            rectangle = rectanglePassed;
            color = colorPassed;
        }

        public Texture2D drawLetter(GraphicsDeviceManager graphics, ContentManager Content)
        {
            //SpriteBatch spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            //SpriteFont spriteFont = Content.Load<SpriteFont>("Medium");
            //if (size == "Tiny")
            //{
            //    spriteFont = Content.Load<SpriteFont>("Tiny");
            //}
            //else if (size == "Small")
            //{
            //    spriteFont = Content.Load<SpriteFont>("Small");
            //}
            //else if (size == "Medium")
            //{
            //    spriteFont = Content.Load<SpriteFont>("Medium");
            //}
            //else if (size == "Large")
            //{
            //    spriteFont = Content.Load<SpriteFont>("Large");
            //}
            //else if (size == "Huge")
            //{
            //    spriteFont = Content.Load<SpriteFont>("Huge");
            //}
            //else
            //{
            //    Console.WriteLine("Have passed in the wrong type of size!");
            //}

            //Vector2 pos = new Vector2(position[0], position[1]);
            //Vector2 middle = spriteFont.MeasureString(letter) / 2;
            //spriteBatch.Begin();
            //spriteBatch.DrawString(spriteFont, letter, pos, color,
            //        0, middle, 1.0f, SpriteEffects.None, 0.5f);

            Texture2D texture = Content.Load<Texture2D>("letterA");

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

        public string letter { get; set; }
        public string size { get; set; }
        public Rectangle rectangle { get; set; }
        public Color color { get; set; }
    }
}
