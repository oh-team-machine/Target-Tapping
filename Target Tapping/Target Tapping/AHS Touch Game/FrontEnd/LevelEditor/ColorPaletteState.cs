using System.Diagnostics;
using System.Reflection;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            // WARNING: Don't look at the implementation of DoEvil.
            // Just replace it.
            var color = DoEvil(button);
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

        private Color DoEvil(Button butt)
        {
            var fields = typeof(Button).GetFields( BindingFlags.NonPublic | BindingFlags.Instance);

            Texture2D tex = null;
            // must get icon field which was field 1 on my machine
            foreach (var info in fields)
            {
                if (info.FieldType == typeof(Texture2D))
                {
                    tex = (Texture2D)info.GetValue(butt);
                }
                
            }

            var centerPixel = new Color[1];
            Debug.Assert(tex != null, "Could not get texture information.");

            tex.GetData<Color>( 0,
                   // Interior rectangle with only one pixel.
                   new Rectangle(2, 2, 1, 1), 
                   centerPixel, 0, 1);

            return centerPixel[0];
        }

    }
}
