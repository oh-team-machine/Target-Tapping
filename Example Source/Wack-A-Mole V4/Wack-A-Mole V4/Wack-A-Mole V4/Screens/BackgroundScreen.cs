using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Wack_A_Mole_V4.Screens
{
    class BackgroundScreen : GameLibrary.Screen
    {
        private ContentManager content;

        private Texture2D bg;

        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Game.Services, "Content");

            bg = content.Load<Texture2D>("Sprites/WhackAMoleBackground");
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch SpriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            SpriteBatch.Draw(bg, fullscreen, null, Color.Gray, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

            SpriteBatch.End();
        }
    }
}
