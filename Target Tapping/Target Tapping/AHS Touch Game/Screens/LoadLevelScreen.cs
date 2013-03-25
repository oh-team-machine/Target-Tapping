using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Vector2 magnifyGlassPosition = (new Vector2(175, 85));
        Vector2 lisBackgroundPosition = (new Vector2(350, 150));
        Vector2 searchBackground = (new Vector2(200, 85));
        Vector2 myLoadLevelTitlePosition = (new Vector2(330, 0));
        Vector2 myCancelButtonPosition = (new Vector2(0, 0));

        SpriteFont font;
        String searchQuery;
        Vector2 searchQueryPosition = (new Vector2(200, 90));
        bool loadKeyBoard = false;
        Button btnCancel, btnOpen, delSearch, goSearch;
        Button clearSearchButton;
        private Keyboard keyboard;

        LevelNames levelNames = GameManager.GlobalInstance.LevelNames;

        public override void LoadContent()
        {
            base.LoadContent();
            font = content.Load<SpriteFont>("font");
            searchQuery = "";
            textBackgorund = content.Load<Texture2D>("GUI/textBackground");
            magnifyGlass = content.Load<Texture2D>("GUI/magnifyGlass");
            listBackground = content.Load<Texture2D>("GUI/listBackground");
            myLoadLevelTitle = content.Load<Texture2D>("GUI/loadGameTitle");
            btnCancel = MakeButton(0, 0, "GUI/cancel");
            btnOpen = MakeButton(1160, 0, "GUI/openButton");
            delSearch = MakeButton(400, 85, "Gui/miniX");
            goSearch = MakeButton(425, 85, "Gui/go");
            clearSearchButton = MakeButton(175, 85, "GUI/nothing2");
            keyboard = new Keyboard(401, 520, content);
            keyboard.LoadContent();
            fileList = new List(levelNames.getFileNames(), 600, 15, 20, lisBackgroundPosition, listBackground, font, Color.Black, Color.Yellow);
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
                GameManager.GlobalInstance.activeLevel = sLevel.constructLevel(content, graphics);
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
            // update load screen
            btnCancel.Update(mouseState);
            btnOpen.Update(mouseState);
            delSearch.Update(mouseState);
            clearSearchButton.Update(mouseState);
            goSearch.Update(mouseState);
            if (loadKeyBoard == true)
            {
                keyboard.Update(mouseState);
            }

            fileList.Update(gameTime, mouseState);

            base.Update(gameTime);
        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            
            btnCancel.Draw(spriteBatch);
            btnOpen.Draw(spriteBatch);
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
            spriteBatch.Draw(listBackground, lisBackgroundPosition, Color.White);
            delSearch.Draw(spriteBatch);

            fileList.Draw(spriteBatch);

            if (loadKeyBoard == true)
            {
                keyboard.Draw(spriteBatch);
            }
        }
    }
}
