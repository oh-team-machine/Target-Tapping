using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AHS_Touch_Game.Screens;
using AHS_Touch_Game;

namespace TargetTapping
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TargetTappingGame : Microsoft.Xna.Framework.Game
    {
        private GameManager manager;

        private GraphicsDeviceManager graphics;

        public TargetTappingGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

#if DEBUG
            graphics.IsFullScreen = false;
#else
            graphics.IsFullScreen = true;
#endif

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

            Components.Add(manager);

#if FOR_TOUCHSCREEN
            manager.Input.InitializeTouch(this.Window.Handle);
#endif

            MenuScreen ms = new MenuScreen();

            manager.AddScreen(ms, true);
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
