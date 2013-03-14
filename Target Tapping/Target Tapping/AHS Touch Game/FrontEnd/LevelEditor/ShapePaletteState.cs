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
        private string[] shapeNames =
        {
            "Circle", "Square", "Triangle", "Star"
        };

        public ShapePaletteState(Palette p) : base(p) { }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            // NOTE! The name of the key IS ALSO the name of the shape in DrawShape.
            foreach (var name in shapeNames)
            {
                shapeButtons.Add(name, null);
            }
        }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var button in shapeButtons)
            {

            }
        }
    }
}
