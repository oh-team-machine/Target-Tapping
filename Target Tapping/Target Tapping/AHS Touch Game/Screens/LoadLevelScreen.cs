using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameLibrary.UI;
using TargetTapping.FrontEnd;
using TargetTapping.Back_end;

namespace TargetTapping.Screens
{
    class LoadLevelScreen : AbstractRichScreen
    {
        Texture2D myLoadLevelTitle, textBackgorund, magnifyGlass, listBackground;
        Vector2 magnifyGlassPosition ;
        Vector2 listBackgroundPosition ;
        Vector2 searchBackground ;
        Vector2 myLoadLevelTitlePosition ;
        //Vector2 myCancelButtonPosition ;

        SpriteFont font;
        String searchQuery;
        Vector2 searchQueryPosition ;
        bool loadKeyBoard = false;
        Button btnCancel, btnOpen, delSearch, goSearch, btnHelp;
        Button clearSearchButton;
        private Keyboard keyboard;

        LevelNames levelNames = GameManager.GlobalInstance.LevelNames;

        public override void LoadContent()
        {
            base.LoadContent();
            searchQueryPosition = (new Vector2(((ScreenWidth / 2) - 275), 120));
            magnifyGlassPosition = (new Vector2(((ScreenWidth / 2) - 300), 120));
            listBackgroundPosition = (new Vector2( ((ScreenWidth / 2) - 300), 200));
            //searchBackground = (new Vector2( ((screenWidth / 2) - 280), 200));

            myLoadLevelTitlePosition = (new Vector2(((ScreenWidth / 2) - 300), 0));
            //myCancelButtonPosition = (new Vector2(0, 0));

            font = Content.Load<SpriteFont>("font");
            searchQuery = "";
            textBackgorund = Content.Load<Texture2D>("GUI/textBackground");
            magnifyGlass = Content.Load<Texture2D>("GUI/magnifyGlass");
            listBackground = Content.Load<Texture2D>("GUI/listBackground");
            myLoadLevelTitle = Content.Load<Texture2D>("GUI/loadGameTitle");

            btnHelp = MakeButton(((ScreenWidth) - 55), ScreenHeight - 55, "HELP/helpIcon");
            btnCancel = MakeButton(0, 0, "GUI/cancel");
            btnOpen = MakeButton(((ScreenWidth) - 120), 0, "GUI/openButton");
            delSearch = MakeButton( ((ScreenWidth / 2) - 19), 113, "Gui/miniX");
            goSearch = MakeButton(  ((ScreenWidth / 2) + 40), 113, "Gui/go");
            clearSearchButton = MakeButton( ((ScreenWidth / 2) - 275), 120, "GUI/nothing2");
            keyboard = new Keyboard(((ScreenWidth / 2) - 250), ((ScreenHeight) - 240), Content);
            keyboard.LoadContent();

            fileList = new List(levelNames.getFileNames(), 600, 15, 25, listBackgroundPosition, listBackground, font, Color.Black, Color.Yellow);
	    // Load buttons 'n' stuff, yo!
        }

        public List fileList { get; set; }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
	    // Lagic!
	    //AddScreenAndChill(new LoadLevelScreen(levelName));

            if (keyboard.CurrentKey != "")
            {
                if (keyboard.CurrentKey == "0")
                {
                   if (searchQuery.Length > 0)
                        searchQuery = searchQuery.Remove((searchQuery.Length) - 1);
                }
                else
                {
                    searchQuery = searchQuery + keyboard.CurrentKey;
                }
            }
            //if (goSearch.IsClicked())
            //{

            //}
            if (btnCancel.IsClicked())
            {
                AddScreenAndChill(new MenuScreen());
            }
            if (btnOpen.IsClicked())
            {
                LevelLoad levelL = new LevelLoad();
                SerializableLevel sLevel = levelL.initiateLoad(fileList.SelectedElement());
                var graphics = GameManager.GlobalInstance.Graphics;
                GameManager.GlobalInstance.activeLevel = sLevel.constructLevel(Content, graphics);
                AddScreenAndChill(new LevelEditScreen());
            }
            if (delSearch.IsClicked())
            {
                searchQuery = "";
            }
            if (clearSearchButton.IsClicked())
            {
                if (loadKeyBoard == false) { loadKeyBoard = true; } //else { loadKeyBoard = false; }
            }
            if (btnHelp.IsClicked())
            {
                AddScreenAndChill(new LoadHelpScreen());
            }
            // update load screen
            btnHelp.Update(MouseState);
            btnCancel.Update(MouseState);
            btnOpen.Update(MouseState);
            delSearch.Update(MouseState);
            clearSearchButton.Update(MouseState);
            goSearch.Update(MouseState);
            if (loadKeyBoard == true)
            {
                keyboard.Update(MouseState);
            }
            
            fileList.Update(gameTime, MouseState);

            base.Update(gameTime);
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            
            btnCancel.Draw(spriteBatch);
            btnOpen.Draw(spriteBatch);
            btnHelp.Draw(spriteBatch);
            spriteBatch.Draw(myLoadLevelTitle, myLoadLevelTitlePosition, Color.White);
            goSearch.Draw(spriteBatch);
            clearSearchButton.Draw(spriteBatch);
            if (loadKeyBoard == false)
            {
                spriteBatch.DrawString(font, searchQuery, searchQueryPosition, Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, searchQuery, searchQueryPosition, Color.Black);
            }
            spriteBatch.Draw(magnifyGlass, magnifyGlassPosition, Color.White);
            spriteBatch.Draw(listBackground, listBackgroundPosition, Color.White);
            delSearch.Draw(spriteBatch);

            fileList.Draw(spriteBatch);

            if (loadKeyBoard == true)
            {
                keyboard.Draw(spriteBatch);
            }
        }
    }
}
