using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using GameLibrary.UI;

namespace TargetTapping.Screens
{
    class GameScreen : AbstractRichScreen
    {
        #region Variables

        // Stuff 'em in here, boss!
        Button btnPause;

        #endregion Variables

        public override void LoadContent()
        {
            base.LoadContent();
            btnPause = MakeButton(0, 0, "GUI/pauseButton");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (btnPause.IsClicked())
            {
                AddScreenAndChill(new PauseScreen());
            }
            btnPause.Update(mouseState);
            base.Update(gameTime);
        }


        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {
            btnPause.Draw(SpriteBatch);
        }
    }
}