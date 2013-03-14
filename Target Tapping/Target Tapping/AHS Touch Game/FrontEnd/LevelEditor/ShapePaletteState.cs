using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.UI;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class ShapePaletteState : RichPaletteState
    {

        private readonly string[] _shapeNames =
        {
            "Circle", "Square", "Triangle", "Star"
        };

        public ShapePaletteState(Palette p) : base(p)
        {
            ThingNames = _shapeNames;
        }

        protected override string ResourceNameFromId(string name)
        {
            var resourceName = string.Format("ShapePallet/demo{0}", name);
            return resourceName;
        }

        protected override bool OnButtonPressed(string shapeName, Button button)
        {
            parent.ObjectFactory.Type = "Shape";
            parent.ObjectFactory.Name = shapeName;

            // Go to the next state.
            return true;
        }
    }
}
