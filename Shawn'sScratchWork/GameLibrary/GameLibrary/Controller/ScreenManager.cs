using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        private static Vector2 baseScreenDimensions = new Vector2(1920.0f, 1080.0f);

        /// <summary>
        /// 
        /// </summary>
        public InputManager Input { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SpriteBatch SpriteBatch { get; set; }

        List<Screen> screens;
        List<Screen> tempScreens;

        GraphicsDevice device;

        Matrix screenTransform;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public ScreenManager(Game game)
            : base(game)
        {
            screens = new List<Screen>();
            tempScreens = new List<Screen>();

            Input = new InputManager(new Vector2(Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height));
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            device = Game.GraphicsDevice;

            screenTransform = Matrix.CreateScale(1);

            foreach (Screen temp in screens)
            {
                temp.LoadContent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();

            foreach (Screen temp in screens)
            {
                temp.UnloadContent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Input.Update();

            tempScreens.Clear();

            foreach (Screen copy in screens)
            {
                tempScreens.Add(copy);
            }

            foreach (Screen temp in tempScreens)
            {
                if (temp.ScreenState == State.Visible)
                {
                    temp.Update(gameTime);
                    temp.HandleInput(gameTime, Input);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (Screen temp in screens)
            {
                if (temp.ScreenState == State.Hidden)
                {
                    continue;
                }

                temp.Draw(gameTime, screenTransform);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="isInitialized"></param>
        public void AddScreen(Screen screen, bool isInitialized)
        {
            screen.ScreenManager = this;

            if (!isInitialized)
            {
                screen.Initialize();
                screen.LoadContent();
            }

            screens.Add(screen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screen"></param>
        public void RemoveScreen(Screen screen)
        {
            screens.Remove(screen);

            screens[screens.Count - 1].ScreenState = State.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit()
        {
            this.UnloadContent();
            base.Game.Exit();
        }

        /// <summary>
        /// Takes a hardcoded X position and returns a scaled position based on screen size and preffered screen size.
        /// </summary>
        /// <param name="position">The unscaled X position</param>
        /// <returns>the scaled X position</returns>
        public float ScaleXPosition(float position)
        {
            return (position / baseScreenDimensions.X) * device.Viewport.Width;
        }

        /// <summary>
        /// Takes a hardcoded Y position and returns a scaled position based on screen size and preffered screen size.
        /// </summary>
        /// <param name="position">The unscaled Y position</param>
        /// <returns>The scaled Y position</returns>
        public float ScaleYPosition(float position)
        {
            return (position / baseScreenDimensions.Y) * device.Viewport.Height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inverted"></param>
        public void InvertScreen(bool inverted)
        {
            if (inverted)
            {
                screenTransform = GetMatrix(180);
            }
            else
            {
                screenTransform = GetMatrix(0);
            }

            Input.InvertScreen(inverted);
        }

        private Matrix GetMatrix(int degrees)
        {
            Matrix translateToOrigin = Matrix.CreateTranslation(-device.Viewport.Width / 2, -device.Viewport.Height / 2, 0);
            Matrix rotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(degrees));
            Matrix translateBackToPosition = Matrix.CreateTranslation(device.Viewport.Width / 2, device.Viewport.Height / 2, 0);

            float horScaling = (float)device.PresentationParameters.BackBufferWidth / device.Viewport.Width;
            float verScaling = (float)device.PresentationParameters.BackBufferHeight / device.Viewport.Height;
            Vector3 screenScalingFactor = new Vector3(horScaling, verScaling, 1);

            Matrix scaleMatrix = Matrix.CreateScale(screenScalingFactor);

            Matrix compositeMatrix = translateToOrigin * rotationMatrix * translateBackToPosition * scaleMatrix;

            return compositeMatrix;
        }
    }
}
