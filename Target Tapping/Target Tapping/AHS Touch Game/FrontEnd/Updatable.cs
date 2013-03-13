using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.FrontEnd
{
    public interface Updatable
    {
        void Update();
        void HandleInput();
        void Draw(SpriteBatch spriteBatch);
    }
}
