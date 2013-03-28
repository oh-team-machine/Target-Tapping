using GameLibrary.UI;

namespace TargetTapping.FrontEnd.LevelEditor
{
    // Shows a list of sizes
    class SizePaletteState : RichPaletteState
    {
        public SizePaletteState(Palette p) : base(p)
        {
            ThingNames = _sizes;
        }

        private readonly string[] _sizes = {
            "Tiny", "Small", "Medium", "Large", "XLarge"
        };

        protected override string ResourceNameFromId(string name)
        {
            var resource = string.Format("ShapePallet/size{0}", name);
            return resource;
        }

        protected override bool OnButtonPressed(string name, Button button)
        {
            Parent.ObjectFactory.SetSizeFromName(name);

            // Go to the next state.
            return true;
        }

    }
}
