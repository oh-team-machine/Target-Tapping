using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TargetTapping.Back_end;
using System;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // State for when the user selects the position on the screen.
    class PositionPaletteState : PaletteState
    {

        public PositionPaletteState(Palette p) : base(p) { }

        internal override Point Position { get { return new Point(0,0);}  set { } }
        public override void LoadContent(RichContentManager content) { }

        public override void Update(MouseState mouse)
        {
            //implement testing to see if our new position is inside the grid and not
            //ontop of any current objects.

            //get the current mouse coordinates
            int x = mouse.X, y = mouse.Y;

            //rectangle representing the size of the object. Need to combine this with the mouses x and y coords
            Rectangle objectRect = Parent.ObjectFactory.Size;

            //create a new point with the x and y coordinates to be adjusted to center the shape
            //This simulates what the actual rectangle would be if we were about to place it normally.
            Point adjustedCoord;
            adjustedCoord.X = x - (objectRect.Width / 2);
            adjustedCoord.Y = y - (objectRect.Height / 2);
            //combine the point with objectRect
            objectRect.Location = adjustedCoord;
                        
            //myLevel contains the list of current objects on the screen to test for intersects
            Level myLevel = GameManager.GlobalInstance.activeLevel;
            //gridRect is the rectangle of the grid in levelEditor where new objects need to be placed.
            Rectangle gridRect = GameManager.GlobalInstance.gridRectangle;
            //rectangle representing the size of the object. Need to combine this with the mouses x and y coords
            
            // Assume the click is the desired position.
            if (mouse.LeftButton == ButtonState.Pressed)
            {

                //now lets test to see if the click is in a valid position
                //Test if the rectangle is contained in the gridRect
                //if it is set it to the new position else dont allow it to be drawn, notify the user somehow that they need to
                //click inside the grid.
                if (gridRect.Contains(objectRect))
                {

                    Console.WriteLine("Good: New position was inside the grid");

                    //if there is no object in the entire list then we can assume the grid is empty and we can place 
                    //the new object anywhere.
                    if (myLevel.objectList.Count == 0)
                    {
                        Console.WriteLine("Good: Only one object in the list.");
                        Console.WriteLine("Good: Proceed with creating a new object.");
                        //original code
                        Parent.ObjectFactory.Coordinates = new Point(x, y);
                        Parent.RequestStateChange("INITIAL");
                        //end original code
                        

                    }
                    else
                    {
                        Console.WriteLine("Good: Multile objects in the list.");

                        //bool to keep track if there was an intersect
                        bool isThereAnIntersect = false;
                        foreach (var myListofObjects in myLevel.objectList)
                        {
                            //check if the object being moved doesnt intersect with any other objects
                            foreach (var myObject in myListofObjects)
                            {
                                //if were comparing the object being redrawn to its self in the list just ignore it 
                                //and skip this iteration of the loop.
                                if (objectRect.Equals(myObject))
                                {
                                    Console.WriteLine("It's equal to itself, skipping this iteration.");
                                    continue;
                                }
                                //check a intersect with a specific object
                                if (objectRect.Intersects(myObject.rectangle))
                                {
                                    Console.WriteLine("collided with another");
                                    isThereAnIntersect = true;
                                }

                            }
                        }

                        //now see if there was an intersect tell the user to pick a new spot else just draw it to 
                        //the new location.
                        if (isThereAnIntersect)
                        {
                            Console.WriteLine("Bad: there was an intersect with another object in the list, pick a new spot");
                        }
                        else
                        {
                            Console.WriteLine("Good: multiple objects in list but no intersects.");

                            //original code
                            Parent.ObjectFactory.Coordinates = new Point(x, y);
                            Parent.RequestStateChange("INITIAL");
                            //end original code
                          
                            Console.WriteLine("Object now has new coordinates, set it to null so we no longer move it.");

                        }


                    }

                }
                else
                {

                    Console.WriteLine("Bad: New position was outside the grid");

                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // Don't need to draw anything!
            Parent.Hide();
        }

    }
}
