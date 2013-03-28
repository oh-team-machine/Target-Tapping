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
    class NewHelpScreen : AbstractRichScreen
    {

        private Button btnCancel;
        private Button cancelAndCreateHelp, descriptionAndNameHelp, xHelp, keyboardHelp;
        SpriteFont font;
        String cancelAndCreateHelpAnswer, descriptionAndNameHelpAnswer, xHelpAnswer, keyboardHelpAnswer;
        Vector2 answerPosition;
        private int whichAnswer = 3;

        Texture2D answerBackDrop, title;
        Vector2 answerBackDropPosition, titlePosition;

        public override void LoadContent()
        {

            base.LoadContent();
            answerPosition = (new Vector2(((ScreenWidth / 2) - 398), 112));
            btnCancel = MakeButton(0, 0, "GUI/cancel");
            cancelAndCreateHelp = MakeButton(0, 120, "HELP/cancelAndCreateHelp");
            descriptionAndNameHelp = MakeButton(0, 170, "HELP/descriptionAndNameHelp");
            xHelp = MakeButton(0, 220, "HELP/littleXHelp");
            keyboardHelp = MakeButton(0, 270, "HELP/keyboardHelp");
            answerBackDrop = Content.Load<Texture2D>("HELP/answerBackDrop");
            answerBackDropPosition = (new Vector2(((ScreenWidth / 2) - 400), 110));
            title = Content.Load<Texture2D>("HELP/title");
            titlePosition = (new Vector2(((ScreenWidth / 2) - 150), 0));

            font = Content.Load<SpriteFont>("font");
            /////////////////////////////////////////////////////////////////////////////
            cancelAndCreateHelpAnswer = 
                "Cancel: This button will bring you back to the previous screen.\n"+
                "Create: This button will advance you to the environment edditing screen\n"+
                "Note, in order to create a level, the name and description must be 2 or\n"+
                "more characters.";

            descriptionAndNameHelpAnswer = 
                "Description and Name are input fields to guide you towards a consistent\n"+
                "naming convention. In order to find previously loaded environments quickly\n"+
                "and without frustration, it is important to consistently name your\n" + 
                "environments. Following this naming convention will ensure this.\n"+
                "In the description field you should describe the environment you intend to\n"+
                "create very briefly, such as '40_Blue_Squares' or 'full_multi'\n"+
                "The name field can contain either your name as the therapist, or the\n" +
                "patient's name who this environment is intended for, like Joe.\n"+
                "The environment will then be saved in a file named along the lines of\n"+
                " 'Joe_40_Blue_Squares'. \n";

            xHelpAnswer = 
                "The little ' X ' located next to the textboxes will delete the text box's\n" +
                "content completely. This is a great way to start clean, rather than\n" +
                "having to backspace 15 times.\n";

            keyboardHelpAnswer = 
                "The keyboard is simple for two reasons. First this game does not require\n"+
                "alot of text input. Second in order to more strictly enforce naming \n"+
                "conventions we have limited the amount of characters you may use in naming.\n"+
                "Hopefully you will find it easier to remember what you have named your files\n"+
                "when you don't have to remember if you used upper/lower case, or if you\n"+
                "spelt out the number two, or used the number sign, 2.\n"+
                "\nWhy is there no space bar? We used underscores instead of whitespace as it\n"+
                "makes for cleaner, more structured looking file names.";

            // Load buttons 'n' stuff, yo!
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Update stuff here!
            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new NewLevelScreen());
            }
            if (cancelAndCreateHelp.IsClicked())
            {
                whichAnswer = 0;
            }
            if (descriptionAndNameHelp.IsClicked())
            {
                whichAnswer = 1;
            }
            if (xHelp.IsClicked())
            {
                whichAnswer = 2;
            }
            if (keyboardHelp.IsClicked())
            {
                whichAnswer = 3;
            }
            btnCancel.Update(MouseState);
            cancelAndCreateHelp.Update(MouseState);
            descriptionAndNameHelp.Update(MouseState);
            xHelp.Update(MouseState);
            keyboardHelp.Update(MouseState);

        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            cancelAndCreateHelp.Draw(spriteBatch);
            descriptionAndNameHelp.Draw(spriteBatch);
            xHelp.Draw(spriteBatch);
            keyboardHelp.Draw(spriteBatch);
            spriteBatch.Draw(answerBackDrop, answerBackDropPosition, Color.White);
            spriteBatch.Draw(title, titlePosition, Color.White);
            if (whichAnswer == 0)
            {
                spriteBatch.DrawString(font, cancelAndCreateHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 1)
            {
                spriteBatch.DrawString(font, descriptionAndNameHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 2)
            {
                spriteBatch.DrawString(font, xHelpAnswer, answerPosition, Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, keyboardHelpAnswer, answerPosition, Color.White);
            }


        }
    }
}
