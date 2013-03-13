using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TargetTapping.Back_end;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace TargetTapping.Back_end
{
    class Object
    {
        private bool bMouseDownInside;
        private bool bIsClicked;

        //Constructor for this class
        public Object(string ObjectPassedIn, Color colorPassedIn, Rectangle rectanglePassedIn,
                             string shapeTypePassedIn, bool multiSelectPassedIn, GraphicsDeviceManager graphics, ContentManager content)
        {
            objectName = ObjectPassedIn;
            color = colorPassedIn;
            rectangle = rectanglePassedIn;
            shapeType = shapeTypePassedIn;
            multiSelect = multiSelectPassedIn;
            texture = grabObject(graphics, content);
            bMouseDownInside = false;
            bIsClicked = false;
        }

        //Creates Buttons on the screen.
        private Texture2D grabObject(GraphicsDeviceManager graphics, ContentManager content)
        {
            Texture2D texture;
            texture = DrawShape.drawShape(objectName, rectangle.Width, graphics);
            if (shapeType == "Shape")
            {
                texture = DrawShape.drawShape(objectName, rectangle.Width, graphics);
            }
            else if (shapeType == "Letter")
            {
                DrawLetter draw = new DrawLetter(objectName, rectangle, color);
                texture = draw.drawLetter(graphics, content);
            }
            else if (shapeType == "Number")
            {
                DrawNumber draw = new DrawNumber(objectName, rectangle, color);
                texture = draw.drawNumber(graphics, content);
            }
            else
            {
                Console.WriteLine("There was an error in Object.cs");
            }
            return texture;
        }

        public void Update(MouseState state)
        {
            //Used for perPixelCollison at later date
            //Color[] data = new Color[texture.Width * texture.Height];
            //texture.GetData(data);
            if (Collisions.CollisionWithMouse(rectangle, state))
            {
                if (state.LeftButton == ButtonState.Released && bMouseDownInside)
                {
                    bIsClicked = true;
                }
                if (state.LeftButton == ButtonState.Pressed)
                    bMouseDownInside = true;
                else
                    bMouseDownInside = false;
            }
            else
                bMouseDownInside = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bMouseDownInside)
            {
                spriteBatch.Draw(texture, new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), 
                    null, Color.DarkViolet, 0.0f, new Vector2(rectangle.Width / 2f, rectangle.Height / 2f), 0.8f, SpriteEffects.None, 0);
            }
            else
                spriteBatch.Draw(texture, rectangle, color);
        }

        public bool IsClicked()
        {
            bool temp = bIsClicked;

            bIsClicked = false;

            return temp;
        }
        //Properties
        public string objectName { get; set; }
        public Color color { get; set; }
        public Rectangle rectangle { get; set; }
        public string shapeType { get; set; }
        public bool multiSelect { get; set; }
        public Texture2D texture { get; set; }
    }
}
