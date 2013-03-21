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
        Vector2 levelEditorMenuPosition = (new Vector2(500, 200));
        Vector2 levelEditorMenuGraphicPosition = (new Vector2(530, 200));


        public override void LoadContent()
        {
            base.LoadContent();
            levelEditorMenuBackground = content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            levelEditorMenuTitle = content.Load<Texture2D>("LevelEditorMenu/levelEditorMenuGraphic");
            btnLemBack = MakeButton(530, 255, "LevelEditorMenu/backButtonGraphic");
            btnLemSave = MakeButton(530, 310, "LevelEditorMenu/saveButtonGraphic");
            btnLemLoad = MakeButton(530, 365, "LevelEditorMenu/loadButtonGraphic");
            btnLemClear = MakeButton(530, 420, "LevelEditorMenu/clearButtonGraphic");
            btnLemExit = MakeButton(530, 475, "LevelEditorMenu/exitButtonGraphic");

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

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(levelEditorMenuBackground, levelEditorMenuPosition, Color.White);
            spriteBatch.Draw(levelEditorMenuTitle, levelEditorMenuGraphicPosition, Color.White);
            btnLemBack.Draw(spriteBatch);
            btnLemClear.Draw(spriteBatch);
            btnLemExit.Draw(spriteBatch);
            btnLemLoad.Draw(spriteBatch);
            btnLemSave.Draw(spriteBatch);
        }

    }
}
