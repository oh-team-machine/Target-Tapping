using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameLibrary;
using MultiTracker;

//TODO: Import the GameLibrary.dll to your folder, or add it to your
//      build path, or add it to your References. It will make all
//      the errors go away.

namespace AHSGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Setup gui;
        PatientDatabase db;

        MultiTracker.CalibrationForm setupGUI;

        MultiTracker.TrackedMarker leftLimb;
        MultiTracker.TrackedMarker rightLimb;

        ArrowButtons arrows;

        KeyboardState keyState;

        bool bConfigChanged;
        bool bResumeFromPause;
        bool bSendExit;
        bool bWin;
        bool bCursorVisible;

        float timeInGame;
        float trialTime;

        int minutes;
        int seconds;

        int score;
        int repetitions;

        Texture2D cursor;
        Texture2D startButton;
        Texture2D background;

        List<Texture2D> arrowTextures;
        List<Texture2D> activeArrowTextures;

        SpriteFont stepperFont;

        Vector2 fontPosition;

        Rectangle cursorRect;
        Rectangle startRect;

        uint[] cursorCollision;
        uint[] startCollision;

        #endregion Variables

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            User32.SetWindowPos((uint)this.Window.Handle, 1, -2000, 50,
             graphics.PreferredBackBufferWidth,
             graphics.PreferredBackBufferHeight, 0);

            //TODO: Add gamename and database name here.
            //      eg.If game is Mountain, database is MountainDB.
            db = new PatientDatabase("Step Master", "Step MasterDB");

            db.SetDesktopLocation(-800, 250);
            db.Show();

            gui = new Setup();
            gui.SetDesktopLocation(-800, 250);

            setupGUI = new MultiTracker.CalibrationForm();

            bConfigChanged = true;
            bResumeFromPause = true;
            bSendExit = false;

            timeInGame = 0.0f;

            arrowTextures = new List<Texture2D>();
            activeArrowTextures = new List<Texture2D>();

            fontPosition = new Vector2(630, 700);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: add the location of your cursor texture
            cursor = Content.Load<Texture2D>("Sprites/Footprint Cursor");
            startButton = Content.Load<Texture2D>("Sprites/Start Button");
            background = Content.Load<Texture2D>("Sprites/Wood Floor");

            leftLimb = new MultiTracker.TrackedMarker(Content.Load<Texture2D>("Sprites/Left Foot"), 0, Vector3.Zero);
            rightLimb = new MultiTracker.TrackedMarker(Content.Load<Texture2D>("Sprites/Right Foot"), 1, Vector3.Zero);

            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Up Arrow"));
            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Up Right Arrow"));
            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Right Arrow"));
            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Down Right Arrow"));
            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Down Arrow"));
            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Down Left Arrow"));
            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Left Arrow"));
            arrowTextures.Add(Content.Load<Texture2D>("Sprites/Up Left Arrow"));

            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Up Arrow"));
            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Up Right Arrow"));
            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Right Arrow"));
            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Down Right Arrow"));
            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Down Arrow"));
            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Down Left Arrow"));
            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Left Arrow"));
            activeArrowTextures.Add(Content.Load<Texture2D>("Sprites/Active Up Left Arrow"));
            // TODO: use this.Content to load your game content here

            cursorRect = new Rectangle(0, 0, cursor.Width, cursor.Height);
            startRect = new Rectangle( 565, 325, startButton.Width, startButton.Height);
            //TODO: create rectangles for any textures that need collisions

            cursorCollision = new uint[cursor.Width * cursor.Height];
            startCollision = new uint[startButton.Width * startButton.Height];
            //TODO: create uint[] for per pixel collisions

            cursor.GetData<uint>(cursorCollision);
            startButton.GetData<uint>(startCollision);
            //TODO: populate uint[] with pixel data

            stepperFont = Content.Load<SpriteFont>("StepperFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            //Allows the game to exit
            if (keyState.IsKeyDown(Keys.Escape))
            {
                GameLibrary.Helpful_Methods.sendExit();
                this.Exit();
            }

            //Checks to make sure a patient has been chosen from the database
            if (db.isReady())
            {
                //Allows the game to pause and access the menu
                if (keyState.IsKeyDown(Keys.Space))
                {
                    //Pauses the IR tracking if needed
                    if (bSendExit)
                        GameLibrary.Helpful_Methods.sendExit();

                    this.IsMouseVisible = true;
                    gui.setGameReady(false);
                    gui.Show();
                    bConfigChanged = true;
                    bResumeFromPause = true;
                    bSendExit = false;
                }

                if (setupGUI.bCalibrated)
                {
                    //Checks if the game is configured and ready to run
                    if (gui.isGameReady())
                    {
                        //tracks total time
                        timeInGame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                        if (!bSendExit)
                            bSendExit = true;

                        //Resumes the IR tracking after being paused
                        if (bResumeFromPause)
                        {
                            if (gui.isQuit())
                            {
                                GameLibrary.Helpful_Methods.sendExit();
                                this.Exit();
                            }

                            GameLibrary.Helpful_Methods.sendExit();
                            bResumeFromPause = false;
                        }

                        this.IsMouseVisible = false;

                        //Deal with configuration changes
                        if (bConfigChanged)
                        {
                            bWin = false;
                            bCursorVisible = true;

                            repetitions = gui.repetitions;

                            arrows = new ArrowButtons(arrowTextures, activeArrowTextures, gui.configuration, gui.timeout, gui.simultaneous);

                            score = 0;
                            timeInGame = 0.0f;
                            bConfigChanged = false;
                        }

                        if (!bCursorVisible && !bWin)
                        {
                            leftLimb.update(setupGUI.getRBData(leftLimb.markerNumber));
                            rightLimb.update(setupGUI.getRBData(rightLimb.markerNumber));
                            arrows.update(gameTime);

                            for (int i = 0; i < 8; i++)
                            {
                                if (arrows.isArrowActive(i))
                                {
                                    if (GameLibrary.Collisions.perPixelCollision(leftLimb.getRect(), arrows.getArrowRectangles()[i], leftLimb.markerCollision, arrows.getArrowCollisionBounds()[i]))
                                    {
                                        updateScore();
                                        arrows.setArrowActive(i, false);
                                    }
                                    else if (GameLibrary.Collisions.perPixelCollision(rightLimb.getRect(), arrows.getArrowRectangles()[i], rightLimb.markerCollision, arrows.getArrowCollisionBounds()[i]))
                                    {
                                        updateScore();
                                        arrows.setArrowActive(i, false);
                                    }
                                }
                            }
                        }

                        if (bCursorVisible)
                        {
                            if (gui.handUsed == "Left")
                            {
                                cursorRect.X = (int)setupGUI.getRBData(leftLimb.markerNumber).X;
                                cursorRect.Y = (int)setupGUI.getRBData(leftLimb.markerNumber).Y;
                            }

                            else
                            {
                                cursorRect.X = (int)setupGUI.getRBData(rightLimb.markerNumber).X;
                                cursorRect.Y = (int)setupGUI.getRBData(rightLimb.markerNumber).Y;
                            }

                            //TODO: pass varaibles for the player object rectangle and collision array
                            bCursorVisible = !GameLibrary.Collisions.perPixelCollision(startRect, cursorRect, startCollision, cursorCollision);
                        }

                    }

                    //Shows the setup GUI 
                    else
                    {
                        gui.Show();
                    }
                }
                else
                {
                    setupGUI.Show();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            if (gui.isGameReady())
            {
                if (!bWin)
                {
                    // TODO: Add your drawing code here
                    arrows.draw(spriteBatch);
                    spriteBatch.DrawString(stepperFont, score.ToString() + "/" + repetitions.ToString(), fontPosition, Color.Yellow);
                    if (!bCursorVisible)
                    {
                        leftLimb.draw(spriteBatch);
                        rightLimb.draw(spriteBatch);
                    }

                    else
                    {
                        spriteBatch.Draw(cursor, cursorRect, Color.White);
                        spriteBatch.Draw(startButton, startRect, Color.White);
                    }
                }

                //Add drawing code for when game is won
                else
                {
                    spriteBatch.DrawString(stepperFont, "You Win!!!!!!", new Vector2(340, 350), Color.Yellow, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 1.0f);
                    spriteBatch.DrawString(stepperFont, "Stepping Time: " + GameLibrary.Helpful_Methods.formatTime(trialTime), new Vector2(340, 450), Color.Yellow);
                    spriteBatch.DrawString(stepperFont, "Number of Correct Steps: " + score, new Vector2(340, 500), Color.Yellow);
                    spriteBatch.DrawString(stepperFont, "Number of Missed Steps: " + arrows.getNumMissed(), new Vector2(350, 550), Color.Yellow);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the score. If the score == repetitions, sets the win game
        /// to true.
        /// </summary>
        private void updateScore()
        {
            score++;

            if (score == repetitions)
                winGame();
        }

        /// <summary>
        /// Functionality for what should happen when the game is won.
        /// </summary>
        private void winGame()
        {
            List<String> data = new List<string>();
            List<int> time = GameLibrary.Helpful_Methods.calculateTime(timeInGame);

            trialTime = timeInGame;

            bWin = true;
            minutes = time[0];
            seconds = time[1];

            data.Add(System.DateTime.Now.ToString());

            data.Add("Hand Used: " + gui.handUsed);
            //Add any tracked data here
            data.Add("Timeout: " + gui.timeout);
            data.Add("Simultaneous Directions: " + gui.simultaneous);
            data.Add("Configuration: " + gui.configuration);
            data.Add("Repetitions: " + gui.repetitions);
            data.Add("Missed: " + arrows.getNumMissed());

            if (seconds < 10)
                data.Add("Time: " + minutes.ToString() + ":0" + seconds.ToString());
            else
                data.Add("Time: " + minutes.ToString() + ":" + seconds.ToString());
            data.Add("");

            db.saveSessionData(data);
        }
    }
}
