using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Front_end
{
    public class Button : GiveMeSomethingICanDraw
    {
	// Tangibility is the appearance and size of the button.
        public Tangibility phyisicallity { get; private set; }

        public bool IsClicked;
        public bool JustClicked;

        public Button(Tangibility initialPhysical)
        {
            phyisicallity = initialPhysical;
        }

        public Point Position
        {
            get { return phyisicallity.Position; }
            set { phyisicallity.Position = value; }
        }

	/// <summary>
	/// Given the mouse state, updates the state of the button.
	/// </summary>
	/// <param name="r"></param>
        public void Update(MouseState mouse)
        {
	    Point pos = new Point(mouse.X, mouse.Y);
	    bool inside = phyisicallity.Intersects(pos);
            Update(inside, mouse.LeftButton == ButtonState.Pressed);
        }

        private void Update(bool didIntersect, bool pressed)
        {

            if (didIntersect) {
		// Logic dictates that if you XOR the last value with
		// the current value, you get whether there was a difference.
	        JustClicked = IsClicked ^ pressed;
		IsClicked = pressed;
            } else {
		IsClicked = false;
            }
        }

	/// <summary>
	/// Returns the tangible, which is a thing that is drawable.
	/// </summary>
	/// <returns></returns>
        public Tangibility getTangible()
        {
            return phyisicallity;
        }

    }

}
