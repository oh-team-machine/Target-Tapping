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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GameLibrary;
using Wack_A_Mole_V4.Screens;

namespace Wack_A_Mole_V4
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

            manager.Input.InitializeTouch(this.Window.Handle);

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
