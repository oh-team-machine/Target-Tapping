using GameLibrary.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.Screens
{
    internal class PauseScreen : AbstractRichScreen
    {
        private Button _btnPauseContinue;
        private Button _btnPauseEdit;

        private Button _btnPauseLoad,
                       _btnPauseRestart;

        private Texture2D _pauseMenuBackground;
        private Vector2 _pauseMenuBackgroundPosition;
        private Vector2 _pauseMenuGraphicPosition;
        private Texture2D _pauseMenuTitle;

        public override void LoadContent()
        {
            base.LoadContent();
            _pauseMenuBackgroundPosition =
                    (new Vector2(((ScreenWidth/2) - 100),
                                 ((ScreenHeight/2) - 175)));
            _pauseMenuGraphicPosition =
                    (new Vector2(((ScreenWidth/2) - 60),
                                 ((ScreenHeight/2) - 175)));
            _pauseMenuBackground =
                    Content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            _pauseMenuTitle =
                    Content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            _btnPauseContinue = MakeButton(((ScreenWidth/2) - 60),
                                          ((ScreenHeight/2) - 150) + 20,
                                          "GamePauseMenu/continueButtonGraphic");
            _btnPauseEdit = MakeButton(((ScreenWidth/2) - 60),
                                      ((ScreenHeight/2) - 150) + 75,
                                      "GamePauseMenu/editButtonGraphic");
            _btnPauseLoad = MakeButton(((ScreenWidth/2) - 60),
                                      ((ScreenHeight/2) - 150) + 130,
                                      "GamePauseMenu/changeLevelButtonGraphic");
            _btnPauseRestart = MakeButton(((ScreenWidth/2) - 60),
                                         ((ScreenHeight/2) - 150) + 185,
                                         "GamePauseMenu/restartButtonGraphic");
            // Load buttons 'n' stuff, yo!
        }

        public override void Update(GameTime gameTime)
        {
            // Update stuff here!
            if (_btnPauseContinue.IsClicked())
            {
                ScreenManager.RemoveScreen(this);
            }
            if (_btnPauseEdit.IsClicked())
            {
                foreach (
                        var myListofObjects in
                                GameManager.GlobalInstance.activeLevel
                                           .objectList)
                {
                    foreach (var myObject in myListofObjects)
                    {
                        myObject.shouldIbeDrawn = true;
                    }
                }
                AddScreenAndChill(new LevelEditScreen());
            }
            if (_btnPauseLoad.IsClicked())
            {
                foreach (
                        var myListofObjects in
                                GameManager.GlobalInstance.activeLevel
                                           .objectList)
                {
                    foreach (var myObject in myListofObjects)
                    {
                        myObject.shouldIbeDrawn = true;
                    }
                }
                AddScreenAndChill(new LoadLevelScreen());
            }
            if (_btnPauseRestart.IsClicked())
            {
                foreach (
                        var myListofObjects in
                                GameManager.GlobalInstance.activeLevel
                                           .objectList)
                {
                    foreach (var myObject in myListofObjects)
                    {
                        myObject.shouldIbeDrawn = true;
                    }
                }
                AddScreenAndChill(new GameScreen());
            }

            _btnPauseContinue.Update(MouseState);
            _btnPauseEdit.Update(MouseState);
            _btnPauseLoad.Update(MouseState);
            _btnPauseRestart.Update(MouseState);
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pauseMenuBackground, _pauseMenuBackgroundPosition,
                             Color.White);
            spriteBatch.Draw(_pauseMenuTitle, _pauseMenuGraphicPosition,
                             Color.White);
            _btnPauseRestart.Draw(spriteBatch);
            _btnPauseLoad.Draw(spriteBatch);
            _btnPauseEdit.Draw(spriteBatch);
            _btnPauseContinue.Draw(spriteBatch);
        }
    }
}