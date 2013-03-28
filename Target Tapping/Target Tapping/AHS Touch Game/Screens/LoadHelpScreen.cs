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
    class LoadHelpScreen : AbstractRichScreen
    {
        private Button btnCancel;
        private Button cancelOpenHelp, searchHelp, listHelp, keyboardHelp;
        SpriteFont font;
        String cancelOpenHelpAnswer, searchHelpAnswer, listHelpAnswer, keyboardHelpAnswer;
        Vector2 answerPosition;
        private int whichAnswer = 3;

        Texture2D answerBackDrop, title;
        Vector2 answerBackDropPosition, titlePosition;

        public override void LoadContent()
        {

            base.LoadContent();
            answerPosition = (new Vector2(((ScreenWidth / 2) - 398), 112));
            btnCancel = MakeButton(0, 0, "GUI/cancel");
            cancelOpenHelp = MakeButton(0, 120, "HELP/cancelAndOpenHelp");
            searchHelp = MakeButton(0, 170, "HELP/searchHelp");
            listHelp = MakeButton(0, 220, "HELP/listHelp");
            keyboardHelp = MakeButton(0, 270, "HELP/searchKeyboardHelp");
            answerBackDrop = Content.Load<Texture2D>("HELP/answerBackDrop");
            answerBackDropPosition = (new Vector2(((ScreenWidth / 2) - 400), 110));
            title = Content.Load<Texture2D>("HELP/title");
            titlePosition = (new Vector2(((ScreenWidth / 2) - 150), 0));
            
            font = Content.Load<SpriteFont>("font");
            /////////////////////////////////////////////////////////////////////////////
            cancelOpenHelpAnswer = 
                "Cancel: Go to the previous screen.\n"+
                "Open: Open the selected environment from the list inside the editor.\n"+
                "Note: You must first select the environment you would like to load\n"+
                "from the list before you can proceed to the editor.\n";
            searchHelpAnswer = 
                "Tap inside the text box to open the keyboard, once the keyboard\n"+
                "has appearded you can type in your search query. If you would like\n"+
                "to clear your search query press the ' X ' next to the text box.\n"+
                "Once you have typed in your search query press ' GO ' to search the "+ 
                "list.\n";
            listHelpAnswer = 
                "The list shows all available saved environments. Once you have found\n"+
                "the environment you would like to open, tap it to select it, then\n"+
                "press the open button in the top right corner.\n"+
                "All environments previosuly saved successfully will appear inside\n"+
                "this list. If the list is to long you may want to search a \n"+
                "environment in the search bar above the list.\n";
            keyboardHelpAnswer = 
                "The keyboard is not visible until you have tapped on the search\n"+
                "To open the keyboard simply tap inside the seach text box. From\n"+
                "here you can type in your search query and press ' GO ' to search.\n";
            // Load buttons 'n' stuff, yo!
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Update stuff here!
            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new LoadLevelScreen());
            }
            if (cancelOpenHelp.IsClicked())
            {
                whichAnswer = 0;
            }
            if (searchHelp.IsClicked())
            {
                whichAnswer = 1;
            }
            if (listHelp.IsClicked())
            {
                whichAnswer = 2;
            }
            if (keyboardHelp.IsClicked())
            {
                whichAnswer = 3;
            }
            btnCancel.Update(MouseState);
            cancelOpenHelp.Update(MouseState);
            searchHelp.Update(MouseState);
            listHelp.Update(MouseState);
            keyboardHelp.Update(MouseState);

        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            cancelOpenHelp.Draw(spriteBatch);
            searchHelp.Draw(spriteBatch);
            listHelp.Draw(spriteBatch);
            keyboardHelp.Draw(spriteBatch);
            spriteBatch.Draw(answerBackDrop, answerBackDropPosition, Color.White);
            spriteBatch.Draw(title, titlePosition, Color.White);
            if (whichAnswer == 0)
            {
                spriteBatch.DrawString(font, cancelOpenHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 1)
            {
                spriteBatch.DrawString(font, searchHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 2)
            {
                spriteBatch.DrawString(font, listHelpAnswer, answerPosition, Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, keyboardHelpAnswer, answerPosition, Color.White);
            }


        }

    }
}
