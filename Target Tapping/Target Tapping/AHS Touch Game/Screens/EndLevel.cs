using GameLibrary.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.Screens
{
    internal class EndLevel : AbstractRichScreen
    {
        private Texture2D _background;
        private Vector2 _backgroundPosition;
        private Button _btnEditLevel;
        private Button _btnLoadLevel;
        private Button _btnPlayAgain;
        private Button _btnQuit;

        public override void LoadContent()
        {
            base.LoadContent();

            /* Load all of the graphics. */
            _backgroundPosition =
                    (new Vector2(((ScreenWidth/2) - 100),
                                 ((ScreenHeight/2) - 175)));
            _background =
                    Content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            _btnPlayAgain = MakeButton(0, 0, "herp/derp");
            _btnEditLevel = MakeButton(0, 0, "Herp/Derp");
            _btnLoadLevel = MakeButton(((ScreenWidth/2) - 60),
                                       ((ScreenHeight/2) - 150) + 130,
                                       "GamePauseMenu/changeLevelButtonGraphic");
            _btnQuit = MakeButton(((ScreenWidth/2) - 60),
                                  ((ScreenHeight/2) - 150) + 185,
                                  "GamePauseMenu/restartButtonGraphic");
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, _backgroundPosition, Color.White);
            _btnPlayAgain.Draw(spriteBatch);
            _btnEditLevel.Draw(spriteBatch);
            _btnLoadLevel.Draw(spriteBatch);
            _btnQuit.Draw(spriteBatch);
        }
    }
}