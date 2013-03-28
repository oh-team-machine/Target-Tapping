using System;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.FrontEnd;

namespace TargetTapping.Screens
{
    internal class NewLevelScreen : AbstractRichScreen
    {
        private Button btnCancel,
                       btnCreate, 
                       btnHelp;

        private Button clearDescriptionButton;
        private Button clearNameButton;

        private Button delDesc,
                       delName;

        private Vector2 descriptionBackgroundPosition;
        private String descriptionByTherapist;
        private Vector2 descriptionByTherapistPosition;
        private SpriteFont font;

        private Keyboard keyboard;
        private Texture2D myDescription;
        private Vector2 myDescriptionPosition;
        private Texture2D myName;
        private Vector2 myNamePosition;
        private Vector2 myNewLevelPosition;
        private Texture2D myNewLevelTitle;
        private Vector2 nameBackgroundPosition;
        private bool nameHighlight = false;
        private String nameOfTherapist;
        private Vector2 nameOfTherapistPosition;
        private Texture2D textBackgorund;

        
        private bool nameCreated = false;
        private bool descriptionCreated = false;


        public override void LoadContent()
        {
            //((ScreenWidth / 2) - 400)
            base.LoadContent();
            myNewLevelPosition = (new Vector2(((ScreenWidth / 2) - 300), 0));

            //descriptionBackgroundPosition = (new Vector2 (((ScreenWidth / 2) - 100), ((ScreenHeight / 2) - 100))) ;
            descriptionByTherapistPosition = (new Vector2(((ScreenWidth / 2) - 100), ((ScreenHeight / 2) - 100))) ;
            myDescriptionPosition = (new Vector2(((ScreenWidth / 2) - 300), ((ScreenHeight / 2) - 100)));

            //nameBackgroundPosition = (new Vector2(((ScreenWidth / 2) - 100), ((ScreenHeight / 2) - 40)));
            nameOfTherapistPosition = (new Vector2(((ScreenWidth / 2) - 100), ((ScreenHeight / 2) - 40)));
            myNamePosition = (new Vector2(((ScreenWidth / 2) - 300), ((ScreenHeight / 2) - 40)));
           


            font = Content.Load<SpriteFont>("font");
            nameOfTherapist = "";
            descriptionByTherapist = "";
            textBackgorund = Content.Load<Texture2D>("GUI/textBackground");
            myNewLevelTitle = Content.Load<Texture2D>("GUI/newLevel");

            btnCancel = MakeButton(0, 0, "GUI/cancel");
            btnCreate = MakeButton(((ScreenWidth) - 120), 0, "GUI/createButton");
            btnHelp = MakeButton(((ScreenWidth) - 55), ScreenHeight - 55, "HELP/helpIcon");
            delDesc = MakeButton(((ScreenWidth / 2) + 301), ((ScreenHeight / 2) - 107), "Gui/miniX");
            delName = MakeButton(((ScreenWidth / 2) + 301), ((ScreenHeight / 2) - 47), "Gui/miniX");

            myName = Content.Load<Texture2D>("GUI/name");
            clearNameButton = MakeButton(((ScreenWidth / 2) - 200), ((ScreenHeight / 2) - 40), "GUI/nothing");

            myDescription = Content.Load<Texture2D>("GUI/description");
            clearDescriptionButton = MakeButton(((ScreenWidth / 2) - 200), ((ScreenHeight / 2) - 100), "GUI/nothing");

            keyboard = new Keyboard(((ScreenWidth / 2) - 250), ((ScreenHeight) - 240), Content);
            keyboard.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (keyboard.CurrentKey != "")
            {
                // Handle backspace.
                if (keyboard.CurrentKey == "0")
                {
                    if (nameHighlight)
                    {
                        if (nameOfTherapist.Length > 0)
                        {
                            nameOfTherapist =
                                nameOfTherapist.Remove((nameOfTherapist.Length) - 1);
                        }
                    }
                    if (nameHighlight == false)
                    {
                        if (descriptionByTherapist.Length > 0)
                        {
                            descriptionByTherapist =
                                descriptionByTherapist.Remove((descriptionByTherapist.Length) - 1);
                        }
                    }
                }
                else
                {
                    if (nameHighlight)
                    {
                        nameOfTherapist = nameOfTherapist + keyboard.CurrentKey;
                        nameCreated = true;
                    }
                    if (nameHighlight == false)
                    {
                        descriptionByTherapist = descriptionByTherapist +
                                                 keyboard.CurrentKey;
                        descriptionCreated = true;
                    }
                }
            }

            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new MenuScreen());
            }
            if (btnCreate.IsClicked())
            {
                if (nameCreated && descriptionCreated)
                {
                    if ((nameOfTherapist.Length >= 1) && (descriptionByTherapist.Length >= 1))
                    {
                        GameManager.GlobalInstance.activeLevel.levelName = nameOfTherapist + "_" + descriptionByTherapist;
                        AddScreenAndChill(new LevelEditScreen());
                    }
                }
            }
            if (clearNameButton.IsClicked())
            {
                nameHighlight = true;
            }
            if (clearDescriptionButton.IsClicked())
            {
                nameHighlight = false;
            }
            if (delName.IsClicked())
            {
                nameHighlight = true;
                nameOfTherapist = "";
            }
            if (delDesc.IsClicked())
            {
                nameHighlight = false;
                descriptionByTherapist = "";
            }
            if (btnHelp.IsClicked())
            {
                AddScreenAndChill(new NewHelpScreen());
            }
            delDesc.Update(MouseState);
            delName.Update(MouseState);
            btnCancel.Update(MouseState);
            btnCreate.Update(MouseState);
            btnHelp.Update(MouseState);
            clearNameButton.Update(MouseState);
            clearDescriptionButton.Update(MouseState);
            // Update the keyboard and all of its keys.
            keyboard.Update(MouseState);
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            btnCreate.Draw(spriteBatch);
            btnHelp.Draw(spriteBatch);
            //spriteBatch.Draw(textBackgorund, nameBackgroundPosition, Color.White);
            clearDescriptionButton.Draw(spriteBatch);
            clearNameButton.Draw(spriteBatch);
            spriteBatch.Draw(myNewLevelTitle, myNewLevelPosition, Color.White);

            if (nameHighlight)
            {
                spriteBatch.DrawString(font, nameOfTherapist,
                                       nameOfTherapistPosition, Color.Black);
                spriteBatch.DrawString(font, descriptionByTherapist,
                                       descriptionByTherapistPosition,
                                       Color.DarkGray);
            }
            else
            {
                spriteBatch.DrawString(font, nameOfTherapist,
                                       nameOfTherapistPosition, Color.DarkGray);
                spriteBatch.DrawString(font, descriptionByTherapist,
                                       descriptionByTherapistPosition,
                                       Color.Black);
            }

            spriteBatch.Draw(myName, myNamePosition, Color.White);
            spriteBatch.Draw(myDescription, myDescriptionPosition, Color.White);

            delName.Draw(spriteBatch);
            delDesc.Draw(spriteBatch);
            keyboard.Draw(spriteBatch);
        }
    }
}