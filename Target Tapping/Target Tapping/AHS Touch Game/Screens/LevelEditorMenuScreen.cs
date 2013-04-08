using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using TargetTapping.Back_end;
using TargetTapping.Back_end.Command;
using System.Threading;

namespace TargetTapping.Screens
{
    class LevelEditorMenuScreen : AbstractRichScreen
    {
        Button btnLemBack, btnLemClear, btnLemExit, btnLemLoad, btnLemSave;
        Texture2D levelEditorMenuBackground, levelEditorMenuTitle, savedPopUp;
        Vector2 levelEditorMenuPosition;
        Vector2 levelEditorMenuGraphicPosition;
        Vector2 savedPopUpPosition;
        bool wasSaveSuccess = false;
        bool switchToLoad = false;

        //Here were going to get the current level that we built in the leveleditor
        Level playingLevel = GameManager.GlobalInstance.activeLevel;

        public override void LoadContent()
        {
            //((screenWidth / 2) - 400)
            base.LoadContent();
            savedPopUpPosition = (new Vector2(((ScreenWidth / 2) - 100), ((ScreenHeight / 2) + 175)));
            levelEditorMenuPosition = (new Vector2( ((ScreenWidth / 2) - 100), ((ScreenHeight / 2) - 175) ) );
            levelEditorMenuGraphicPosition = (new Vector2(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 175) ) );

            levelEditorMenuBackground = Content.Load<Texture2D>("LevelEditorMenu/menuBackground");
            levelEditorMenuTitle = Content.Load<Texture2D>("LevelEditorMenu/levelEditorMenuGraphic");
            savedPopUp = Content.Load<Texture2D>("LevelEditorMenu/saveSuccessful");
            btnLemBack = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150)+20, "LevelEditorMenu/backButtonGraphic");
            btnLemSave = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 75, "LevelEditorMenu/saveButtonGraphic");
            btnLemLoad = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 130, "LevelEditorMenu/loadButtonGraphic");
            btnLemClear = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 185, "LevelEditorMenu/clearButtonGraphic");
            btnLemExit = MakeButton(((ScreenWidth / 2) - 60), ((ScreenHeight / 2) - 150) + 240, "LevelEditorMenu/exitButtonGraphic");

	       // Load buttons 'n' stuff, yo!
        }

        public override void Update(GameTime gameTime)
        {
	       // Update stuff here!
            if (switchToLoad == true)
            {
                switchToLoad = false;
                AddScreenAndChill(new LevelEditScreen());

            }
            if (btnLemBack.IsClicked())
            {
                AddScreenAndChill(new LevelEditScreen());
                
            }
            if (btnLemSave.IsClicked())
            {
                GameManager.GlobalInstance.LevelNames.addFilename(GameManager.GlobalInstance.activeLevel.levelName);
                var sLevel = new SerializableLevel(GameManager.GlobalInstance.activeLevel);
                new SaveLevel().initiateSave(sLevel);
                wasSaveSuccess = true;
                //AddScreenAndChill(new LevelEditScreen());
            }
            if (btnLemLoad.IsClicked())
            {
                var confirmResult = System.Windows.Forms.MessageBox.Show("Do you want to save the level before loading?", "Confirm Save!", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (confirmResult.ToString() == "Yes")
                {
                    // if 'Yes' do something here 
                    GameManager.GlobalInstance.LevelNames.addFilename(GameManager.GlobalInstance.activeLevel.levelName);
                    var sLevel = new SerializableLevel(GameManager.GlobalInstance.activeLevel);
                    new SaveLevel().initiateSave(sLevel);
                    System.Windows.Forms.MessageBox.Show("Save Successful");
                }
                else
                {
                    // if 'No' do something here
                    System.Windows.Forms.MessageBox.Show("Level Changes Discarded!");
                }
                AddScreenAndChill(new LoadLevelScreen());
            }
            if (btnLemClear.IsClicked())
            {
                //Clear the current level that you have been building and go to the level 
                //editor screen with a blank level.
                var confirmResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to clear all objects?", "Confirm Clear!", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (confirmResult.ToString() == "Yes")
                {
                    //clear the levlel of all object
                    this.playingLevel.clearAllObjects(); 
                    //clear the undo/redo history
                    CommandManager commandManager = CommandManager.getInstance();
                    commandManager.clearUndoRedo();

                }
                else
                {
                    
                }
                AddScreenAndChill(new LevelEditScreen());
            }
            if (btnLemExit.IsClicked())
            {
                var confirmResult = System.Windows.Forms.MessageBox.Show("Do you want to save the level before exiting?", "Confirm Save!", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (confirmResult.ToString() == "Yes")
                {
                    // if 'Yes' do something here 
                    GameManager.GlobalInstance.LevelNames.addFilename(GameManager.GlobalInstance.activeLevel.levelName);
                    var sLevel = new SerializableLevel(GameManager.GlobalInstance.activeLevel);
                    new SaveLevel().initiateSave(sLevel);
                    System.Windows.Forms.MessageBox.Show("Save Successful");
                }
                else
                {
                    // if 'No' do something here
                    System.Windows.Forms.MessageBox.Show("Level Changes Discarded!");
                }
                ScreenManager.Exit();
            }
            btnLemSave.Update(MouseState);
            btnLemLoad.Update(MouseState);
            btnLemExit.Update(MouseState);
            btnLemClear.Update(MouseState);
            btnLemBack.Update(MouseState);

        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(levelEditorMenuBackground, levelEditorMenuPosition, Color.White);
            spriteBatch.Draw(levelEditorMenuTitle, levelEditorMenuGraphicPosition, Color.White);
            btnLemBack.Draw(spriteBatch);
            btnLemClear.Draw(spriteBatch);
            btnLemExit.Draw(spriteBatch);
            btnLemLoad.Draw(spriteBatch);
            btnLemSave.Draw(spriteBatch);
            if (wasSaveSuccess == true)
            {
                System.Windows.Forms.MessageBox.Show("Save Successful");
                wasSaveSuccess = false;
                switchToLoad = true;
            }
        }

    }
}
