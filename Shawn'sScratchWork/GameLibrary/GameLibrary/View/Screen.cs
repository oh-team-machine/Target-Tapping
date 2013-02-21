using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameLibrary
{
    /// <summary>
    /// Enum for the state of the Screen, currently either Visible or Hidden.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Marks the screen as visible, allowing it to be updated and drawn.
        /// </summary>
        Visible,
        /// <summary>
        /// Marks the screen as hidden, disallowing it to be updated and drawn.
        /// </summary>
        Hidden
    }

    /// <summary>
    /// This clss represents a screen. It has methods for Initialization, loading and unloading
    /// content, updating, drawing, and handling input. It requires a ScreenManager to look after
    /// it and to call its functions. Anything that should be drawn to the screen in XNA should
    /// reside in one screen or another.
    /// 
    /// This class is an interface.
    /// </summary>
    public abstract class Screen
    {
        /// <summary>
        /// The current state of the screen. One of the enum State.
        /// </summary>
        public State ScreenState { get; set; }

        /// <summary>
        /// The screen manager that owns this screen.
        /// </summary>
        public ScreenManager ScreenManager { get; set; }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        virtual public void Initialize(){}

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        abstract public void LoadContent();

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        abstract public void UnloadContent();

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        virtual public void Update(GameTime gameTime){}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="transform">A matrix representing the screen transformation for scaling and drawing purposes.</param>
        abstract public void Draw(GameTime gameTime, Matrix transform);

        /// <summary>
        /// Allows the screen to accept and handle input.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="input">The input manager that handles the different hardware inputs.</param>
        virtual public void HandleInput(GameTime gameTime, InputManager input) {}
    }
}
