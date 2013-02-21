using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Stunt_Plane_V2K.Screens
{
    class BackgroundScreen : GameLibrary.Screen
    {
        private ContentManager content;

        private SpriteBatch SpriteBatch;
        private Viewport viewport;
        private Rectangle fullscreen;

        private Texture2D bg;
        private Texture2D bg2;
        private Texture2D bg3;

        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Game.Services, "Content");

            SpriteBatch = ScreenManager.SpriteBatch;
            viewport = ScreenManager.GraphicsDevice.Viewport;
            fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);


            bg = content.Load<Texture2D>("Sprites/background");
            bg2 = content.Load<Texture2D>("Sprites/middleground");
            bg3 = content.Load<Texture2D>("Sprites/foreground");
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            SpriteBatch.Draw(bg, fullscreen, null, Color.Gray, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            SpriteBatch.Draw(bg2, fullscreen, null, Color.Gray, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            SpriteBatch.Draw(bg3, fullscreen, null, Color.Gray, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

            SpriteBatch.End();
        }
    }
}
