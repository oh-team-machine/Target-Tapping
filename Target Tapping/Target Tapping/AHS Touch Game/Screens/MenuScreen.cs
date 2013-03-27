using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;

namespace TargetTapping.Screens
{
    class MenuScreen : AbstractRichScreen
    {

        private Button btnNew, btnLoad, btnExit, btnHelp;
        Texture2D myTitle;
        Vector2 myTitlePosition;


        public override void LoadContent()
        {
            base.LoadContent();
            myTitlePosition = new Vector2(((ScreenWidth / 2) - 400), 0);
            btnNew = MakeButton(((ScreenWidth/2)-300), ((ScreenHeight/3)), "GUI/newButton");
            btnLoad = MakeButton(((ScreenWidth / 2) - 300), ((ScreenHeight / 3)+150), "GUI/loadButton");
            btnExit = MakeButton(((ScreenWidth / 2) - 300), ((ScreenHeight / 3)+300), "GUI/exitButton");
            btnHelp = MakeButton(((ScreenWidth) - 55), ScreenHeight-55, "HELP/helpIcon");
            myTitle = Content.Load<Texture2D>("GUI/targetTappingGame");
            System.Diagnostics.Debug.WriteLine(((ScreenWidth / 2) - 300));
        }

        public override void Update(GameTime gameTime)
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
        else if (btnHelp.IsClicked())
        {
            AddScreenAndChill(new HomeHelpScreen());
        }
	    else if (btnExit.IsClicked())
	    {
            ScreenManager.Exit();
	    }

            btnNew.Update(MouseState);
            btnLoad.Update(MouseState);
            btnExit.Update(MouseState);
            btnHelp.Update(MouseState);

        }
        

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
	        // Draw all dem buttons.
            btnNew.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
            btnExit.Draw(spriteBatch);
            btnHelp.Draw(spriteBatch);

            spriteBatch.Draw(myTitle, myTitlePosition, Color.White);
        }

    }
}
