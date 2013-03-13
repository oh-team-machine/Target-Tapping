using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BackendDevelopment.BackEnd;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace BackendDevelopment.BackEnd
{
    class Object
    {   
        //Constructor for this class
        public Object(string ObjectPassedIn, Color colorPassedIn, Rectangle rectanglePassedIn,
                             string shapeTypePassedIn, bool multiSelectPassedIn)
        {
            Object = ObjectPassedIn;
            color = colorPassedIn;
            rectangle = rectanglePassedIn;
            shapeType = shapeTypePassedIn;
            multiSelect = multiSelectPassedIn;
        }

        //Draws the shape on the screen.
        public void drawShape(GraphicsDeviceManager graphics, ContentManager content)
        {
            Texture2D texture;
            if (shapeType == "Shape")
            {
                texture = DrawShape.drawShape(Object, rectangle.Width, graphics);
            }
            else if (shapeType == "Letter")
            {
                DrawLetter draw = new DrawLetter(Object, rectangle, color);
                texture = draw.drawLetter(graphics, content);
            }
            else if (shapeType == "Number")
            {
                DrawNumber draw = new DrawNumber(Object, rectangle, color);
                texture = draw.drawNumber(graphics, content);
            }
            else
            {
                Console.WriteLine("There was an error in Object.cs");
            }
        }
        //Properties
        public string Object { get; set; }
        public Color color { get; set; }
        public Rectangle rectangle { get; set; }
        public string shapeType { get; set; }
        public bool multiSelect { get; set; }
    }
}
