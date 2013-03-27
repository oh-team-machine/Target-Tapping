using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using GameLibrary.UI;
using TargetTapping.Back_end;

namespace TargetTapping.Screens
{
    class GameScreen : AbstractRichScreen
    {
        #region Variables

        //Stuff to draw objects on screen in order.
        int time = 0;
        int currentListNumber = 0;

        // Stuff 'em in here, boss!
        Button btnPause, btnTouchToStart;

        //Here were going to get the current level that we built in the leveleditor
        Level playingLevel = GameManager.GlobalInstance.activeLevel;

        //keep track of the score
        private int score;
        //the font to prit the score too
        private SpriteFont font;
        //position to put the score at
        private Vector2 scorePosition;

        //This boolean say whether the game has been started
        private bool hasTouchedToStart = false;

        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        #endregion Variables

        public override void LoadContent()
        {
            base.LoadContent();
            btnTouchToStart = MakeButton(((screenWidth / 2) - 100), ((screenHeight / 2) - 100), "GameScreenContent/touchToStart");

            btnPause = MakeButton(0, 0, "GUI/pauseButton");
            //initialize the score
            this.score = 0;
            //load the spritefount to print the scroe to
            this.font = Content.Load<SpriteFont>("Font");

            float scoreLength = (font.MeasureString("999/999")).X;
            //double check this position
            scorePosition = new Vector2(this.ScreenManager.ScaleXPosition((this.ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth / 2.0f) - (scoreLength / 2.0f)), this.ScreenManager.ScaleYPosition(20.0f));

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (hasTouchedToStart == false)
            {

            }
            else 
            {
                if (time == (playingLevel.upTime * 60))
                {
                    if (currentListNumber == playingLevel.objectList.Count - 1)
                    {
                        foreach (var myListofObjects in playingLevel.objectList)
                        {
                            foreach (var myObject in myListofObjects)
                            {
                                myObject.shouldIbeDrawn = true;
                            }
                        }
                        //TO DO: add end level screen.
                    }
                    else
                    {
                        currentListNumber = currentListNumber + 1;
                    }
                    time = 0;
                }
                else
                {
                    time = time + 1;
                }
            }
            
            //Touch to Start functionality.
            if (btnTouchToStart.IsClicked())
            {
                hasTouchedToStart = true;
                //Start Timer here and all game events
            }
            if (hasTouchedToStart == false)
            {
                btnTouchToStart.Update(MouseState);
            }

            //This all happens after touch to start!
            if (hasTouchedToStart == true)
            {
                if (btnPause.IsClicked())
                {
                    AddScreenAndChill(new PauseScreen());
                }
                btnPause.Update(MouseState);


                foreach (var myObject in playingLevel.objectList[currentListNumber])
                {
                    // Update the state of all objects that have been brought over from the leveleditor screen
                    myObject.Update(MouseState);
                }
                // This foreach loop will check if a button in the list of buttonlists
                // is clicked and if it is then we are going to move its position.
                foreach (var myObject in playingLevel.objectList[currentListNumber])
                {
                    if (myObject.IsClicked())
                    {
                        this.score++;
                        //now were going to set the current clicked object on the gamescree 
                        //to have its property shouldIbeDrawn = false. Because its been clicked
                        //Lets also update the score
                        //playingLevel.objectList[currentListNumber][position].shouldIbeDrawn = false;
                        myObject.shouldIbeDrawn = false;
                    }
                }
            }

            base.Update(gameTime);
        }


        public override void PreparedDraw(SpriteBatch spriteBatch)
        {
            if (hasTouchedToStart == false)
            {
                btnTouchToStart.Draw(spriteBatch);
            }
            else
            {

                btnPause.Draw(spriteBatch);

                // draw all objects that were created 
                foreach (var myObject in playingLevel.objectList[currentListNumber])
                {
                    myObject.Draw(spriteBatch);
                }
                //SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);
                spriteBatch.DrawString(font, "Score: " + score.ToString(), scorePosition, Color.White);
                //SpriteBatch.End();

            }
        }
    }
}