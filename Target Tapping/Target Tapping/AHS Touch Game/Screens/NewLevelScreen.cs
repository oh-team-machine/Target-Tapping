using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.FrontEnd;

namespace TargetTapping.Screens
{
    class NewLevelScreen : AbstractRichScreen
    {
        private Keyboard keyboard;

        public override void LoadContent()
        {
            base.LoadContent();

            keyboard = new Keyboard(401, 520, content);
            keyboard.LoadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
	        // Update stuff here!

        }

        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {
	    keyboard.Draw(SpriteBatch);
        }
    }
}
