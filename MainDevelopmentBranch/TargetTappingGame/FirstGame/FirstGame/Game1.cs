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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            HomeScreen,
            NewLevelScreen,
            LoadLevelScreen,
            LevelEditor,
            PatientGame,
        }
        GameState CurrentGameState = GameState.HomeScreen;

        Texture2D myTopHeaderBkGround;
        Vector2 myTopHeaderPosition = Vector2.Zero; //example code
        Texture2D myTitle, myNewLevelTitle;
        Vector2 myTitlePosition = (new Vector2(300, 0));
        Vector2 myNewLevelTitlePosition = (new Vector2(300, 0));
        Vector2 myCancelButtonPosition = (new Vector2(0, 0));
        
        
        int screenWidth = 1280, screenHeight = 720;
        cButton btnNew, btnLoad, btnExit;
        cButton120x50 btnCancel;
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
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // myTexture = Content.Load<Texture2D>("Sprites/hockey-puck"); //example code
            // TODO: use this.Content to load your game content here
            myTopHeaderBkGround = Content.Load<Texture2D>("GUI/topHeaderBkGround");
            myTitle = Content.Load<Texture2D>("GUI/targetTappingGame");
            btnNew = new cButton(Content.Load<Texture2D>("GUI/newButton"), graphics.GraphicsDevice);
            btnNew.setPosition(new Vector2(340, 200 ));
            btnLoad = new cButton(Content.Load<Texture2D>("GUI/loadButton"), graphics.GraphicsDevice);
            btnLoad.setPosition(new Vector2(340, 350));
            btnExit = new cButton(Content.Load<Texture2D>("GUI/exitButton"), graphics.GraphicsDevice);
            btnExit.setPosition(new Vector2(340, 500));
            btnCancel = new cButton120x50(Content.Load<Texture2D>("GUI/cancel"), graphics.GraphicsDevice);
            myNewLevelTitle = Content.Load<Texture2D>("GUI/newLevel");


            //graphics.ApplyChanges();
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
            MouseState mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (CurrentGameState)
            {
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
                case GameState.NewLevelScreen:
                    if (btnCancel.isClicked == true)
                    {
                        CurrentGameState = GameState.HomeScreen;
                    }
                    btnCancel.Update(mouse);
                    break;
                case GameState.LoadLevelScreen:

                    break;
                case GameState.LevelEditor:

                    break;
                case GameState.PatientGame:

                    break;

            }
            // TODO: Add your update logic here
           // UpdateSprite(gameTime); //example code
            base.Update(gameTime);
        }
        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Begin();
            spriteBatch.Draw(myTopHeaderBkGround, myTopHeaderPosition, Color.White); //example code
            switch (CurrentGameState)
            {
                case GameState.HomeScreen:
                    btnNew.Draw(spriteBatch);
                    btnLoad.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                    spriteBatch.Draw(myTitle, myTitlePosition, Color.White);

                    break;
                case GameState.NewLevelScreen:
                    btnCancel.Draw(spriteBatch);
                    spriteBatch.Draw(myNewLevelTitle, myNewLevelTitlePosition, Color.White);
                    break;
                case GameState.LoadLevelScreen:

                    break;
                case GameState.LevelEditor:

                    break;
                case GameState.PatientGame:

                    break;

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
