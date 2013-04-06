using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using TargetTapping.Back_end;
using Microsoft.Xna.Framework.Input;
using TargetTapping.FrontEnd.LevelEditor;

namespace TargetTapping.Screens
{
    class LevelEditScreen : AbstractRichScreen
    {
        //These booleans are used to specify states and what GUI elements should be presented
        bool multiState = false; //for multi button 
        // wat.
        bool levelEditorMenuON = false; //Menu opens with menu button in level editor

        int intUpTime = 5;
        int intHoldTime = 1;
        Vector2 intUpTimePosition;
        Vector2 intHoldTimePosition;
        SpriteFont font;

        //to handle the multiple number of clicks that occur
        MouseState mouseStateCurrent, mouseStatePrevious;
        
        // The level editor's got a bunch of buttons!
        Dictionary<String, Button> btns = new Dictionary<string,Button>();

        // Declare a new local reference to level object for the level editor screen to load in loadContent
        Level myLevel = GameManager.GlobalInstance.activeLevel;

        // Declare a new object that will be used to reference an object that we are moving
        // around on the levelEditor screen.
        private TargetTapping.Back_end.Object objBeingMoved = null;

        private Palette palette;

        // Title stuff, apparently.
        Texture2D grid, addLabel;
        Vector2 gridPosition = (new Vector2(0, 110));
        Vector2 addLabelPosition = (new Vector2(985, 32));

        //rectangle that represents the boundaries of the placement grid
        Rectangle gridRect;

        public override void LoadContent()
        {
            base.LoadContent();
            intUpTimePosition = (new Vector2(520, 50));
            intHoldTimePosition = (new Vector2(785, 50));

            font = Content.Load<SpriteFont>("font");
            // Load all o' dem buttons
            btns.Add("Home",  MakeButton(30,  35, "LevelEditorGUI/homeButton"));
            btns.Add("Menu",  MakeButton(95,  35, "LevelEditorGUI/menuButton"));
            btns.Add("HoldTime",  MakeButton(700,  35, "LevelEditorGUI/holdTimeButton"));
            btns.Add("Multiple",  MakeButton(355,  35, "LevelEditorGUI/multipleToggleOff"));
            btns.Add("Play",  MakeButton(290,  35, "LevelEditorGUI/playButton"));
            btns.Add("Redo",  MakeButton(215,  35, "LevelEditorGUI/redoButton"));
            btns.Add("Undo",  MakeButton(160,  35, "LevelEditorGUI/undoButton"));
            btns.Add("UpTime",  MakeButton(440,  35, "LevelEditorGUI/upTimeButton"));
            btns.Add("MoreUp",  MakeButton(560,  35, "LevelEditorGUI/moreButton"));
            btns.Add("MoreHold",  MakeButton(820,  35, "LevelEditorGUI/moreButton"));
            btns.Add("LessUp",  MakeButton(615,  35, "LevelEditorGUI/lessButton"));
            btns.Add("LessHold",  MakeButton(875,  35, "LevelEditorGUI/lessButton"));
            btns.Add("AddShape", MakeButton(1040, 35, "LevelEditorGUI/addShapeButton"));
            btns.Add("AddAlpha", MakeButton(1105, 35, "LevelEditorGUI/addAlphButton"));
            btns.Add("AddNumbr", MakeButton(1170, 35, "LevelEditorGUI/addNumButton"));
            btns.Add("Edit", MakeButton(1230, 35, "LevelEditorGUI/editButton"));
            btns.Add("HelpBtn", MakeButton(((ScreenWidth) - 55), 35, "HELP/LEhelpIcon"));

            // Also, the grid.
            grid = Content.Load<Texture2D>("LevelEditorGUI/placementGrid");
            addLabel = Content.Load<Texture2D>("LevelEditorGUI/addLabel");

            //setup the rectangle for the grid
            this.gridRect = new Rectangle(0, 110, ScreenWidth, ScreenHeight);

            // Sets upTime and holdTime if already been set
            if (myLevel.upTime > 0)
            {
                intUpTime = myLevel.upTime;
            }
            else
            {
                intUpTime = 5;
            }
            if (myLevel.holdTime >= 0)
            {
                intHoldTime = myLevel.holdTime;
            }
            else
            {
                intHoldTime = 1;
            }
            // Place the palette.
            palette = new Palette((ScreenWidth / 2) - 50 , 100);
            palette.Hide(); // Keep it hidden!
            palette.LoadContent(Content);

        }

        public override void Update(GameTime gameTime)
        {

            // Update the palette -- and make sure the rest should be updated.
            palette.Update(MouseState);

            if (palette.ShouldBeModal)
            {
                return;
            }

            // Update stuff here!
            if (btns["Home"].IsClicked())
            {
                AddScreenAndChill(new MenuScreen());
            }
            if (btns["Menu"].IsClicked())
            {
                AddScreenAndChill(new LevelEditorMenuScreen());
            }
            if (btns["Play"].IsClicked())
            {
                //going to set the gameManager to have a reference to myLevel so that we can access
                //all the objects that we've created and now want to display on the gamescreen.
                //GameManager.GlobalInstance.activeLevel = myLevel; //commented out as were just re referencing, didn't seem to break anything.
                //also we want to check to make sure that there are objects on the playing field. if there are not then dont allow
                //transfer to the gameScreen.
                if(myLevel.objectList.Count == 0){
                    //show popup saying that you need to add at least 1 object to the level
                    System.Windows.Forms.MessageBox.Show("A minimum of 1 object must be added prior to playing the level.");
                }
                else{
                    //otherwise go to the gamescreen and play the level.
                    AddScreenAndChill(new GameScreen());
                }
                
            }
            if (btns["UpTime"].IsClicked() )
            {
                //Call A Method Defined In Another Class
            }
            if (btns["MoreUp"].IsClicked())
            {
                if (intUpTime < 30)
                {
                    intUpTime++;
                    GameManager.GlobalInstance.activeLevel.upTime = intUpTime;
                }
            }
            if (btns["LessUp"].IsClicked())
            {
                if (intUpTime > 1)
                {
                    intUpTime--;
                    GameManager.GlobalInstance.activeLevel.upTime = intUpTime;
                }
            }
            if (btns["MoreHold"].IsClicked())
            {
                if ((intHoldTime < 30) && (intHoldTime < intUpTime))
                {
                    intHoldTime++;
                    GameManager.GlobalInstance.activeLevel.holdTime = intHoldTime;
                }
            }
            if (btns["LessHold"].IsClicked())
            {
                if (intHoldTime > 0)
                {
                    intHoldTime--;
                    GameManager.GlobalInstance.activeLevel.holdTime = intHoldTime;
                }
            }
            if (btns["Redo"].IsClicked())
            {

            }
            if (btns["Undo"].IsClicked())
            {

            }
            if (btns["AddAlpha"].IsClicked())
            {
                palette.RequestStateChange("Alph");
                palette.Show();
            }
            if (btns["AddShape"].IsClicked())
            {
                palette.RequestStateChange("Shape");
                palette.Show();
            }
            if (btns["AddNumbr"].IsClicked())
            {
                palette.RequestStateChange("Num");
                palette.Show();
            }
            if (btns["Edit"].IsClicked())
            {
                
            }
            if (btns["HelpBtn"].IsClicked())
            {
                AddScreenAndChill(new LEHelpScreen());
            }
            if (btns["Multiple"].IsClicked())
            {
                if (multiState)
                {
                    multiState = false;
                    btns["Multiple"] = MakeButton(355, 30, "LevelEditorGUI/multipleToggleOff");
                    btns["Multiple"].Update(MouseState);
                    GameManager.GlobalInstance.activeLevel.multiSelect = false;
                }
                else
                {
                    multiState = true;
                    btns["Multiple"] = MakeButton(355, 30, "LevelEditorGUI/multipleToggleOn");
                    btns["Multiple"].Update(MouseState);
                    GameManager.GlobalInstance.activeLevel.multiSelect = true;
                }
                //Call A Method Defined In Another Class
            }

            // Handle the creation of object/entities.
            if (palette.ObjectFactory.IsReady())
            {
                var entity = palette.ObjectFactory.Make(Content);

                myLevel.addObject(entity);

                palette.Reset();
            }

            // Update the state of all buttons.
            foreach (var button in btns.Values)
            {
                button.Update(MouseState);
            }



            // This foreach loop will check if a button in the list of buttonlists
            // is clicked and if it is then we are going assign a reference to it via this.objBeingMoved
            // and when the next mouse click is registered put the object at the new coordinates.
            foreach (var myListofObjects in myLevel.objectList)
            {
                foreach (var myObject in myListofObjects)
                {
                    if (myObject.IsClicked())
                    {
                        //now were going to set the current clicked object on the leveleditor grid 
                        //to have its property shouldIbeDrawn = false.
                        //myObject.shouldIbeDrawn = false;
                        //now make a temporary reference to this object so that once we register a 
                        //new mouse click anywhere on the level editor grid we set the reference to
                        //have the new coordinates and put its shouldIBeDrawn property back to true;
                        this.objBeingMoved = myObject;
                    }
                }
            }

            
           
            //now we test if the mouse has been clickd and if it has move the current selected object to the coordinates clicked.
            //also only register one mouse click with the initial if statement
            this.mouseStateCurrent = MouseState;
            if (this.mouseStateCurrent.LeftButton == ButtonState.Pressed && this.mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                Console.WriteLine("Ive been clicked");
                if (this.objBeingMoved != null)
                {
                    Console.WriteLine("Object is not null, starting moving it.");
                    //need to implement a check to see if the spot we are clicking in intersects within the
                    //rectangle of the grid.

                    //set new position of the objectbeingMoved to wherever the mouse was just clicked.
                    int newXcoord = MouseState.X - (this.objBeingMoved.rectangle.Width / 2);
                    int newYcoord = MouseState.Y - (this.objBeingMoved.rectangle.Height / 2);
                    //set the new width and height of the objectBeingMoved
                    int newWidth = this.objBeingMoved.rectangle.Width;
                    int newHeight = this.objBeingMoved.rectangle.Height;

                    //create a new rectangle based on the above coordinates and width,height.
                    Rectangle newObjectBeingMovedPosition = new Rectangle(newXcoord, newYcoord, newWidth, newHeight);

                    //Test if the rectangle is contained in the gridRect
                    //if it is set it to the new position else dont allow it to be drawn, notify the user somehow that they need to
                    //click inside the grid.
                    if(this.gridRect.Contains(newObjectBeingMovedPosition)){

                        Console.WriteLine("Good: New position was inside the grid");

                        //TODO: now we need to check if the object we are about to re-draw to a new position 
                        //overlaps with any of the current objects. 
                        
                            //if there is only one object in the entire list then we can assume we are comparing
                            //the object being moved to itself. so we can just ignore intersection checking and 
                            //set the new position of the object.
                            if (myLevel.objectList.Count == 1 && this.myLevel.objectList.Count == 1)
                            {
                                Console.WriteLine("Good: Only one object in the list.");

                                this.objBeingMoved.rectangle = newObjectBeingMovedPosition;

                                //Now set it so that the object being moved property of shouldIBeDrawn is set back to true
                                //So this will now redraw the object at the new position.
                                this.objBeingMoved.shouldIbeDrawn = true;

                                //now remove the reference so we no long change the actual object in the list.
                                this.objBeingMoved = null;
                                Console.WriteLine("Object now has new coordinates, set it to null so we no longer move it.");

                            }
                            else
                            {
                                Console.WriteLine("Good: Multile objects in the list.");

                                //bool to keep track if there was an intersect
                                bool isThereAnIntersect = false;
                                foreach (List<TargetTapping.Back_end.Object> myListofObjects in myLevel.objectList)
                                {
                                    //check if the object being moved doesnt intersect with any other objects
                                    foreach (TargetTapping.Back_end.Object myObject in myListofObjects)
                                    {
                                        //if were comparing the object being redrawn to its self in the list just ignore it 
                                        //and skip this iteration of the loop.
                                        if(this.objBeingMoved.Equals(myObject)){
                                            Console.WriteLine("It's equal to itself, skipping this iteration.");
                                            continue;
                                        }
                                        //check a intersect with a specific object
                                        if (newObjectBeingMovedPosition.Intersects(myObject.rectangle))
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

                                    this.objBeingMoved.rectangle = newObjectBeingMovedPosition;

                                    //Now set it so that the object being moved property of shouldIBeDrawn is set back to true
                                    //So this will now redraw the object at the new position.
                                    this.objBeingMoved.shouldIbeDrawn = true;

                                    //now remove the reference so we no long change the actual object in the list.
                                    this.objBeingMoved = null;
                                    Console.WriteLine("Object now has new coordinates, set it to null so we no longer move it.");

                                }


                            }
     
                    }
                    else{

                        Console.WriteLine("Bad: New position was outside the grid");

                    }

                 }
            }
            mouseStatePrevious = mouseStateCurrent;


            //update the mousestate of each object (ie. test if its been clicked)
            foreach (var myListofObjects in myLevel.objectList)
            {
                foreach (var myObject in myListofObjects)
                {
                   myObject.Update(MouseState);
                }
            }




            base.Update(gameTime);
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            //draw add object label
            spriteBatch.Draw(addLabel, addLabelPosition, Color.White);
            
            // Draw all of the buttons.
            foreach (var button in btns.Values)
            {
                button.Draw(spriteBatch);
            }

            // Draw the grid
            spriteBatch.Draw(grid,this.gridRect, Color.White);

            // draw the all objects that have been created in this level on the level grid
            foreach (List<TargetTapping.Back_end.Object> myListofObjects in myLevel.objectList)
            {
                foreach (TargetTapping.Back_end.Object myObject in myListofObjects)
                {
                    myObject.Draw(spriteBatch);

                }
            }
            spriteBatch.DrawString(font, intHoldTime.ToString(), intHoldTimePosition, Color.Black);
            spriteBatch.DrawString(font, intUpTime.ToString(), intUpTimePosition, Color.Black);
            // Draw the Palette
            palette.Draw(spriteBatch);
        }
    }
}
