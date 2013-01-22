using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stunt_Plane_V2K.Screens;

namespace Stunt_Plane_V2K
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Manager manager;

        private GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            graphics.IsFullScreen = true;

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
            manager = new Manager(this);

            Components.Add(manager);

            manager.Input.InitializeKinect();

            BackgroundScreen bgs = new BackgroundScreen();
            MenuScreen ms = new MenuScreen();

            manager.AddScreen(bgs, true);
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
