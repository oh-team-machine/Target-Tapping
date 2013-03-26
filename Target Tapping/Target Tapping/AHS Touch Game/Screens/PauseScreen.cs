using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;

namespace TargetTapping.Screens
{
    class PauseScreen : AbstractRichScreen
    {
        Button btnPauseLoad, btnPauseRestart, btnPauseEdit, btnPauseContinue;
        Texture2D pauseMenuBackground, pauseMenuTitle;
        Vector2 pauseMenuBackgroundPosition;
        Vector2 pauseMenuGraphicPosition;
        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public override void LoadContent()
        {
            base.LoadContent();
            pauseMenuBackgroundPosition = (new Vector2(((screenWidth / 2) - 200), ((screenHeight / 2) - 175)));
            pauseMenuGraphicPosition = (new Vector2(((screenWidth / 2) - 160), ((screenHeight / 2) - 175)));
            pauseMenuBackground = content.Load<Texture2D>("GamePauseMenu/menuBackground");
            pauseMenuTitle = content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            btnPauseContinue = MakeButton(((screenWidth / 2) - 160), ((screenHeight / 2) - 150) + 20, "GamePauseMenu/continueButtonGraphic");
            btnPauseEdit = MakeButton(((screenWidth / 2) - 160), ((screenHeight / 2) - 150) + 75, "GamePauseMenu/editButtonGraphic");
            btnPauseLoad = MakeButton(((screenWidth / 2) - 160), ((screenHeight / 2) - 150) + 130, "GamePauseMenu/changeLevelButtonGraphic");
            btnPauseRestart = MakeButton(((screenWidth / 2) - 160), ((screenHeight / 2) - 150) + 185, "GamePauseMenu/restartButtonGraphic");
	       // Load buttons 'n' stuff, yo!
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
	       // Update stuff here!
            if (btnPauseContinue.IsClicked())
            {
                ScreenManager.RemoveScreen(this);
            }
            if (btnPauseEdit.IsClicked())
            {
                foreach (var myListofObjects in GameManager.GlobalInstance.activeLevel.objectList)
                {
                    foreach (var myObject in myListofObjects)
                    {
                        myObject.shouldIbeDrawn = true;
                    }
                }
                AddScreenAndChill(new LevelEditScreen());
            }
            if (btnPauseLoad.IsClicked())
            {
                foreach (var myListofObjects in GameManager.GlobalInstance.activeLevel.objectList)
                {
                    foreach (var myObject in myListofObjects)
                    {
                        myObject.shouldIbeDrawn = true;
                    }
                }
                AddScreenAndChill(new LoadLevelScreen());
            }
            if (btnPauseRestart.IsClicked())
            {
                foreach (var myListofObjects in GameManager.GlobalInstance.activeLevel.objectList)
                {
                    foreach (var myObject in myListofObjects)
                    {
                        myObject.shouldIbeDrawn = true;
                    }
                }
                AddScreenAndChill(new GameScreen());
            }
            btnPauseContinue.Update(mouseState);
            btnPauseEdit.Update(mouseState);
            btnPauseLoad.Update(mouseState);
            btnPauseRestart.Update(mouseState);

        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pauseMenuBackground, pauseMenuBackgroundPosition, Color.White);
            spriteBatch.Draw(pauseMenuTitle, pauseMenuGraphicPosition, Color.White);
            btnPauseRestart.Draw(spriteBatch);
            btnPauseLoad.Draw(spriteBatch);
            btnPauseEdit.Draw(spriteBatch);
            btnPauseContinue.Draw(spriteBatch); 
        }

    }
}
