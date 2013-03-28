using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TargetTapping.Back_end
{
    internal class EntityFactory
    {
        //Put private class variables here.

        //int i = 123;      // a value type 
        //object o = i;     // boxing 
        //int j = (int)o;   // unboxing

        private static readonly Dictionary<string, Rectangle> SizeNameToRect =  new Dictionary<string, Rectangle>
        {
            {"Tiny", new Rectangle(0, 0, 32, 32) },
            {"Small", new Rectangle(0, 0, 64, 64) } ,
            {"Medium", new Rectangle(0, 0, 128, 128) },
            {"Large", new Rectangle(0, 0, 192, 192)  },
            {"XLarge", new Rectangle(0, 0, 256, 256) },
        };

        // Colour as a Color
        private object _colorObj; // this needs to be unboxed later

        // Position as a Point
        private object _coordinatesObj; // this needs to be unboxed later

        // Size as a rect, minus the position.
        private object _sizeObj;

        // "Circle", "A", "1"
        private string _objName;
        private String _type;


        //Constructor for this class just make a blank object
        public EntityFactory()
        {
            _type = null;
            _colorObj = null;
            _sizeObj = null;
            _coordinatesObj = null;
        }

        //Method to test if all the properties are set in this object
        //if it returns true then an actual object can be created based on the properites
        //of this object.

        public bool IsReady()
        {
            return ((_type != null)
                    && (_colorObj != null)
                    && (_sizeObj != null)
                    && (_coordinatesObj != null)
                    && (_objName != null));
        }

        public Object Make(ContentManager content)
        {
            //need to fix this, currently incrementing pos.x and pos.y by 1 to avoid the double click error thats going on.
            //need to remove this and find a proper fix.
            var rect = Size;

            // I think this creates a copy...
            rect.Location = Coordinates;

            var graphman = GameManager.GlobalInstance.Graphics;

            var entity = new Object(Type, Name, rect, Color,
               content, graphman);

            return entity;

        }

        #region Public properties

        //The following block is the getter and setters for the actual values 

        //getter and setter for type
        public string Type
        {
            //set the shape type 
            set { _type = value; }
            //get the shape type  
            get { return _type; }
        }

        //getter and setter for name
        public string Name
        {
            set { _objName = value; }
            get { return _objName; }
        }

        //getter and setter for color
        public Color Color
        {
            //set the shape type 
            set { _colorObj = value; }
            //get the shape type  
            get { return (Color) _colorObj; }
        }

        //getter and setter for size
        public Rectangle Size
        {
            //set the shape type 
            set { _sizeObj = value; }
            //get the shape type  
            get { return (Rectangle) _sizeObj; }
        }

        //getter and setter for coordinates
        public Point Coordinates
        {
            //set the shape type 
            set { _coordinatesObj = value; }
            //get the shape type  
            get { return (Point) _coordinatesObj; }
        }

        #endregion

        #region Shape setters

        internal void SetNumber()
        {
            Type = "Number";
        }

        internal void SetShape()
        {
            Type = "Shape";
        }

        internal void SetLetter()
        {
            Type = "Letter";
        }

        #endregion

        public void SetSizeFromName(string name)
        {
            Size = SizeNameToRect[name];
        }

    }
}