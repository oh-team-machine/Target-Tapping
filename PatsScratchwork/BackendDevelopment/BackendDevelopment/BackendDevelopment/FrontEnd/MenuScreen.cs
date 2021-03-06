﻿using System.Windows.Forms;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BackendDevelopment.BackEnd;
using BackendDevelopment.FrontEnd;

namespace BackendDevelopment.FrontEnd
{
    class MenuScreen : GameLibrary.Screen
    {
        private ContentManager content;

        private SpriteBatch SpriteBatch;

        private Texture2D menuBackground;
        private Rectangle menuRect;

        private GameLibrary.UI.Button exitButton;
        private GameLibrary.UI.Button patientButton;
        private GameLibrary.UI.Button optionsButton;
        
        //private GraphicsDeviceManager Graphics;
        ////constructor
        //public MenuScreen(GraphicsDeviceManager graphics)
        //{
        //    this.Graphics = graphics;
        //}
                    
        /// <summary>
        /// 
        /// </summary>
        public override void LoadContent()
        {
            //DrawShape draw = new DrawShape();
            //Texture2D circleOverlay = draw.drawShape("Circle", 20, this.Graphics);

            this.content = new ContentManager(ScreenManager.Game.Services, "Content");

            this.menuBackground = this.content.Load<Texture2D>("Sprites/MenuBackground");

            //Buttons are 300 x 75 but lets change it to have the drawShape method make the shape for us
            DrawShape mydraw = new DrawShape();
            //Texture2D exitText = mydraw.drawShape("Square", 100, this.Graphics);
            //Texture2D patientText = mydraw.drawShape("Circle", 100, this.Graphics);
            //Texture2D optionsText = mydraw.drawShape("Rectanlge", 100, this.Graphics);

            Texture2D exitText = content.Load<Texture2D>("GUI/ExitButton");
            Texture2D patientText = content.Load<Texture2D>("GUI/PatientButton");
            Texture2D optionsText = content.Load<Texture2D>("GUI/OptionsButton");

            int menuX = (ScreenManager.GraphicsDevice.Viewport.Width / 2) - (menuBackground.Width / 2);
            int menuY = (ScreenManager.GraphicsDevice.Viewport.Height / 2) - (menuBackground.Height / 2);

            menuRect = new Rectangle(menuX, menuY, menuBackground.Width, menuBackground.Height);

            int buttonSpace = 75;
            int buttonX = menuX + (menuBackground.Width / 2) - 150;
            int buttonY = menuY + buttonSpace;

            Rectangle patientRect = new Rectangle(buttonX, buttonY, patientText.Width, patientText.Height);
            Rectangle optionsRect = new Rectangle(buttonX, patientRect.Y + buttonSpace + patientText.Height, optionsText.Width, optionsText.Height);
            Rectangle exitRect = new Rectangle(buttonX, optionsRect.Y + buttonSpace + exitText.Height, exitText.Width, exitText.Height);

            exitButton = new GameLibrary.UI.Button(exitText, exitRect);
            patientButton = new GameLibrary.UI.Button(patientText, patientRect);
            optionsButton = new GameLibrary.UI.Button(optionsText, optionsRect);

            
        }

        public override void UnloadContent()
        {
            this.content.Unload();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //if (exitButton.IsClicked())
            //{
            //    ScreenManager.Exit();
            //}

            //else if (patientButton.IsClicked())
            //{

            //    //PatientScreen ps = new PatientScreen();

            //    // ScreenManager.AddScreen(ps, false);

            //    //this.ScreenState = State.Hidden;

            //    Cursor.Current = Cursors.WaitCursor;
            //}

            //else if (optionsButton.IsClicked())
            //{


            //    //OptionsScreen os = new OptionsScreen();

            //    // ScreenManager.AddScreen(os, false);

            //    // this.ScreenState = State.Hidden;

            //    Cursor.Current = Cursors.WaitCursor;
            //}

            //base.Update(gameTime);
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            MouseState MouseState = input.MouseState;

            //exitButton.Update(MouseState);
            //patientButton.Update(MouseState);
            //optionsButton.Update(MouseState);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch = ScreenManager.SpriteBatch;

           // SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

           // SpriteBatch.Draw(menuBackground, menuRect, Color.Blue);

            //exitButton.Draw(SpriteBatch);
            //patientButton.Draw(SpriteBatch);
            //optionsButton.Draw(SpriteBatch);
        

            //SpriteBatch.End();
        }
        
    }
}
