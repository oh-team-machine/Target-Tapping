using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.Screens
{
    class BackgroundScreen : AbstractRichScreen
    {

        private Texture2D Background;
        private Rectangle ScreenSize;

        public BackgroundScreen(Rectangle screenSize)
        {
            ScreenSize = screenSize;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Background = content.Load<Texture2D>("GUI/topHeaderBkGround"); 

        }

        public override void PreparedDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, ScreenSize, Color.White);
        }
    }
}
