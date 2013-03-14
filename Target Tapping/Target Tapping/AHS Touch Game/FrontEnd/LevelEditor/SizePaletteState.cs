using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.UI;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // Shows a list of sizes
    class SizePaletteState : PaletteState
    {
        public SizePaletteState(Palette p) : base(p) { }

        private string[] sizes = {
            "Tiny", "Small", "Medium", "Large", "XLarge"
        };

        private Dictionary<string, Button> sizeButtons = new Dictionary<string, Button>();

        public override void LoadContent(RichContentManager content)
        {
            // Load all of the size palette icons.
            int x = parent.Position.X,
                y = parent.Position.Y;

            // Add each button to the dict.
            foreach (var name in sizes)
            {
                var resource = "ShapePallet/size" + name;
                var button = content.MakeButton(x, y, resource);
                y += button.Rect.Height;
                sizeButtons.Add(name, button);
            }

        }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            foreach (var pair in sizeButtons)
            {
                if (pair.Value.IsClicked())
                {
                    // A size button has been clicked!

                    // TODO: REMOVE LAZINESS:
                    parent.ObjectFactory.Size = 1;
                    parent.RequestStateChange("NEXT");
                    break;
                }
            }


            // Update the buttons.
            foreach (var button in sizeButtons.Values)
            {
                button.Update(state);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var button in sizeButtons.Values)
            {
                button.Draw(spriteBatch);
            }

        }
    }
}
