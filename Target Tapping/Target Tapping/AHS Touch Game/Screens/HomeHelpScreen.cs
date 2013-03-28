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
            ///////////////////////////////////////////////////CUT OFF HERE//////////
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

            loadLayoutHelpAnswer = //////////////////////////////////////////////////////
                "QUICK ANSWER:\nTap load layout if you would like to start playing a game\n" +
                "or change to a game that has already been designed. The game does not\n"+
                "come with any pre-created games, thus if you or another therapist have\n" +
                "not already created a 'target tapping environment' you will first have\n"+
                "to create one, which you will be able to load everytime afterwards.\n\n" +
                "LONG ANSWER:\nAs a therapist you may want to create environments that\n"+
                "accomplish different tasks, or have different features. For this reason\n"+
                "we allow you to retrive levels you have previosuly saved that have been\n" +
                "designed with certain goals in mind. An example may be that JOE's\n" +
                "rehabilitation is is currently best suited with a game that requires\n" +
                "him to use both his hands at once.\n"+
                "Therefore, you have created and saved an environment that uses the\n" +
                "'multi' on every tapping event. JOE may also benefit from a environment\n"+
                "where he has to spell out a word. Therefore you have created an\n" +
                "environment filled with letters.\n" + 
                "By saving and loading games you have the option of having many\n" +
                "environments pre-planned, saving you precious time with the patient.";

            exitHelpAnswer = 
                "The EXIT GAME button will return you to windows.\n"+
                "Any unsaved progress will be lost. Use this or any one of the other\n"+
                "EXIT buttons placed around the game to end your session.\n";

            gameBreakDownHelpAnswer = /////////////////////////////////////////////////// 
                "INTRO:\n"+
                "This game is designed both for therapists and patients. Therapist will\n" +
                "use this game as a tool to save time and energy while working with\n" +
                "patients. Patients will in turn benefit from more diverse environments,\n" +
                "as well as a more modern and fun experience.\n"+
                "This game is split into two interfaces, the therapist interface, and the\n"+
                "patient interface. The therapist interface allows therapists to design\n"+
                "environments they find match a patient or a group of patients needs best.\n"+
                "The patient interface is presented as a game. Patients will have to tap\n"+
                "objects as they appear on the screen, and will recieve a score depending\n"+
                "the number of times they tapped a object correctly. This allows for a more\n" +
                "engaging experience for the patient.\n"+
                "\nAVAILABLE TOOLS:\n"+
                "As a therapist you will have the option to design many great environments\n"+
                "and SAVE them for future use. This will save time and energy for you and\n"+
                "your patients. The therapist interface is broken down into 4 main screens.\n"+
                "The first is the main-menu, here you can choose to create new environments\n" +
                "or choose to load previously created environments. From here you may also\n"+
                "exit to Windows\n"+
                "The second and third interfaces are the 'new layout' and 'load layout'\n"+
                "screens. These screens repectively allow you to name a layout you are\n"+
                "about to create for the first time, or load a layout that has previously\n"+
                "been created by you or another therapist.\n"+
                "The last therepist interface is the environment editor. This screen allows\n"+
                "you as the thereapist to design your environment from scratch, or edit a\n"+
                "previously created environment that you have loaded.\n"+
                "On the patients side they have the game interface. The game interface is\n"+
                "where the patient will play the game that has been designed to aid in their\n"+
                "rehabilitation. The patients role is to tap on the objects as they appear\n"+
                "or, in general follow the therapists insructions.";

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
