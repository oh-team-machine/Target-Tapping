using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GameLibrary.UI;

namespace TargetTapping.FrontEnd.LevelEditor
{
    class AlphPaletteState : RichPaletteState
    {
        public AlphPaletteState(Palette p) : base(p)
        {
            ThingNames = MakeAlphapbet().ToArray();
        }

        protected override int MaxInRow()
        {
            return 3;
        }

        // Yields the alphabet as strings.
        private static IEnumerable<string> MakeAlphapbet()
        {
            for (var c = 'A'; c <= 'Z'; c++)
            {
                // I don't know what this InvariantCulture stuff is about,
                // but ReShaper was complaining about it.
                yield return c.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected override bool OnButtonPressed(string name, Button button)
        {
            Parent.ObjectFactory.SetLetter();
            Parent.ObjectFactory.Name = name;

            return true;
        }

        protected override string ResourceNameFromId(string name)
        {
            return string.Format("OSK/{0}Button", name);
        }
    }
}
