using GameLibrary.UI;
using Microsoft.Xna.Framework;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class ColorPaletteState : RichPaletteState
    {
        private readonly string[] _names = {
            "black", "darkGrey",
            "darkBlue", "blue",
            "lightBlue", "lightGreen",
            "orange", "yellow",
            "red", "pink"
        };

        public ColorPaletteState(Palette p) : base(p)
        {
            ThingNames = _names;
        }

        // What happens when a Color gets clicked on.
        protected override bool OnButtonPressed(string name, Button button)
        {
            // TODO: DETERMINE THE ACTUAL COLOUR BY THE NAME
            var color = new Color(255, 255, 0);
            parent.ObjectFactory.Color = color;

            // Go to the next state.
            return true;
        }
        
        // Gets the resource name from the colour name.
        protected override string ResourceNameFromId(string name)
        {
            var resource = string.Format("ShapePallet/{0}Color", name);
            return resource;
        }

        // Max buttons in row is two.
        protected override int MaxInRow()
        {
            return 2;
        }

    }
}
