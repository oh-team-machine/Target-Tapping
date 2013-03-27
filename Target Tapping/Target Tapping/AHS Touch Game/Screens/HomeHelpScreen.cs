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
    class HomeHelpScreen : AbstractRichScreen
    {
        private Button btnCancel;
        private Button newLayoutHelp, loadLayoutHelp, exitHelp, gameBreakDownHelp;
        SpriteFont font;
        String newLayoutHelpAnswer, loadLayoutHelpAnswer, exitHelpAnswer, gameBreakDownHelpAnswer;
        Vector2 answerPosition;
        private int whichAnswer = 3;

        Texture2D answerBackDrop;
        Vector2 answerBackDropPosition;

        public override void LoadContent()
        {
            
            base.LoadContent();
            answerPosition = (new Vector2(((ScreenWidth / 2) - 398), 112));
            btnCancel = MakeButton(0, 0, "GUI/cancel");
            newLayoutHelp = MakeButton(0, 120, "HELP/newLayoutHelp");
            loadLayoutHelp = MakeButton(0, 170, "HELP/loadLayoutHelp");
            exitHelp = MakeButton(0, 220, "HELP/exitHelp");
            gameBreakDownHelp = MakeButton(0, 270, "HELP/gameBreakDownHelp");
            answerBackDrop = Content.Load<Texture2D>("HELP/answerBackDrop");
            answerBackDropPosition = (new Vector2(((ScreenWidth / 2) - 400), 110));

            //
            //Below are the hard-coded answers for help topics.
            //
            font = Content.Load<SpriteFont>("font");
            ///////////////////////////////////////////////////CUT OFF HERE////////
            newLayoutHelpAnswer =
                "QUICK ANSWER:\nTap new layout if you would like to design a new\n" +
                "'Target-Tapping' environment for a patient.\n\n" +
                "LONG ANSWER:\n" +
                "As a therapist, your role is to design 'tapping environments' for\n" +
                "patients. Patients will then play the games you make for them.\n" +
                "To simplify the process of creating a 'tapping environment' we allow\n" +
                "you to save the tapping environments you create.\n" +
                "Enter a description of the game you intend to create so that in the \n" +
                "future you will be able to identify the 'tapping environment'. In\n" +
                "addition associate a name to the tapping environment. Name can be yours'\n" +
                "as the therapist, or it could be the patients name that the environment\n" +
                "is is intended for. Either way just name and describe your environments\n" +
                "in a way that will allow them to be identified in upcoming sessions.\n\n" +
                "E.G. Description: forty_blue_squares\n"+
                "     Name:        sydney_crosby\n" +
                "This will be saved as sydney_crosby_forty_blue_squares.\n" +
                "The important thing to note is for productivity make new environments \n" + 
                "when a current like-environment does not already exist. When \n" +
                "like-environments you may want to load them, instead of making a new\n" + 
                "environment.\n";

            loadLayoutHelpAnswer = "Choose load layout if you would like to";
            exitHelpAnswer = "The EXIT GAME button will return you to windows.\n Any unsaved progress will be lost.";
            gameBreakDownHelpAnswer = "This game is designed both for therapists and patients.";

            //end of answers for help.

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
	       // Update stuff here!
            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new MenuScreen());
            }
            if (newLayoutHelp.IsClicked())
            {
                whichAnswer = 0;
            }
            if (loadLayoutHelp.IsClicked())
            {
                whichAnswer = 1;
            }
            if (exitHelp.IsClicked())
            {
                whichAnswer = 2;
            }
            if (gameBreakDownHelp.IsClicked())
            {
                whichAnswer = 3;
            }
            btnCancel.Update(MouseState);
            newLayoutHelp.Update(MouseState);
            loadLayoutHelp.Update(MouseState);
            exitHelp.Update(MouseState);
            gameBreakDownHelp.Update(MouseState);

        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            newLayoutHelp.Draw(spriteBatch);
            loadLayoutHelp.Draw(spriteBatch);
            exitHelp.Draw(spriteBatch);
            gameBreakDownHelp.Draw(spriteBatch);
            spriteBatch.Draw(answerBackDrop, answerBackDropPosition, Color.White);
            if (whichAnswer == 0)
            {
                spriteBatch.DrawString(font, newLayoutHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 1)
            {
                spriteBatch.DrawString(font, loadLayoutHelpAnswer, answerPosition, Color.White);
            }
            else if (whichAnswer == 2)
            {
                spriteBatch.DrawString(font, exitHelpAnswer, answerPosition, Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, gameBreakDownHelpAnswer, answerPosition, Color.White);
            }

            
        }

    }
}
