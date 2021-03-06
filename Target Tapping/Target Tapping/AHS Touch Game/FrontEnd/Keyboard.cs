﻿using System;
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
        Texture2D myOSKBackground;
        Vector2 myOSKBackgroundPosition;
        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

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
		"zxcvbnm",
        "_____0" //_ for underscore, and 0 for delete!
	};

    public Keyboard(int x, int y, ContentManager content)
    {
	    this.content = content;
        CurrentKey = "";
        Position = new Point(x, y);
    }
	
	public void LoadContent() {
        myOSKBackgroundPosition = (new Vector2(((screenWidth / 2) - 260), ((screenHeight) - 250)));
        myOSKBackground = content.Load<Texture2D>("OSK/keyboardBackground");

        keys = new List<Key>();

	    int x = Position.X;
        int y = Position.Y;
	    // GAH! HARD CODED! GET AWAY!
	    int keyWidth = 48;

	    int keyHeight = 48;
        int indentation = 0;

	    // Make a key for each key in a row.
	    foreach (string row in rows)
	    {
            indentation += 1;
            int insideCounter =  0;
                foreach (char c in row)
                {
                    keys.Add(MakeKey(x, y, c));
                    x += keyWidth;
                    insideCounter++;
                    if(indentation == 4) {
                        if (insideCounter == 5) { x += 50; }
                    }
                }
                if (indentation == 1)
                {
                    x = Position.X + 15;
                    y += keyHeight;
                }
                if (indentation == 2)
                {
                    x = Position.X + 35;
                    y += keyHeight;
                }
                if (indentation == 3)
                {
                    x = Position.X + 130;
                    y += keyHeight;
                }
               
	    }

	}

        public void Update(MouseState state)
        {
            // Do the updates independently.
            foreach (Key key in keys)
            {
                key.Update(state);
            }
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

	   
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myOSKBackground, myOSKBackgroundPosition, Color.White);
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

        // Stuff from the constructor should really go in here.
        public void LoadContent(RichContentManager content)
        {
            throw new NotImplementedException("This class was designed before the Updatable interface "
                + "and I'm too lazy to fix it right now.");
        }
    }
}
