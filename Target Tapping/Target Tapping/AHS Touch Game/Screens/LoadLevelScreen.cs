using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.Screens
{
    class LoadLevelScreen : AbstractRichScreen
    {

        public override void LoadContent()
        {
            base.LoadContent();

	    // Load buttons 'n' stuff, yo!
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
	    // Lagic!
	    //AddScreenAndChill(new LoadLevelScreen(levelName));

            base.Update(gameTime);
        }

        public override void PreparedDraw(SpriteBatch SpriteBatch)
        {
	    
        }
    }
}
