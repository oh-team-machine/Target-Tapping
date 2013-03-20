using System;
using Microsoft.Xna.Framework;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // Every PaletteState takes in the parent.
    abstract class PaletteState : Updatable
    {
        protected Palette Parent;

        // Position of the palette's top-left
        abstract internal Point Position { get; set; }

        protected PaletteState(Palette p)
        {
            if (p == null) throw new ArgumentNullException("p");
            Parent = p;
        }

        abstract public void LoadContent(RichContentManager content);

        abstract public void Update(Microsoft.Xna.Framework.Input.MouseState state);

        abstract public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);

    }
}
