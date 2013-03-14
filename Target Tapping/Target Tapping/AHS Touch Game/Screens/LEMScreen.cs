using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;

namespace TargetTapping.Screens
{
    class LEMScreen : AbstractRichScreen
    {
        Button btnLemBack, btnLemClear, btnLemExit, btnLemLoad, btnLemSave;
        Texture2D levelEditorMenuBackground, levelEditorMenuTitle;
        Vector2 levelEditorMenuPosition = (new Vector2(600, 300));
        Vector2 levelEditorMenuGraphicPosition = (new Vector2(630, 300));


        public override void LoadContent()
        {
            base.LoadContent();
            levelEditorMenuBackground = content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            levelEditorMenuTitle = content.Load<Texture2D>("LevelEditorMenu/levelEditorMenuGraphic");
            btnLemBack = MakeButton(630, 355, "LevelEditorMenu/backButtonGraphic");
            btnLemSave = MakeButton(630, 410, "LevelEditorMenu/saveButtonGraphic");
            btnLemLoad = MakeButton(630, 465, "LevelEditorMenu/loadButtonGraphic");
            btnLemClear = MakeButton(630, 520, "LevelEditorMenu/clearButtonGraphic");
            btnLemExit = MakeButton(630, 575, "LevelEditorMenu/exitButtonGraphic");

	       // Load buttons 'n' stuff, yo!
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
	       // Update stuff here!
            if (btnLemBack.IsClicked())
            {
                AddScreenAndChill(new LevelEditScreen());
                
            }
            if (btnLemSave.IsClicked())
            {
                AddScreenAndChill(new LevelEditScreen());
            }
            if (btnLemLoad.IsClicked())
            {
                AddScreenAndChill(new LevelEditScreen());
            }
            if (btnLemClear.IsClicked())
            {
                AddScreenAndChill(new LevelEditScreen());
            }
            if (btnLemExit.IsClicked())
            {
                ScreenManager.Exit();
            }
            btnLemSave.Update(mouseState);
            btnLemLoad.Update(mouseState);
            btnLemExit.Update(mouseState);
            btnLemClear.Update(mouseState);
            btnLemBack.Update(mouseState);

        }

        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {

            SpriteBatch.Draw(levelEditorMenuBackground, levelEditorMenuPosition, Color.White);
            SpriteBatch.Draw(levelEditorMenuTitle, levelEditorMenuGraphicPosition, Color.White);
            btnLemBack.Draw(SpriteBatch);
            btnLemClear.Draw(SpriteBatch);
            btnLemExit.Draw(SpriteBatch);
            btnLemLoad.Draw(SpriteBatch);
            btnLemSave.Draw(SpriteBatch);
        }

    }
}
