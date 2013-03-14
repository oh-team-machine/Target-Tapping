using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TargetTapping.FrontEnd;

namespace TargetTapping.Screens
{

    /// <summary>
    /// A RichScreen does a bunch of things that most screens do.
    /// </summary>
    /// Including loading the content manager, getting the mouse state,
    /// setting up the SpriteBatch on Draw, etc.
    /// Also, contains fun utility funcitons! Yay!
    abstract class AbstractRichScreen : GameLibrary.Screen
    {
        /// The directory for finding content.
        public const string CONTENT_DIR = "Content";


        protected RichContentManager content;
        protected MouseState mouseState;

        /// IMPORTANT! THIS *MUST* BE CALLED BEFORE ANYTHING HAPPENS
        /// IN THE OVERRIDEN LoadContent. That is, this:
        ///    base.LoadContent()
        /// should always be the first line.
        public override void LoadContent()
        {
            content = new RichContentManager(ScreenManager.Game.Services, CONTENT_DIR);
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            mouseState = input.MouseState;

            base.HandleInput(gameTime, input);
        }

        /// <summary>
        /// Use the base class when you want to draw things in a SpriteBatch,
        /// but don't want to manually setup the SpriteBatch.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="transform"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch SpriteBatch = ScreenManager.SpriteBatch;

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            PreparedDraw(SpriteBatch);

            SpriteBatch.End();
        }

        /// <summary>
        ///  This makes it unncessary to Begin/End the SpriteBatch. 
        ///  This gets called by the Draw method, inside the SpriteBatch create.
        /// </summary>
        abstract public void PreparedDraw(SpriteBatch spriteBatch);

        // UTILITY FUNCTIONS!

        /// <summary>
        /// Adds the screen to the manager and chills out.
        /// </summary>
        /// <param name="screen"></param>
        /// (That is, it stops redrawing and taking input).
        protected void AddScreenAndChill(GameLibrary.Screen screen)
        {
            ScreenManager.AddScreen(screen, false);
            ScreenState = GameLibrary.State.Hidden;
        }

        protected GameLibrary.UI.Button MakeButton(int x, int y, string resourceName)
        {
            return content.MakeButton(x, y, resourceName);
        }
    }
}
