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
using FirstGame.Front_end;

namespace FirstGame
{
    /// <summary>
    /// This is the main type for your game. 
    /// We may want to keep all GUI in here and define classes and methods to act
    /// upon those GUI elements elsewhere. This way we can change the entire GUI
    /// within one file. Which we will need to do to adapt to the final screen size.
    /// We will not use 1920x1080 until the end to help in laptop coding.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth = 1280, screenHeight = 720;
        enum GameState
        {
            HomeScreen,
            NewLevelScreen,
            LoadLevelScreen,
            LevelEditor,
            PatientGame,
        }
        GameState CurrentGameState = GameState.HomeScreen;
        bool multiState = false;
        //Initializing Graphical Elements
        Texture2D myTopHeaderBkGround;
        Vector2 myTopHeaderPosition = Vector2.Zero; //example code
        Texture2D myTitle, myNewLevelTitle, myNewLevel, myName, myDescription, myLoadLevelTitle;
        Vector2 myNewLevelPosition = (new Vector2(450, 0));
        Vector2 myLoadLevelTitlePosition = (new Vector2(520, 0));
        Vector2 myNamePosition = (new Vector2(0, 130));
        Vector2 myDescriptionPosition = (new Vector2(0, 200));
        Vector2 myTitlePosition = (new Vector2(300, 0));
        Vector2 myNewLevelTitlePosition = (new Vector2(500, 0));
        Vector2 myCancelButtonPosition = (new Vector2(0, 0));
        //Initialize Button Elements (There are different Sizes of Buttons)
        cButton btnNew, btnLoad, btnExit;
        cButton120x50 btnCancel, btnCreate, btnOpen;
        cButton120x55 btnUpTime, btnHoldTime;
        cButton55x55 btnHome, btnMenu, btnMultiple, btnPlay, btnRedo, btnUndo, btnMoreUp, btnLessUp, btnMoreHold, btnLessHold;
        
        //CREATE GAME CONSTRUCTOR//
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = screenWidth; //currently set to 720P for laptop support, will change to 1080P
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        //LOAD CONTENT HERE//
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // myTexture = Content.Load<Texture2D>("Sprites/hockey-puck"); //example code
            // TODO: use this.Content to load your game content here
            myTopHeaderBkGround = Content.Load<Texture2D>("GUI/topHeaderBkGround");
            
            //Home Screen Elements
            myTitle = Content.Load<Texture2D>("GUI/targetTappingGame");
            btnNew = new cButton(Content.Load<Texture2D>("GUI/newButton"), graphics.GraphicsDevice);
            btnNew.setPosition(new Vector2(340, 200 ));
            btnLoad = new cButton(Content.Load<Texture2D>("GUI/loadButton"), graphics.GraphicsDevice);
            btnLoad.setPosition(new Vector2(340, 350));
            btnExit = new cButton(Content.Load<Texture2D>("GUI/exitButton"), graphics.GraphicsDevice);
            btnExit.setPosition(new Vector2(340, 500));
            
            //NewLevel Screen Elements
            btnCancel = new cButton120x50(Content.Load<Texture2D>("GUI/cancel"), graphics.GraphicsDevice);
            myNewLevelTitle = Content.Load<Texture2D>("GUI/newLevel");
            myNewLevel = Content.Load<Texture2D>("GUI/newLevel");
            btnCreate = new cButton120x50(Content.Load<Texture2D>("GUI/createButton"), graphics.GraphicsDevice);
            btnCreate.setPosition(new Vector2(1160, 0));
            myName = Content.Load<Texture2D>("GUI/name");
            myDescription = Content.Load<Texture2D>("GUI/description");

            //LoadGame Screen Elements
            //btnCancel = new cButton120x50(Content.Load<Texture2D>("GUI/cancel"), graphics.GraphicsDevice);// Place Holder
            myLoadLevelTitle = Content.Load<Texture2D>("GUI/loadGameTitle");
            btnOpen = new cButton120x50(Content.Load<Texture2D>("GUI/openButton"), graphics.GraphicsDevice);
            btnOpen.setPosition(new Vector2(1160, 0));

            //Level Editor Elements
            btnHome = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/homeButton"), graphics.GraphicsDevice);
            btnHome.setPosition(new Vector2(30, 30));
            btnMenu = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/menuButton"), graphics.GraphicsDevice);
            btnMenu.setPosition(new Vector2(95, 30));
            btnHoldTime = new cButton120x55(Content.Load<Texture2D>("LevelEditorGUI/holdTimeButton"), graphics.GraphicsDevice);
            btnHoldTime.setPosition(new Vector2(700, 30));
            btnMultiple = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/multipleToggleOff"), graphics.GraphicsDevice);
            btnMultiple.setPosition(new Vector2(355, 30));
            btnPlay = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/playButton"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(290, 30));
            btnRedo = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/redoButton"), graphics.GraphicsDevice);
            btnRedo.setPosition(new Vector2(215, 30));
            btnUndo = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/undoButton"), graphics.GraphicsDevice);
            btnUndo.setPosition(new Vector2(160, 30));
            btnUpTime = new cButton120x55(Content.Load<Texture2D>("LevelEditorGUI/upTimeButton"), graphics.GraphicsDevice);
            btnUpTime.setPosition(new Vector2(440, 30));
            btnMoreUp = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/moreButton"), graphics.GraphicsDevice);
            btnMoreUp.setPosition(new Vector2(560, 30));
            btnMoreHold = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/moreButton"), graphics.GraphicsDevice);
            btnMoreHold.setPosition(new Vector2(820, 30));
            btnLessUp = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/lessButton"), graphics.GraphicsDevice);
            btnLessUp.setPosition(new Vector2(615, 30));
            btnLessHold = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/lessButton"), graphics.GraphicsDevice);
            btnLessHold.setPosition(new Vector2(875, 30));
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
        //UPDATE GAME HERE//
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (CurrentGameState)
            {
                    //update if in HOME SCREEN
                case GameState.HomeScreen:
                    if (btnNew.isClicked == true)
                    {
                        CurrentGameState = GameState.NewLevelScreen;
                    }
                    if (btnLoad.isClicked == true)
                    {
                        CurrentGameState = GameState.LoadLevelScreen;
                    }
                    if (btnExit.isClicked == true)
                    {
                        this.Exit();
                    }
                    btnNew.Update(mouse);
                    btnLoad.Update(mouse);
                    btnExit.Update(mouse);
                    break;

                    //update if in NEWLEVEL SCREEN
                case GameState.NewLevelScreen:
                    if (btnCancel.isClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                    }
                    if (btnCreate.isClicked == true)
                    {
                        CurrentGameState = GameState.LevelEditor;
                    }
                    btnCancel.Update(mouse);
                    btnCreate.Update(mouse);

                    break;

                    //update if in LOAD LEVEL SCREEN
                case GameState.LoadLevelScreen:
                    if (btnCancel.isClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                    }
                    if (btnOpen.isClicked == true)
                    {
                        CurrentGameState = GameState.LevelEditor;
                    }
                    btnCancel.Update(mouse);
                    btnOpen.Update(mouse);
                    break;

                    //update if in LEVEL EDITIOR SCREEN
                case GameState.LevelEditor:
                    if (btnHome.isClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;

                        //Call A Method Defined In Another Class
                    }
                    if (btnMenu.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnHoldTime.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnMultiple.isClicked == true)
                    {
                        if (multiState == true)
                        {
                            multiState = false;
                            btnMultiple = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/multipleToggleOff"), graphics.GraphicsDevice);
                             btnMultiple.setPosition(new Vector2(355, 30));
                            btnMultiple.Update(mouse);
                        }
                        else if (multiState == false)
                        {
                            multiState = true;
                            btnMultiple = new cButton55x55(Content.Load<Texture2D>("LevelEditorGUI/multipleToggleOn"), graphics.GraphicsDevice);
                            btnMultiple.setPosition(new Vector2(355, 30));
                            btnMultiple.Update(mouse);

                        }
                        //Call A Method Defined In Another Class
                    }
                    if (btnPlay.isClicked == true)
                    {
                        CurrentGameState = GameState.PatientGame; 
                        //Call A Method Defined In Another Class
                    }
                    if (btnRedo.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnUndo.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnUpTime.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnMoreUp.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnLessUp.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnMoreHold.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    if (btnLessHold.isClicked == true)
                    {
                        //Call A Method Defined In Another Class
                    }
                    btnHome.Update(mouse);
                    btnMenu.Update(mouse);
                    btnHoldTime.Update(mouse);
                    btnMultiple.Update(mouse);
                    btnPlay.Update(mouse);
                    btnRedo.Update(mouse);
                    btnUndo.Update(mouse);
                    btnUpTime.Update(mouse);
                    btnMoreHold.Update(mouse);
                    btnMoreUp.Update(mouse);
                    btnLessHold.Update(mouse);
                    btnLessUp.Update(mouse);
                    break;

                    //update if playing PATIENT GAME
                case GameState.PatientGame:

                    break;

            }
            // TODO: Add your update logic here
           // UpdateSprite(gameTime); //example code
            base.Update(gameTime);
        }
        

        /// <summary>
        /// This is called when the game should DRAW itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //DRAW GAME HERE//
        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Begin();
            spriteBatch.Draw(myTopHeaderBkGround, myTopHeaderPosition, Color.White); //example code
            switch (CurrentGameState)
            {
                case GameState.HomeScreen: //Draw all elements for Home Screen
                    btnNew.Draw(spriteBatch);
                    btnLoad.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                    spriteBatch.Draw(myTitle, myTitlePosition, Color.White);

                    break;
                case GameState.NewLevelScreen: //Draw all elements for NewLevel Screen
                    btnCancel.Draw(spriteBatch);
                    btnCreate.Draw(spriteBatch);
                    spriteBatch.Draw(myNewLevelTitle, myNewLevelTitlePosition, Color.White);
                    spriteBatch.Draw(myName, myNamePosition, Color.White);
                    spriteBatch.Draw(myDescription, myDescriptionPosition, Color.White);
                    break;
                case GameState.LoadLevelScreen: //Draw all elements for Loadlevel Screen
                    btnCancel.Draw(spriteBatch);
                    btnOpen.Draw(spriteBatch);
                    spriteBatch.Draw(myLoadLevelTitle, myLoadLevelTitlePosition, Color.White);
                    break;
                case GameState.LevelEditor:
                    btnHome.Draw(spriteBatch);
                    btnMenu.Draw(spriteBatch);
                    btnHoldTime.Draw(spriteBatch);
                    btnMultiple.Draw(spriteBatch);
                    btnPlay.Draw(spriteBatch);
                    btnRedo.Draw(spriteBatch);
                    btnUndo.Draw(spriteBatch);
                    btnUpTime.Draw(spriteBatch);
                    btnMoreUp.Draw(spriteBatch);
                    btnMoreHold.Draw(spriteBatch);
                    btnLessUp.Draw(spriteBatch);
                    btnLessHold.Draw(spriteBatch);
                    break;
                case GameState.PatientGame:

                    break;

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
