using System.Collections.Generic;
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
                yield return c.ToString();
            }
        }

        protected override bool OnButtonPressed(string name, Button button)
        {
            parent.ObjectFactory.SetLetter();
            parent.ObjectFactory.Name = name;

            return true;
        }

        protected override string ResourceNameFromId(string name)
        {
            return string.Format("Letters/letter{0}", name);
        }

        public override void LoadContent(RichContentManager content)
        {
            //throw new NotImplementedException();
        }
    }
}
