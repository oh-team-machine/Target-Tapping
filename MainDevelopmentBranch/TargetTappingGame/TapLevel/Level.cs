using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OhTeamMachine.TapLevel
{
    /// <summary>
    /// Root of our level serialization classes.
    /// </summary>
    public class Level
    {
        public LevelMeta Meta { get; set; }
        public List<TargetContainer> Targets { get; set; }
    }
}
