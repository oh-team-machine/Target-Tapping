using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // State for when the user selects the position on the screen.
    class PositionPaletteState : PaletteState
    {

        public PositionPaletteState(Palette p) : base(p) { }

        public override void Update(MouseState mouse)
        {
            // Assume the click is the position.
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                //int x = mouse.X, y = mouse.Y;
                parent.RequestStateChange("Initial");
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // Don't need to draw anything!
        }
    }
}
