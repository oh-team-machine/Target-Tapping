using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TargetTapping.FrontEnd
{
    public interface Updatable
    {
        void LoadContent(RichContentManager content);
        void Update(MouseState state);
        void Draw(SpriteBatch spriteBatch);
    }
}
