using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using TargetTapping.Back_end;
using Microsoft.Xna.Framework.Input;

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

        // The level editor's got a bunch of buttons!
        Dictionary<String, Button> btns = new Dictionary<string,Button>();

        // Declare a new level object for the level editor screen to load in loadContent
        Level myLevel = new Level();

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

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

	        // Update stuff here!
            if (btns["Home"].IsClicked())
            {
                ScreenManager.RemoveScreen(this);
            }


            // Update the state of all buttons
            foreach (var button in btns.Values)
            {
                button.Update(mouseState);
            }

            // Update the state of all objects that have been created in this level on the level grid
            foreach (List<TargetTapping.Back_end.Object> myListofObjects in myLevel.objectList)
            {
                foreach (TargetTapping.Back_end.Object myObject in myListofObjects)
                {
                    myObject.Update(mouseState);
                }
            }

            // This foreach loop will check if a button in the list of buttonlists
            // is clicked and if it is then we are going to move its position.
            foreach (List<TargetTapping.Back_end.Object> myListofObjects in myLevel.objectList)
            {
                foreach (TargetTapping.Back_end.Object myObject in myListofObjects)
                {
                    if (myObject.IsClicked())
                    {
                        Console.WriteLine("Your but has been clicked");
                        myObject..Rect = new Rectangle(750, 750, 50, 50);
                    }
                    myObject.Update(mouseState);
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

            // draw the all objects that have been created in this level on the level grid
            foreach (List<TargetTapping.Back_end.Object> myListofObjects in myLevel.objectList)
            {
                foreach (TargetTapping.Back_end.Object myObject in myListofObjects)
                {
                    myObject.Draw(spriteBatch);
                }
            }

            // Draw dat grid, yo.
            spriteBatch.Draw(grid,
                   gridPosition, null, Color.White, rotationAngle, originForRotation, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
