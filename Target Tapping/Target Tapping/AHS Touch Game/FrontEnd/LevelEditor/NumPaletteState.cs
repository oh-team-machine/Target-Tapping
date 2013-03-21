using System;
using GameLibrary.UI;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // Choose a number, move to size state.
    class NumPaletteState : RichPaletteState
    {
        private readonly string[] _numbers =
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
        };

        public NumPaletteState(Palette p) : base(p)
        {
            ThingNames = _numbers;
        }

        protected override bool OnButtonPressed(string name, Button button)
        {
            Parent.ObjectFactory.SetNumber();
            Parent.ObjectFactory.Name = name;

            return true;
        }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState state)
        {
            base.Update(state);
            Console.Beep();
        }

        protected override int MaxInRow()
        {
            return 3;
        }

        protected override string ResourceNameFromId(string name)
        {
            return string.Format("ShapePallet/{0}Btn", name);
        }

    }
}
