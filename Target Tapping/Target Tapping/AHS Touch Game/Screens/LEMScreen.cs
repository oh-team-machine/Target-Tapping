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
    class LEMScreen : AbstractRichScreen
    {
        Button btnLemBack, btnLemClear, btnLemExit, btnLemLoad, btnLemSave;
        Texture2D levelEditorMenuBackground, levelEditorMenuTitle;
        Vector2 levelEditorMenuPosition;
        Vector2 levelEditorMenuGraphicPosition;
        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public override void LoadContent()
        {
            //((screenWidth / 2) - 400)
            base.LoadContent();
            levelEditorMenuPosition = (new Vector2( ((screenWidth / 2) - 100), ((screenHeight / 2) - 175) ) );
            levelEditorMenuGraphicPosition = (new Vector2(((screenWidth / 2) - 60), ((screenHeight / 2) - 175) ) );

            levelEditorMenuBackground = content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            levelEditorMenuTitle = content.Load<Texture2D>("LevelEditorMenu/levelEditorMenuGraphic");
            btnLemBack = MakeButton(((screenWidth / 2) - 60), ((screenHeight / 2) - 150)+20, "LevelEditorMenu/backButtonGraphic");
            btnLemSave = MakeButton(((screenWidth / 2) - 60), ((screenHeight / 2) - 150) + 75, "LevelEditorMenu/saveButtonGraphic");
            btnLemLoad = MakeButton(((screenWidth / 2) - 60), ((screenHeight / 2) - 150) + 130, "LevelEditorMenu/loadButtonGraphic");
            btnLemClear = MakeButton(((screenWidth / 2) - 60), ((screenHeight / 2) - 150) + 185, "LevelEditorMenu/clearButtonGraphic");
            btnLemExit = MakeButton(((screenWidth / 2) - 60), ((screenHeight / 2) - 150) + 240, "LevelEditorMenu/exitButtonGraphic");

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
                GameManager.GlobalInstance.LevelNames.addFilename(GameManager.GlobalInstance.activeLevel.levelName);
                SerializableLevel sLevel = new SerializableLevel(GameManager.GlobalInstance.activeLevel);
                new SaveLevel().initiateSave(sLevel);

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
