using System;
using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AHS_Touch_Game.Screens
{
    class OptionsScreen : GameLibrary.Screen
    {
        private ContentManager content;

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            MouseState mouseState = input.MouseState;

            base.HandleInput(gameTime, input);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch SpriteBatch = ScreenManager.SpriteBatch;

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);


            SpriteBatch.End();
        }
    }
}