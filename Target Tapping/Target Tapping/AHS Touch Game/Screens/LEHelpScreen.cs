using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using TargetTapping.Back_end;

namespace TargetTapping.Screens
{
    class LEHelpScreen : AbstractRichScreen
    {
        
        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public override void LoadContent()
        {
            //((screenWidth / 2) - 400)
            base.LoadContent();
            
	       // Load buttons 'n' stuff, yo!
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
	       // Update stuff here!
           

        }

        public override void PreparedDraw(SpriteBatch spriteBatch)
        {

            
        }

    }
}
