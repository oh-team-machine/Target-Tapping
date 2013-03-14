﻿using System;
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
        Vector2 pauseMenuBackgroundPosition = (new Vector2(600, 300));
        Vector2 pauseMenuGraphicPosition = (new Vector2(630, 300));

        public override void LoadContent()
        {
            base.LoadContent();
            pauseMenuBackground = content.Load<Texture2D>("GamePauseMenu/menuBackground");
            pauseMenuTitle = content.Load<Texture2D>("GamePauseMenu/pauseMenuGraphic");
            btnPauseContinue = MakeButton(630, 355, "GamePauseMenu/continueButtonGraphic");
            btnPauseEdit = MakeButton(630, 410, "GamePauseMenu/editButtonGraphic");
            btnPauseLoad = MakeButton(630, 465, "GamePauseMenu/changeLevelButtonGraphic");
            btnPauseRestart = MakeButton(630, 520, "GamePauseMenu/restartButtonGraphic");
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

        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(pauseMenuBackground, pauseMenuBackgroundPosition, Color.White);
            SpriteBatch.Draw(pauseMenuTitle, pauseMenuGraphicPosition, Color.White);
            btnPauseRestart.Draw(SpriteBatch);
            btnPauseLoad.Draw(SpriteBatch);
            btnPauseEdit.Draw(SpriteBatch);
            btnPauseContinue.Draw(SpriteBatch); 
        }

    }
}
