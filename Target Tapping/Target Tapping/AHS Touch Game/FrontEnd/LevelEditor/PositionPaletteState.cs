using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class PositionPaletteState : PaletteState
    {
        private Palette parent;

        public PositionPaletteState(Palette p)
        {
            parent = p;
            // Immediately hide the palette
            parent.Hide();
        }


        public void Update(MouseState mouse)
        {
            // Should check if mouse state is clicked.
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // Don't need to draw anything!
        }
    }
}
