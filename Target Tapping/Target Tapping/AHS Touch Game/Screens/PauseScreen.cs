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
        Vector2 pauseMenuBackgroundPosition = (new Vector2(500, 200));
        Vector2 pauseMenuGraphicPosition = (new Vector2(530, 200));

        public override void LoadContent()
        {
            base.LoadContent();
            pauseMenuBackground = content.Load<Texture2D>("GamePauseMenu/menuBackground");
            pauseMenuTitle = content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            btnPauseContinue = MakeButton(530, 255, "GamePauseMenu/continueButtonGraphic");
            btnPauseEdit = MakeButton(530, 310, "GamePauseMenu/editButtonGraphic");
            btnPauseLoad = MakeButton(530, 365, "GamePauseMenu/changeLevelButtonGraphic");
            btnPauseRestart = MakeButton(530, 420, "GamePauseMenu/restartButtonGraphic");
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
                AddScreenAndChill(new LevelEditScreen());
            }
            if (btnPauseLoad.IsClicked())
            {
                AddScreenAndChill(new LoadLevelScreen());
            }
            if (btnPauseRestart.IsClicked())
            {
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
