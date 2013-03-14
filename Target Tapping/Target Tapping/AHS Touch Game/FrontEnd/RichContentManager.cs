using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TargetTapping.FrontEnd
{
    public class RichContentManager : ContentManager
    {

        public RichContentManager(IServiceProvider sp, string dir) : base(sp, dir) { }

	    /// <summary>
	    /// Load a spiffy button from the specified resource.
	    /// </summary>
	    /// <param name="x">The x of the button's top-left corner.</param>
	    /// <param name="y">The y of the button's top-left corner.</param>
	    /// <param name="resource">The name of the texture to load.</param>
	    /// <returns></returns>
        public GameLibrary.UI.Button MakeButton(int x, int y, string resource)
        {
            Texture2D texture = Load<Texture2D>(resource);
            Rectangle area = new Rectangle(x, y, texture.Width, texture.Height);

	        return new GameLibrary.UI.Button(texture, area);
        }

    }
}
