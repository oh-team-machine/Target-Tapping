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

        public override void LoadContent()
        {
            base.LoadContent();
            pauseMenuBackgroundPosition = (new Vector2(((ScreenWidth / 2) - 100), ((ScreenHeight / 2) - 175)));
            pauseMenuGraphicPosition = (new Vector2(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 175)));
            pauseMenuBackground = Content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            pauseMenuTitle = Content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            btnPauseContinue = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 20, "GamePauseMenu/continueButtonGraphic");
            btnPauseEdit = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 75, "GamePauseMenu/editButtonGraphic");
            btnPauseLoad = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 130, "GamePauseMenu/changeLevelButtonGraphic");
            btnPauseRestart = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 185, "GamePauseMenu/restartButtonGraphic");
	       // Load buttons 'n' stuff, yo!
        }

        public override void Update(GameTime gameTime)
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

            btnPauseContinue.Update(MouseState);
            btnPauseEdit.Update(MouseState);
            btnPauseLoad.Update(MouseState);
            btnPauseRestart.Update(MouseState);

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
