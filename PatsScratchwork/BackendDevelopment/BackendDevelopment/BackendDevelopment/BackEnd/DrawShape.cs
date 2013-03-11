using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace BackendDevelopment.BackEnd
{
    class DrawShape
    {
        //Put private class variables here.
        //int = myInt;

        //Constructor for this class
        public Texture2D drawShape(string shape, int size, GraphicsDeviceManager graphics)
        {
            Texture2D texture;
            if (shape == "Circle")
            {
                texture = drawCircle(size, graphics);
            } else
            {
            texture = drawSquare(size, graphics);
            }

            return texture;
        }

        private static Texture2D drawCircle(int radius, GraphicsDeviceManager graphics)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds

            Texture2D texture = new Texture2D(graphics.GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            data = fillCircle(data, outerRadius);
            
            texture.SetData(data);
            return texture;
        }

        private static Texture2D drawSquare(int width, GraphicsDeviceManager graphics)
        {
            int outerWidth = width * 2 + 2; // So circle doesn't go out of bounds

            Texture2D texture = new Texture2D(graphics.GraphicsDevice, outerWidth, outerWidth);

            Color[] data = new Color[outerWidth * outerWidth];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.White;
            texture.SetData(data);
            return texture;
        }

        private static Color[] fillCircle(Color[] data, int outerRadius)
        {
            bool finished = false;
            int firstSkip = 0;
            int lastSkip = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (finished == false)
                {
                    if((data[i] == Color.White) && (firstSkip == 0)) 
                    {
                        while (data[i + 1] == Color.White)
                        {
                            i++;
                        }
                        firstSkip = 1;
                        i++;
                    }
                    if (firstSkip == 1) {
                        if (data[i] == Color.White && data[i + 1] != Color.White)
                        {
                            i++;
                            while (data[i] != Color.White)
                            {
                                for (int j = 1; j <= outerRadius; j++)
                                {
                                    if (data[i + j] != Color.White)
                                    {
                                        lastSkip++;
                                    }
                                }
                                if (lastSkip == outerRadius)
                                {
                                    finished = true;
                                    break;
                                }
                                else
                                {
                                    data[i] = Color.White;
                                    i++;
                                    lastSkip = 0;
                                }
                            }
                            while (data[i] == Color.White)
                            {
                                i++;
                            }
                            i--;
                        }
                    }
                }
            }
            return data;
        }
    }
}
