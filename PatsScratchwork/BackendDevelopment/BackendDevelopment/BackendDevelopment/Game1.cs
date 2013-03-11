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
using BackendDevelopment.BackEnd;
using BackendDevelopment.FrontEnd;
using BackendDevelopment.UIControls;

namespace BackendDevelopment
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        //private Manager manager;
        public GraphicsDeviceManager graphics;
        private SpriteBatch SpriteBatch;

        //declare my button list
        List<Button> buttonList;

        private Texture2D menuBackground;
        private Rectangle menuRect;

        private Button exitButton;
        private Button patientButton;
        private Button optionsButton;

        //The screen size will change to 1080P at the end
        private int screenWidth = 1200, screenHeight = 800;

        //private ContentManager content;
        InputManager myManager = new InputManager(new Vector2(1200, 800));

        public Game1()
        {
            //initialize the graphics with this class as the parameter
            this.graphics = new GraphicsDeviceManager(this);
            //set root directoy for sprites
            Content.RootDirectory = "Content";

            //increaste the window size
            this.graphics.PreferredBackBufferWidth = screenWidth; //currently set to 720P for laptop support, will change to 1080P
            this.graphics.PreferredBackBufferHeight = screenHeight;
            this.graphics.IsFullScreen = false;
            this.graphics.ApplyChanges();
            
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
            //show the mouse
            this.IsMouseVisible = true;
           
            //this.manager = new Manager(this);

            //Components.Add(this.manager);

           // BackgroundScreen bgs = new BackgroundScreen();
            //MenuScreen ms = new MenuScreen();

            //manager.AddScreen(bgs, true);
            base.Initialize();
        }
        protected override void LoadContent(){
             // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);


            //make a button list

            buttonList = new List<Button>();



            //DrawShape draw = new DrawShape();
            //Texture2D circleOverlay = draw.drawShape("Circle", 20, this.Graphics);

            //this.content = new ContentManager(ScreenManager.Game.Services, "Content");

           // this.menuBackground = Content.Load<Texture2D>("Sprites/MenuBackground");

            //Buttons are 300 x 75 but lets change it to have the drawShape method make the shape for us
            DrawShape mydraw = new DrawShape();
            Texture2D exitText = mydraw.drawShape("Circle", 100, graphics);
            Texture2D patientText = mydraw.drawShape("Square", 100, graphics);
           // Texture2D optionsText = mydraw.drawShape("Rectanlge", 100, graphics);

            //Texture2D exitText = Content.Load<Texture2D>("Sprites/ExitButton");
            //Texture2D patientText = Content.Load<Texture2D>("Sprites/PatientButton");
            //Texture2D optionsText = Content.Load<Texture2D>("Sprites/OptionsButton");

            //int menuX = (GraphicsDevice.Viewport.Width / 2) - (menuBackground.Width / 2);
            //int menuY = (GraphicsDevice.Viewport.Height / 2) - (menuBackground.Height / 2);

            //menuRect = new Rectangle(menuX, menuY, menuBackground.Width, menuBackground.Height);

            //int buttonSpace = 75;
           // int buttonX = menuX + (menuBackground.Width / 2) - 150;
            //int buttonY = menuY + buttonSpace;

            //Rectangle patientRect = new Rectangle(buttonX, buttonY, patientText.Width, patientText.Height);
           // Rectangle optionsRect = new Rectangle(buttonX, patientRect.Y + buttonSpace + patientText.Height, optionsText.Width, optionsText.Height);
           // Rectangle exitRect = new Rectangle(buttonX, optionsRect.Y + buttonSpace + exitText.Height, exitText.Width, exitText.Height);
            Rectangle exitRect = new Rectangle(100, 500, 100, 100);
            Rectangle patientRect = new Rectangle(100, 100, 100, 100);
            
            exitButton = new Button(exitText, exitRect);
            patientButton = new Button(patientText, patientRect);
            //optionsButton = new GameLibrary.UI.Button(optionsText, optionsRect);


        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            //Get the current state of the Mouse
            MouseState aMouse = Mouse.GetState();

            exitButton.Update(aMouse);
            patientButton.Update(aMouse);
            //optionsButton.Update(aMouse);

            if (exitButton.IsClicked())
            {
                DrawShape mydraw = new DrawShape();
                Texture2D exitText = mydraw.drawShape("Circle", 100, graphics);
                Rectangle exitRect = new Rectangle(500, 500, 100, 100);
                Button temp = new Button(exitText, exitRect);
                buttonList.Add(temp);
            }

            else if (patientButton.IsClicked())
            {

                DrawShape mydraw = new DrawShape();
                Texture2D exitText = mydraw.drawShape("Square", 10, graphics);
                Rectangle exitRect = new Rectangle(500, 100, 100, 100);
                Button temp = new Button(exitText, exitRect);
                buttonList.Add(temp);
            }

            //else if (optionsButton.IsClicked())
           // {


                //OptionsScreen os = new OptionsScreen();

                // ScreenManager.AddScreen(os, false);

                // this.ScreenState = State.Hidden;

               // Cursor.Current = Cursors.WaitCursor;
           // }
       



            base.Update(gameTime);
        }

        //public void HandleInput(GameTime gameTime, InputManager input)
        //{
        //    MouseState MouseState = input.MouseState;

        //    exitButton.Update(MouseState);
        //    patientButton.Update(MouseState);
        //    //optionsButton.Update(MouseState);
        //}
       

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
      

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.graphics.GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            //SpriteBatch = ScreenManager.SpriteBatch;
            SpriteBatch.Begin();
            //SpriteBatch.Draw(menuBackground, menuRect, Color.Blue);




            exitButton.Draw(SpriteBatch);
            patientButton.Draw(SpriteBatch);
            //optionsButton.Draw(SpriteBatch);


            foreach (Button myBut in buttonList)
	        {
	            myBut.Draw(SpriteBatch);
	        }


            SpriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
