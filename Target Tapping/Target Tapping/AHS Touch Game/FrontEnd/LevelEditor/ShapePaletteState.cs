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

        public override void LoadContent(RichContentManager content)
        {

            int x = parent.Position.X;
            int y = parent.Position.Y;
            
            // NOTE! The name of the key IS ALSO the name of the shape in DrawShape.
            foreach (var name in shapeNames)
            {
                var resourceName = "ShapePallet/demo" + name;
                var button = content.MakeButton(x, y, resourceName);
                shapeButtons.Add(name, button);
                y += button.Rect.Height;
            }
        }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {

            // Find a pair that is clicked.
            foreach (var pair in shapeButtons)
            {
                if (pair.Value.IsClicked())
                {
                    var shapeName = pair.Key;

                    parent.ObjectFactory.Type = "Shape";
                    parent.ObjectFactory.Name = shapeName;

                    // Next state in the palette.
                    parent.RequestStateChange("NEXT");

                    break;
                }

            }

            // Update all of the button's states
            foreach (var button in shapeButtons.Values)
            {
                button.Update(state);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var button in shapeButtons.Values)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
