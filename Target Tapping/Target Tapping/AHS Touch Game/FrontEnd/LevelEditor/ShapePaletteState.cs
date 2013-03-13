using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.UI;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class ShapePaletteState : PaletteState
    {

        private Dictionary<string, Button> shapeButtons = new Dictionary<string, Button>();

        public ShapePaletteState(Palette p) : base(p) { }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            // NOTE! The name of the key IS ALSO the name of the shape in DrawShape.
            //shapeButtons.Add("Circle", null);
        }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
