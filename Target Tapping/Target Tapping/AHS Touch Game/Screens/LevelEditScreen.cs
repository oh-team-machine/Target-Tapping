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
        

        //counter for debugging mouse clicksint
        private int mouseClickCounter = 0;
        //counter for debugging number of times a object is clicked
        private int shapeClickCounter = 0;

        //to handle the multiple number of clicks that occur
        MouseState mouseStateCurrent, mouseStatePrevious;


        // The level editor's got a bunch of buttons!
        Dictionary<String, Button> btns = new Dictionary<string,Button>();

        // Declare a new level object for the level editor screen to load in loadContent
        Level myLevel = GameManager.GlobalInstance.activeLevel;

        // Declare a new object that will be used to reference an object that we are moving
        // around on the levelEditor screen.
        private TargetTapping.Back_end.Object objBeingMoved = null;

        private Palette palette;

        // Title stuff, apparently.
        Texture2D grid, addLabel;
        Vector2 gridPosition = (new Vector2(0, 110));
        Vector2 addLabelPosition = (new Vector2(985, 32));

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

            // Sets upTime and holdTime if already been set
            if (myLevel.upTime > 0)
            {
                intUpTime = myLevel.upTime;
            }
            else
            {
                intUpTime = 5;
            }
            if (myLevel.holdTime > 0)
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
                GameManager.GlobalInstance.activeLevel = myLevel;
                AddScreenAndChill(new GameScreen());
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
                if (intHoldTime > 1)
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
                if (this.objBeingMoved != null)
                {
                    Console.WriteLine("Object is not null, starting moving it.");
                    //need to implement a check to see if the spot we are clicking in intersects within the
                    //rectangle of the grid.

                    //set new position of the objectbeingMoved to wherever the mouse was just clicked.
                    int newXcoord = MouseState.X - (this.objBeingMoved.rectangle.Width / 2);
                    int newYcoord = MouseState.Y - (this.objBeingMoved.rectangle.Height / 2);
                    this.objBeingMoved.rectangle = new Rectangle(newXcoord, newYcoord,
                        this.objBeingMoved.rectangle.Width, this.objBeingMoved.rectangle.Height);

                    //Now set it so that the object being moved property of shouldIBeDrawn is set back to true
                    //So this will now redraw the object at the new position.
                    this.objBeingMoved.shouldIbeDrawn = true;

                    //now remove the reference so we no long change the actual object in the list.
                    this.objBeingMoved = null;
                    Console.WriteLine("Object now had new coordinates set it to null.");

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

            // Draw dat grid, yo.
            //spriteBatch.Draw(grid,
              //     gridPosition, null, Color.White, null, null, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(grid,new Rectangle(0, 110, ScreenWidth, ScreenHeight), Color.White);

            

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
