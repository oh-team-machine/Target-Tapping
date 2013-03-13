using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TargetTapping.FrontEnd
{
    public interface Updatable
    {
        void Update(MouseState state);
        void Draw(SpriteBatch spriteBatch);
    }
}
