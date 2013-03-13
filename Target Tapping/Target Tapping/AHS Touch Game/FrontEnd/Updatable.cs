using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TargetTapping.FrontEnd
{
    public interface Updatable
    {
        void LoadContent(ContentManager content);
        void Update(MouseState state);
        void Draw(SpriteBatch spriteBatch);
    }
}
