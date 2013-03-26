using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.Screens;
using TargetTapping.Back_end;
using System;

namespace TargetTapping
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TargetTappingGame : Microsoft.Xna.Framework.Game
    {
        private GameManager manager;
        private GraphicsDeviceManager graphics;

	// Resolution of the screen. For debug, will fit our laptop screens.
#if DEBUG
	private Rectangle screenResolution = new Rectangle(0, 0, 1280, 720);
        private bool isFullScreen = false;
#else
	// For release, will fit the 1080p display.
	private Rectangle screenResolution = new Rectangle(0, 0, 1920, 1080);
        private bool isFullScreen = true;
#endif

        public TargetTappingGame()
        {
            //Shawn is using this for setting up new resolution.
            graphics = new GraphicsDeviceManager(this);
            screenResolution = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            //


            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = screenResolution.Width;
            graphics.PreferredBackBufferHeight = screenResolution.Height;

            graphics.IsFullScreen = isFullScreen;
            //Shawn's editing
            graphics.IsFullScreen = false;// full screen turned off as there are to many bugs to test in full
            //


            graphics.ApplyChanges();

            Cursor.Show();

            InitializeGame();
        }

        /// <summary>
        /// This initializes the game. It creates the manager for the game and adds the menu screen
        /// and the background screen.
        /// </summary>
        private void InitializeGame()
        {
            manager = new GameManager(this);
            this.manager.Graphics = graphics;

            Components.Add(manager);

#if FOR_TOUCHSCREEN
            manager.Input.InitializeTouch(this.Window.Handle);
#endif

            var back = new BackgroundScreen(screenResolution);

	    // Load the first screen. THE MENU SCREEN!
            var ms = new MenuScreen();

            manager.AddScreen(back, true);
            manager.AddScreen(ms, false);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
        }
        
    }
}
