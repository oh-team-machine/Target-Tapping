using System;
using System.Threading;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace TargetTapping.Back_end
{
    public class Object
    {
        public bool bMouseDownInside;
        private bool bIsClicked;

        //Constructor for this class
        public Object(string objectType, string objectSubname, Rectangle rectanglePassedIn, Color colorPassedIn, ContentManager content, GraphicsDeviceManager graphics)
        {
            objectName = objectSubname;
            color = colorPassedIn;
            rectangle = rectanglePassedIn;
            shapeType = objectType;
            texture = grabObject(graphics, content);
            bMouseDownInside = false;
            bIsClicked = false;
            holdCount = 0;
            this.shouldIbeDrawn = true;

            this.frameDelay = 80;

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
            if (this.frameDelay == 0)
            {
                //Used for perPixelCollison at later date
                //Color[] data = new Color[texture.Width * texture.Height];
                //texture.GetData(data);
                if (Collisions.CollisionWithMouse(rectangle, state))
                {
                    if (state.LeftButton == ButtonState.Released && bMouseDownInside && this.shouldIbeDrawn)
                    {
                        bIsClicked = true;
                        //since this object was just clicked try and delay it from being updated again for a while to avoid couble clicking
                        //due to the rally fast polling rate of the game loop, which registers a mouse click 5-7 times.
                        this.frameDelay = 80;
                    }
                    if (state.LeftButton == ButtonState.Pressed && this.shouldIbeDrawn)
                    {
                        bMouseDownInside = true;
                    }
                    else
                    {
                        bMouseDownInside = false;
                    }

                }
                else
                {
                    bMouseDownInside = false;
                }
            }
            else
            {
                this.frameDelay--;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            

            //Only if the property shouldIbeDrawn is set to true should this object be drawn on screen\
            if(this.shouldIbeDrawn){

                if (bMouseDownInside)
                {
                    spriteBatch.Draw(texture, new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2), 
                        null, Color.DarkViolet, 0.0f, new Vector2(rectangle.Width / 2f, rectangle.Height / 2f), 0.8f, SpriteEffects.None, 0);
                }
                else{

                    spriteBatch.Draw(texture, rectangle, color);
                   
                }
            }
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
        private Texture2D texture { get; set; }
        public bool shouldIbeDrawn { get; set; }
        public int frameDelay { get; set; }
        public int holdCount { get; set; }

    }
}
