using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;

namespace TargetTapping.Screens
{
    class MenuScreen : AbstractRichScreen
    {

        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private Button btnNew, btnLoad, btnExit;
        Texture2D myTitle;
        Vector2 myTitlePosition;
       

        public override void LoadContent()
        {
            base.LoadContent();
            myTitlePosition = new Vector2(((screenWidth / 2) - 400), 0);
            btnNew = MakeButton(((screenWidth/2)-300), ((screenHeight/3)), "GUI/newButton");
            btnLoad = MakeButton(((screenWidth / 2) - 300), ((screenHeight / 3)+150), "GUI/loadButton");
            btnExit = MakeButton(((screenWidth / 2) - 300), ((screenHeight / 3)+300), "GUI/exitButton");
            myTitle = content.Load<Texture2D>("GUI/targetTappingGame");
            System.Diagnostics.Debug.WriteLine(((screenWidth / 2) - 300));
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
	    
	    // Check if any of the buttons have been clicked.
	    if (btnNew.IsClicked())
	    {
            GameManager.GlobalInstance.activeLevel = new Back_end.Level();
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

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
	        // Draw all dem buttons.
            btnNew.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
            btnExit.Draw(spriteBatch);

            spriteBatch.Draw(myTitle, myTitlePosition, Color.White);
        }

    }
}
