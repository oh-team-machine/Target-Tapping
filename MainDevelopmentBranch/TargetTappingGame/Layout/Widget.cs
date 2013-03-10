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

namespace OhTeamMachine.Widget
{
    /// <summary>
    /// The abstract widget class from which all widgets and layouts inherit from.
    /// </summary>
    public abstract class Widget
    {
	/// <summary>
	/// The dimensions of the Widget.
	/// </summary>
        [ContentSerializer(Optional = true)]
        public Rectangle Dimensions { get; set; }
    
        /// <summary>
        /// Prepares the item after construction.
        /// </summary>
        public abstract void prepare();
    }
}
