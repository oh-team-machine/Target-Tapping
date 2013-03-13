using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.FrontEnd
{
    /// <summary>
    /// A Key for a Keyboard. Set the letter and run!
    /// </summary>
    class Key : Button
    {
        public char Char { get; private set; }

        public Key(char initialChar, Texture2D appearance, Rectangle dimensions)
		: base (appearance, dimensions)
        {
            Char = initialChar;
        }

    }
}
