using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.UI;
using Microsoft.Xna.Framework.Graphics;

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
            throw new NotImplementedException();
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

        protected override void DrawExtras(SpriteBatch spriteBatch)
        {
            base.DrawExtras(spriteBatch);
        }

        protected override string ResourceNameFromId(string name)
        {
            return string.Format("Numbers/number{0}", name);
        }

    }
}
