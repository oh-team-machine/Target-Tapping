using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TargetTapping.FrontEnd
{
    /// <summary>
    /// An on-screen Keyboard. Get the key pressed by
    /// getting the CurrentKey, if it's not "".
    /// </summary>
    class Keyboard : Updatable
    {

	// All of the keys.
        private List<Key> keys;
        private ContentManager content;

	public string CurrentKey { get; private set; }

        public Point Position
        {
            get;
            private set;
            //set
            //{
            // TODO: make crazy keyboard mover here!
            //}
        }


	// The rows as strings.
        private string[] rows = {
		"qwertyuiop",
		"asdfghjkl",
		"zxcvbnm"
	};

        public Keyboard(int x, int y, ContentManager content)
        {
	    this.content = content;
            CurrentKey = "";
            Position = new Point(x, y);
        }
	
	public void LoadContent() {

            keys = new List<Key>();

	    int x = Position.X;
            int y = Position.Y;
	    // GAH! HARD CODED! GET AWAY!
	    int keyWidth = 48;
	    int keyHeight = 48;


	    // Make a key for each key in a row.
	    foreach (string row in rows)
	    {
                foreach (char c in row)
                {
                    keys.Add(MakeKey(x, y, c));
                    x += keyWidth;
                }
	        x = Position.X;
                y += keyHeight;
	    }

	}

        public void Update(MouseState state)
        {
	    // Get the key press for every input.
            foreach (Key key in keys)
            {
                if (key.IsClicked())
                {
                    CurrentKey = key.Char.ToString();
                    return;
                }
            }

	    // If we don't find a key, let the current key be none.
            CurrentKey = "";

	    // Do the updates independently.
            foreach (Key key in keys)
            {
                key.Update(state);
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (Key key in keys)
            {
                key.Draw(spriteBatch);
            }
        }

	// Produces a new Key object.
        private Key MakeKey(int x, int y, char key)
        {
	    var appearance = content.Load<Texture2D>(
	        "OSK/" + key + "Button"
	    );

	    var dimensions = new Rectangle(x, y,
		appearance.Width, appearance.Height);

	    return new Key(key, appearance, dimensions);
        }
    }
}
