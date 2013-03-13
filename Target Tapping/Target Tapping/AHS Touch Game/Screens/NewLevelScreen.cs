using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.FrontEnd;
using Microsoft.Xna.Framework;
using GameLibrary.UI;

namespace TargetTapping.Screens
{
    class NewLevelScreen : AbstractRichScreen
    {
        private Keyboard keyboard;
        Texture2D myNewLevelTitle, myName, myDescription, textBackgorund;
        Vector2 myNewLevelPosition = (new Vector2(300, 0));
        Vector2 nameBackgroundPosition = (new Vector2(200, 130));
        Vector2 descriptionBackgroundPosition = (new Vector2(200, 200));
        Vector2 myNamePosition = (new Vector2(0, 130));
        Vector2 myDescriptionPosition = (new Vector2(0, 200));
        SpriteFont font;
        String nameOfTherapist;
        String descriptionByTherapist;
        Vector2 nameOfTherapistPosition = (new Vector2(200, 130));
        Vector2 descriptionByTherapistPosition = (new Vector2(200, 200));
        bool nameHighlight = true;
        Button btnCancel, btnCreate, delDesc, delName, clearNameButton, clearDescriptionButton;
        
        

        public override void LoadContent()
        {
            base.LoadContent();

            font = content.Load<SpriteFont>("font");
            nameOfTherapist = "Enter Your Name Here...";
            descriptionByTherapist = "Enter Your Description Here...";
            textBackgorund = content.Load<Texture2D>("GUI/textBackground");
            myNewLevelTitle = content.Load<Texture2D>("GUI/newLevel");

            btnCancel = MakeButton(0,0,"GUI/cancel");
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

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Update stuff here!
            //if (keyboard.CurrentKey != "")
            //{
            //}

            if (keyboard.CurrentKey != "")
            {
                if (keyboard.CurrentKey == "0")
                {
                    if (nameHighlight == true) { 
                        if (nameOfTherapist.Length > 0)
                        nameOfTherapist = nameOfTherapist.Remove((nameOfTherapist.Length) - 1);
                    }
                    if (nameHighlight == false) {
                        if (descriptionByTherapist.Length > 0)
                        descriptionByTherapist = descriptionByTherapist.Remove((descriptionByTherapist.Length) - 1);
                    }
                }
                else
                {
                    if (nameHighlight == true) { nameOfTherapist = nameOfTherapist + keyboard.CurrentKey; }
                    if (nameHighlight == false) { descriptionByTherapist = descriptionByTherapist + keyboard.CurrentKey; }
                }
            }

            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new MenuScreen());
            }
            if (btnCreate.IsClicked())
            {
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

        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {

            btnCancel.Draw(SpriteBatch);
            btnCreate.Draw(SpriteBatch);
            SpriteBatch.Draw(textBackgorund, nameBackgroundPosition, Color.White);
            clearDescriptionButton.Draw(SpriteBatch);
            clearNameButton.Draw(SpriteBatch);
            SpriteBatch.Draw(myNewLevelTitle, myNewLevelPosition, Color.White);
            if (nameHighlight == true) { SpriteBatch.DrawString(font, nameOfTherapist, nameOfTherapistPosition, Color.Black); }
            else { SpriteBatch.DrawString(font, nameOfTherapist, nameOfTherapistPosition, Color.DarkGray); }

            if (nameHighlight == false) { SpriteBatch.DrawString(font, descriptionByTherapist, descriptionByTherapistPosition, Color.Black); }
            else { SpriteBatch.DrawString(font, descriptionByTherapist, descriptionByTherapistPosition, Color.DarkGray); }

            SpriteBatch.Draw(myName, myNamePosition, Color.White);
            SpriteBatch.Draw(myDescription, myDescriptionPosition, Color.White);

            delName.Draw(SpriteBatch);
            delDesc.Draw(SpriteBatch);
            keyboard.Draw(SpriteBatch);
        }
    }
}
