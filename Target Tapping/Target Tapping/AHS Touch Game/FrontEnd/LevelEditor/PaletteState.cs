using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // Every PaletteState takes in the parent.
    abstract class PaletteState : Updatable
    {
        protected Palette parent;
        public PaletteState(Palette p) {
            parent = p;
        }

        abstract public void Update(Microsoft.Xna.Framework.Input.MouseState state);

        abstract public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}
