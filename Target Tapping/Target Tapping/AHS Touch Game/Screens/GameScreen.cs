using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AHS_Touch_Game.Screens
{
    class GameScreen : GameLibrary.Screen
    {
        #region Variables

        private ContentManager Content;
        private SpriteBatch SpriteBatch;
        private MouseState state;

        private Texture2D bg;
        private Rectangle fullscreen;

        #endregion Variables

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            state = input.MouseState;

            base.HandleInput(gameTime, input);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, transform);

            SpriteBatch.Draw(bg, fullscreen, Color.White);

            SpriteBatch.End();
        }
    }
}