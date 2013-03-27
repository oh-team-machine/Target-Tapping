using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public const string ContentDir = "Content";


        protected RichContentManager Content;
        protected MouseState MouseState;
        protected readonly int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        protected readonly int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        /// IMPORTANT! THIS *MUST* BE CALLED BEFORE ANYTHING HAPPENS
        /// IN THE OVERRIDEN LoadContent. That is, this:
        ///    base.LoadContent()
        /// should always be the first line.
        public override void LoadContent()
        {
            Content = new RichContentManager(ScreenManager.Game.Services, ContentDir);
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            MouseState = input.MouseState;

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
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            PreparedDraw(spriteBatch);

            spriteBatch.End();
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
            return Content.MakeButton(x, y, resourceName);
        }
    }
}
