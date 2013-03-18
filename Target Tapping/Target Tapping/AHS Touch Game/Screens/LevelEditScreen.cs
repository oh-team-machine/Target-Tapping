﻿using System;
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
        bool levelEditorMenuON = false; //Menu opens with menu button in level editor
        bool shapePalletVisible = true; // Showing the pallet
        // This should be replaced by FSM
        bool sizeChoosing = false; // Choosing the size of an object in the pallet
        bool colorChoosing = false; // Color choosing for an object in the pallet
        bool showingShapes = true; //Choosing between shapes in the pallet
        bool showingNumbers = false; //Choosing between numerical object in the pallet
        bool showingAlpha = false; //CHoosing alpha objets in the pallet

        int intUpTime = 5;
        int intHoldTime = 1;
        Vector2 intUpTimePosition = (new Vector2(520, 45));
        Vector2 intHoldTimePosition = (new Vector2(785, 45));
        SpriteFont font;

        // The level editor's got a bunch of buttons!
        Dictionary<String, Button> btns = new Dictionary<string,Button>();

        // Declare a new level object for the level editor screen to load in loadContent
        Level myLevel = GameManager.GlobalInstance.activeLevel;

        // Declare a new object that will be used to reference an object that we are moving
        // around on the levelEditor screen.
        private TargetTapping.Back_end.Object objBeingMoved = null;

        private Palette palette;

        // Title stuff, apparently.
        Texture2D grid;
        Vector2 gridPosition = (new Vector2(0, 110));
        float rotationAngle = 4.71238898F; // wat.
        Vector2 originForRotation = new Vector2(
             960, //(grid.Width / 2);
             485  //(grid.Height + 110) / 2;
        );

        public override void LoadContent()
        {
            base.LoadContent();

            font = content.Load<SpriteFont>("font");
            // Load all o' dem buttons
            btns.Add("Home",  MakeButton(30,  30, "LevelEditorGUI/homeButton"));
            btns.Add("Menu",  MakeButton(95,  30, "LevelEditorGUI/menuButton"));
            btns.Add("HoldTime",  MakeButton(700,  30, "LevelEditorGUI/holdTimeButton"));
            btns.Add("Multiple",  MakeButton(355,  30, "LevelEditorGUI/multipleToggleOff"));
            btns.Add("Play",  MakeButton(290,  30, "LevelEditorGUI/playButton"));
            btns.Add("Redo",  MakeButton(215,  30, "LevelEditorGUI/redoButton"));
            btns.Add("Undo",  MakeButton(160,  30, "LevelEditorGUI/undoButton"));
            btns.Add("UpTime",  MakeButton(440,  30, "LevelEditorGUI/upTimeButton"));
            btns.Add("MoreUp",  MakeButton(560,  30, "LevelEditorGUI/moreButton"));
            btns.Add("MoreHold",  MakeButton(820,  30, "LevelEditorGUI/moreButton"));
            btns.Add("LessUp",  MakeButton(615,  30, "LevelEditorGUI/lessButton"));
            btns.Add("LessHold",  MakeButton(875,  30, "LevelEditorGUI/lessButton"));

            // Also, the grid.
            grid = content.Load<Texture2D>("LevelEditorGUI/placementGrid");

            // Place the palette.
            palette = new Palette(0, 100);
            palette.LoadContent(content);

        }

        public override void Update(GameTime gameTime)
        {
            

	        // Update stuff here!
            if (btns["Home"].IsClicked())
            {
                AddScreenAndChill(new MenuScreen());
            }
            if (btns["Menu"].IsClicked())
            {
                AddScreenAndChill(new LEMScreen());
            }
            if (btns["Play"].IsClicked())
            {
                //going to set the gameManager to have a reference to myLevel so that we can access
                //all the objects that we've created and now want to display on the gamescreen.
                GameManager.GlobalInstance.activeLevel = this.myLevel;
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
                }
            }
            if (btns["LessUp"].IsClicked())
            {
                if (intUpTime > 1)
                {
                    intUpTime--;
                }
            }
            if (btns["MoreHold"].IsClicked())
            {
                if (intHoldTime < 30)
                {
                    intHoldTime++;
                }
            }
            if (btns["LessHold"].IsClicked())
            {
                if (intHoldTime > 1)
                {
                    intHoldTime--;
                }
            }
            if (btns["Redo"].IsClicked())
            {

            }
            if (btns["Undo"].IsClicked())
            {

            }
            if (btns["Multiple"].IsClicked())
            {
                if (multiState == true)
                {
                    multiState = false;
                    btns["Multiple"] = MakeButton(355, 30, "LevelEditorGUI/multipleToggleOff");
                    btns["Multiple"].Update(mouseState);
                }
                else if (multiState == false)
                {
                    multiState = true;
                    btns["Multiple"] = MakeButton(355, 30, "LevelEditorGUI/multipleToggleOn");
                    btns["Multiple"].Update(mouseState);
                }
                //Call A Method Defined In Another Class
            }

            // Handle the creation of stuff.
            if (palette.ObjectFactory.IsReady())
            {
                var fac = palette.ObjectFactory;

                var color = fac.Color;
                var type = fac.Type;
                var name = fac.Name;
                var pos = fac.Coordinates;
                // TODO: Not a hardcoded size!
                var rect = new Rectangle(pos.X, pos.Y, 100, 100);
                var graphman = GameManager.GlobalInstance.Graphics;

                var level = new Back_end.Object(type, name, rect, color, content,
                                                graphman);
                myLevel.addObject(level);

                palette.Reset();
            }

            // Update the state of all buttons.
            foreach (var button in btns.Values)
            {
                button.Update(mouseState);
            }
            // Update the palette
            palette.Update(mouseState);

            // This foreach loop will check if a button in the list of buttonlists
            // is clicked and if it is then we are going to move its position.
            foreach (var myListofObjects in myLevel.objectList)
            {
                foreach (var myObject in myListofObjects)
                {
                    // Update the state of all objects that have been created in this level on the level grid
                    myObject.Update(mouseState);

                    if (myObject.IsClicked())
                    {
                        //now were going to set the current clicked object on the leveleditor grid 
                        //to have its property shouldIbeDrawn = false.
                        //myObject.shouldIbeDrawn = false;
                        //now make a temporary reference to this object so that once we register a 
                        //new mouse click anywhere on the level editor grid we set the reference to
                        //have the new coordinates and put its shouldIBeDrawn property back to true;
                        this.objBeingMoved = myObject;
                        //Console.WriteLine("Stuff");

                    }
                }
            }
            
            //now were going to test if the mouse has been clicked anywhere on the leveleditor grid
            if(mouseState.LeftButton == ButtonState.Pressed){

                if (this.objBeingMoved != null)
                {
                    //need to implement a check to see if the spot we are clicking in intersects within the
                    //rectangle of the grid.

                    //set new position of the objectbeingMoved to wherever the mouse was just clicked.
                    //add one pixel so we are not above the shape when we drop it!!!!!!!!!!!!!!!!!!!!
                    this.objBeingMoved.rectangle = new Rectangle(mouseState.X+1, mouseState.Y+1,
                        this.objBeingMoved.rectangle.Width, this.objBeingMoved.rectangle.Height);

                    //Now set it so that the object being moved property of shouldIBeDrawn is set back to true
                    //So this will now redraw the object at the new position.
                    this.objBeingMoved.shouldIbeDrawn = true;

                    //now remove the reference so we no long change the actual object in the list.
                    this.objBeingMoved = null;
                }
            
            }
            
            base.Update(gameTime);
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {

            
            // Draw all of the buttons.
            foreach (var button in btns.Values)
            {
                button.Draw(spriteBatch);
            }

            // Draw dat grid, yo.
            spriteBatch.Draw(grid,
                   gridPosition, null, Color.White, rotationAngle, originForRotation, 1.0f, SpriteEffects.None, 0f);

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
