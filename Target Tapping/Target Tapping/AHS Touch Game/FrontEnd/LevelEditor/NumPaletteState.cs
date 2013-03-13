using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // Choose a number, move to size state.
    class NumPaletteState : PaletteState
    {
        public NumPaletteState(Palette p) : base(p) { }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }
    }
}
