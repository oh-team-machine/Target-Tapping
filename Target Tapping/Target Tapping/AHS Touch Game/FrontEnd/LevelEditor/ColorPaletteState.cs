namespace TargetTapping.FrontEnd.LevelEditor
{
    class ColorPaletteState : PaletteStateBase
    {
        public ColorPaletteState(Palette p) : base(p)
        {
            ThingNames = _names;
        }
        
        private readonly string[] _names = {
            "black", "darkGrey",
            "darkBlue", "blue",
            "lightBlue", "lightGreen",
            "orange", "yellow",
            "red", "pink"
        };

        // Gets the resource name from the colour name.
        protected override string ResourceNameFromId(string name)
        {
            var resource = string.Format("ShapePallet/{0}Color", name);
            return resource;
        }
    }
}
