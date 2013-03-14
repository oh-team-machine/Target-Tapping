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
            // TODO: DETERMINE THE SIZE FROM THE NAME OF THE SIZE
            parent.ObjectFactory.Size = 1;

            // Go to the next state.
            return true;
        }

    }
}
