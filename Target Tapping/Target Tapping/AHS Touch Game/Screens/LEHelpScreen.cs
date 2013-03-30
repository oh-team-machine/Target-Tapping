using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using TargetTapping.Back_end;

namespace TargetTapping.Screens
{
    class LEHelpScreen : AbstractRichScreen
    {

        private Button btnCancel;
        private Button toolbarHelp, addObjectHelp, designHelp, saveHelp;
        SpriteFont font;
        String toolbarHelpAnswer, addObjectHelpAnswer, designHelpAnswer, saveHelpAnswer;
        Vector2 answerPosition;
        private int whichAnswer = 3;

        Texture2D answerBackDrop, title;
        Vector2 answerBackDropPosition, titlePosition;

        public override void LoadContent()
        {

            base.LoadContent();
            answerPosition = (new Vector2(((ScreenWidth / 2) - 498), 112));
            btnCancel = MakeButton(0, 0, "GUI/cancel");
            toolbarHelp = MakeButton(0, 120, "HELP/toolbarHelp");
            addObjectHelp = MakeButton(0, 170, "HELP/addObjectHelp");
            designHelp = MakeButton(0, 220, "HELP/designHelp");
            saveHelp = MakeButton(0, 270, "HELP/saveHelp");
            answerBackDrop = Content.Load<Texture2D>("HELP/answerBackDrop");
            answerBackDropPosition = (new Vector2(((ScreenWidth / 2) - 400), 110));
            title = Content.Load<Texture2D>("HELP/title");
            titlePosition = (new Vector2(((ScreenWidth / 2) - 150), 0));

            font = Content.Load<SpriteFont>("font");
            /////////////////////////////////////////////////////////////////////////////
            toolbarHelpAnswer = 
                "TOOLBAR BUTTONS:\n"+
                "Home: The home button will take you to the games start screen, from there\n"+
                "      you can choose to create a new environment, load an existing\n"+ 
                "      environment, or quit the game and return to Windows.\n"+
                "Menu: The menu button will open a menu screen that allows you to save\n"+
                "      or load an environment, clear the current environment, exit to\n"+
                "      Windows, or go back to the level editor (closing the menu).\n"+
                "Undo/Redo: Undo and Redo allow you to revert actions you recently made.\n"+
                "Play: Pressing play will take you to the patient game, where the environment\n"+
                "      will be loaded and the patient can begin their rehabilitation. In the\n"+
                "      game screen the patient will have tap on the objects as they appear,\n"+
                "      as well the therapist can pause the game with the button in the upper\n"+
                "      left corner of the screen.\n"+
                "Multi: This is a button that allows you to tell the game creator that two or\n"+
                "      more objects should appear on screen in the game at a time. Once you\n"+
                "      turn on multi all objects placed until multi is turned off will appear\n"+
                "      simultanously in the game screen.\n"+
                "Up Time: This is the amount of time each object in the game should last for,\n"+
                "      if the object is not tapped within this amount of time it will\n"+
                "      dissapear.\n"+
                "Hold Time: This is the amount of time a object must be held for inorder for it\n"+
                "      to dissapear.\n"+
                "Add Label: You will notice inside the box that says add, there are shapes,\n"+
                "      numbers, and letters. Tap on which ever one you would like to\n"+
                "      place on the grid, select the prefered size and color, then tap on the\n"+
                "      screen where you would like to place the object.\n";

            addObjectHelpAnswer = 
                "So you want to add an object? No problem, first choose from the toolbar\n"+
                "which object type you would like to add (shape, number, letter). Then\n"+
                "choose which size you would like the object to be. Next choose the\n"+
                "color you would like your object to be. Last thing, tap somewhere on the\n"+
                "grid where you would like your object to be placed.\n"+
                "If you would like to change the position of an object, tap on it in the\n"+
                "editor to select it, now tap where you would like to place it, thats all.\n"+
                "Objects by default will appear in the game in the order that they were placed\n"+
                "on the grid. To specify that multiple objects should appear at one, press multi,\n"+
                "while multi is ON you can put as many objects on the grid as you want to appear\n"+
                "simultaneously.";

            designHelpAnswer = 
                "Making a environment is left up to your creativity. If you are unsure how to\n"+
                "place objects on the gird check out the other help topics available on the left.\n"+
                "Some guidelines for making environments are: Put the objects on the gird in the\n"+
                "order you would like them to appear in the game. Use the MULTI feature to place\n"+
                "objects on the grid that will appear simultaneously. If you want to edit many\n"+
                "objects in an evironment, don't, instead make a new environment. Save your\n"+
                "environments often, dont risk loosing your changes.";

            saveHelpAnswer = 
                "One of the most important features this game offers you as a therapist, is the\n"+
                "ability to save the environments you create. This allows you to have quick access\n"+
                "to a broad range of environments and makes sessions with patients more productive.\n"+
                "To save an environment, tap MENU in the tool bar, then tap save, its that simple!.\n"+
                "Your environment will be saved with the name and description you gave when you\n"+
                "initially filled out the new environment screen. If you loaded a environment,\n"+
                "saving will overwrite the environment you initially loaded.\n";

            // Load buttons 'n' stuff, yo!
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Update stuff here!
            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new LevelEditScreen());
            }
            if (toolbarHelp.IsClicked())
            {
                whichAnswer = 0;
            }
            if (addObjectHelp.IsClicked())
            {
                whichAnswer = 1;
            }
            if (designHelp.IsClicked())
            {
                whichAnswer = 2;
            }
            if (saveHelp.IsClicked())
            {
                whichAnswer = 3;
            }
            btnCancel.Update(MouseState);
            toolbarHelp.Update(MouseState);
            addObjectHelp.Update(MouseState);
            designHelp.Update(MouseState);
            saveHelp.Update(MouseState);

        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            toolbarHelp.Draw(spriteBatch);
            addObjectHelp.Draw(spriteBatch);
            designHelp.Draw(spriteBatch);
            saveHelp.Draw(spriteBatch);
            spriteBatch.Draw(answerBackDrop, new Rectangle(((ScreenWidth / 2) - 500), 110, 1000, 1000), Color.White);
            spriteBatch.Draw(title, titlePosition, Color.White);

            if (whichAnswer == 0)
            {
                spriteBatch.DrawString(font, toolbarHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 1)
            {
                spriteBatch.DrawString(font, addObjectHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 2)
            {
                spriteBatch.DrawString(font, designHelpAnswer, answerPosition, Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, saveHelpAnswer, answerPosition, Color.White);
            }


        }
    }
}
