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
                       btnCreate;

        private Button clearDescriptionButton;
        private Button clearNameButton;

        private Button delDesc,
                       delName;

        private Vector2 descriptionBackgroundPosition = (new Vector2(200, 200));
        private String descriptionByTherapist;
        private Vector2 descriptionByTherapistPosition = (new Vector2(200, 200));
        private SpriteFont font;

        private Keyboard keyboard;
        private Texture2D myDescription;
        private Vector2 myDescriptionPosition = (new Vector2(0, 200));
        private Texture2D myName;
        private Vector2 myNamePosition = (new Vector2(0, 130));
        private Vector2 myNewLevelPosition = (new Vector2(300, 0));
        private Texture2D myNewLevelTitle;
        private Vector2 nameBackgroundPosition = (new Vector2(200, 130));
        private bool nameHighlight = true;
        private String nameOfTherapist;
        private Vector2 nameOfTherapistPosition = (new Vector2(200, 130));
        private Texture2D textBackgorund;


        public override void LoadContent()
        {
            base.LoadContent();

            font = content.Load<SpriteFont>("font");
            nameOfTherapist = "Enter Your Name Here...";
            descriptionByTherapist = "Enter Your Description Here...";
            textBackgorund = content.Load<Texture2D>("GUI/textBackground");
            myNewLevelTitle = content.Load<Texture2D>("GUI/newLevel");

            btnCancel = MakeButton(0, 0, "GUI/cancel");
            btnCreate = MakeButton(1160, 0, "GUI/createButton");
            delDesc = MakeButton(500, 200, "Gui/miniX");
            delName = MakeButton(500, 130, "Gui/miniX");

            myName = content.Load<Texture2D>("GUI/name");
            clearNameButton = MakeButton(0, 130, "GUI/nothing");

            myDescription = content.Load<Texture2D>("GUI/description");
            clearDescriptionButton = MakeButton(0, 200, "GUI/nothing");

            keyboard = new Keyboard(401, 520, content);
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
                    }
                    if (nameHighlight == false)
                    {
                        descriptionByTherapist = descriptionByTherapist +
                                                 keyboard.CurrentKey;
                    }
                }
            }

            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new MenuScreen());
            }
            if (btnCreate.IsClicked())
            {
                GameManager.GlobalInstance.activeLevel.levelName = nameOfTherapist + "_" + descriptionByTherapist;
                AddScreenAndChill(new LevelEditScreen());
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
            delDesc.Update(mouseState);
            delName.Update(mouseState);
            btnCancel.Update(mouseState);
            btnCreate.Update(mouseState);
            clearNameButton.Update(mouseState);
            clearDescriptionButton.Update(mouseState);
            // Update the keyboard and all of its keys.
            keyboard.Update(mouseState);
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            btnCancel.Draw(spriteBatch);
            btnCreate.Draw(spriteBatch);
            spriteBatch.Draw(textBackgorund, nameBackgroundPosition, Color.White);
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