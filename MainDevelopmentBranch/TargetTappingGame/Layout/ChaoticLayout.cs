using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OhTeamMachine.Widget
{
    /// <summary>
    /// A ChaoticLayout is a layout in which all of its widgets are freely
    /// placed; they have no specific layout.
    /// </summary>
    class ChaoticLayout : Layout
    {
        // Contains an unorganized, unorder amount of elements.
        public List<Widget> Elements = new List<Widget>();

   	override public void prepare() {
	    // Composite pattern: apply prepare to everything.
            foreach (var element in Elements) {
                element.prepare();
            }

	}
    }
}
