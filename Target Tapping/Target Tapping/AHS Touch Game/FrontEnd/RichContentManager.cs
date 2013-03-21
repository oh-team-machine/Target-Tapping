using System;
using GameLibrary.UI;
using Microsoft.Xna.Framework;
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
        public Button MakeButton(int x, int y, string resource)
        {
            var texture = Load<Texture2D>(resource);
            var area = new Rectangle(x, y, texture.Width, texture.Height);

	        return new Button(texture, area);
        }

    }
}
