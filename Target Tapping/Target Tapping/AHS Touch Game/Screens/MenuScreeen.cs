using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;

namespace TargetTapping.Screens
{
    class MenuScreen : AbstractRichScreen
    {

        private Button btnNew, btnLoad, btnExit;

        public override void LoadContent()
        {
            base.LoadContent();

            btnNew = MakeButton(340, 200, "GUI/newButton");
            btnLoad = MakeButton(340, 350, "GUI/loadButton");
            btnExit = MakeButton(340, 500, "GUI/exitButton");

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
	    
	    // Check if any of the buttons have been clicked.
	    if (btnNew.IsClicked())
	    {
                AddScreenAndChill(new NewLevelScreen());
	    }
	    else if (btnLoad.IsClicked())
	    {
                AddScreenAndChill(new LoadLevelScreen());
	    }
	    else if (btnExit.IsClicked())
	    {
                ScreenManager.Exit();
	    }

            btnNew.Update(mouseState);
            btnLoad.Update(mouseState);
            btnExit.Update(mouseState);

        }

        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {
	    // Draw all dem buttons.
            btnNew.Draw(SpriteBatch);
            btnLoad.Draw(SpriteBatch);
            btnExit.Draw(SpriteBatch);
        }

    }
}
