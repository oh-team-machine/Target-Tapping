using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;
using Microsoft.Xna.Framework;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class ColorPaletteState : PaletteState
    {
        public ColorPaletteState(Palette p) : base(p) { }

        private string[] colorNames =
        {
            "black", "darkGrey",
            "darkBlue", "blue",
            "lightBlue", "lightGreen",
            "orange", "yellow",
            "red", "pink"
        };
        private Dictionary<string,Button> colorButtons = new Dictionary<string,Button>();

        public override void LoadContent(RichContentManager content)
        {
            // Load the colours.
            int x = parent.Position.X,
                y = parent.Position.Y;

            const int maxInRow = 2;
            int inRow = 0;

            foreach (var name in colorNames)
            {
                var resource = "ShapePallet/" + name + "Color";
                var button = content.MakeButton(x, y, resource);

                x += button.Rect.Width;
                inRow++;

                // Start from the beginning for a new row.
                if (inRow >= maxInRow) {
                    x = parent.Position.X;
                    inRow = 0;
                    y += button.Rect.Height;
                }

                colorButtons.Add(name, button);

            }
        }

        public override void Update(MouseState state)
        {
            // Get the button that is clicked and do the next action!
            foreach (var button in colorButtons.Values)
            {
                if (button.IsClicked())
                {
                    // TODO: UNHARDCODE THIS
                    var color = new Color(255, 0, 0);

                    parent.ObjectFactory.Color = color;
                    parent.RequestStateChange("NEXT");

                    break;
                }
            }

            // Update the button states.
            foreach (var button in colorButtons.Values)
            {
                button.Update(state);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in colorButtons.Values)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
