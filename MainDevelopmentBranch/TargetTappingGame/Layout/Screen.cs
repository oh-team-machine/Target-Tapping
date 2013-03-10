using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OhTeamMachine.Widget
{
    /// <summary>
    /// A Screen is a layout that represents an interactive screen in the
    /// program. A screen is a layout that contains a Layout.
    /// </summary>
    class Screen : Layout
    {
	/// <summary>
	///  A Screen is fitted with a layout.
	/// </summary>
        public Layout Content;

	/// Default Screen sets default Layout to its defaults.
        public Screen() : this(new ChaoticLayout())
        {
	    // Default layout is a chaotic one!
	    // SERVES YOU RIGHT FOR USING THE DEFAULT LAYOUT.
        }

	/// <summary>
	///  Screen loader with inital layout, if you're one of those cool
        ///   dependency injection kids.
	/// </summary>
	/// <param name="initialLayout">The initial Layout for this screen.</param>
        public Screen(Layout initialLayout)
        {
            Content = initialLayout;
            this.prepare();
        }

        public override void prepare()
        {
            Content.prepare();
        }

    }
}
