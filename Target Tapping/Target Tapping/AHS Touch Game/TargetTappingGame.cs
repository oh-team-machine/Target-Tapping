using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.Screens;

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

            graphics = new GraphicsDeviceManager(this);
           

            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = screenResolution.Width;
            graphics.PreferredBackBufferHeight = screenResolution.Height;

            graphics.IsFullScreen = isFullScreen;

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
            this.manager.graphics = graphics;

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
