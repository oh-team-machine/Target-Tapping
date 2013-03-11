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
        public CreateLetter(string letterPassed, string sizePassed, int[] positionPassed, Color colorPassed)
        {
            letter = letterPassed;
            size = sizePassed;
            position = positionPassed;
            color = colorPassed;
        }

        public void drawLetter(GraphicsDeviceManager graphics, ContentManager Content)
        {
            SpriteBatch spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            SpriteFont spriteFont = Content.Load<SpriteFont>("Medium");
            if (size == "Tiny")
            {
                spriteFont = Content.Load<SpriteFont>("Tiny");
            }
            else if (size == "Small")
            {
                spriteFont = Content.Load<SpriteFont>("Small");
            }
            else if (size == "Medium")
            {
                spriteFont = Content.Load<SpriteFont>("Medium");
            }
            else if (size == "Large")
            {
                spriteFont = Content.Load<SpriteFont>("Large");
            }
            else if (size == "Huge")
            {
                spriteFont = Content.Load<SpriteFont>("Huge");
            }
            else
            {
                Console.WriteLine("Have passed in the wrong type of size!");
            }

            Vector2 pos = new Vector2(position[0], position[1]);
            Vector2 middle = spriteFont.MeasureString(letter) / 2;
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, letter, pos, color,
                    0, middle, 1.0f, SpriteEffects.None, 0.5f);
        }

        public string letter { get; set; }
        public string size { get; set; }
        public int[] position { get; set; }
        public Color color { get; set; }
    }
}
