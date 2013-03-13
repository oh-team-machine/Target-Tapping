using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class Palette : Updatable
    {
        public Object Object { get; private set; }

        public Palette()
        {
            Object = null;
        }

        void Hide()
        {
        }


        public void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
