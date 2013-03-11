using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OhTeamMachine.TapLevel
{
    public class Target : TargetContainer
    {
        public Rectangle Coords { get; set; }
        public string Shape { get; set; }
        public string Size { get; set; }
        public Color Color { get; set; }
    }
}
