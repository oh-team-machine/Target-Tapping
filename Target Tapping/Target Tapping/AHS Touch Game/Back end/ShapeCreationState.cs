using System;
using Microsoft.Xna.Framework;

namespace TargetTapping.Back_end
{
    internal class ShapeCreationState
    {
        //Put private class variables here.
        //int i = 123;      // a value type 
        //object o = i;     // boxing 
        //int j = (int)o;   // unboxing

        // "Shape", "Letter", "Number"

        // Colour
        private object _colorObj; // this needs to be unboxed later

        // Size as an int

        // Position as a point
        private object _coordinatesObj; // this needs to be unboxed later

        // "Circle", "A", "1"
        private string _objName;
        private object _sizeObj; // this needs to be unboxed later
        private String _type;


        //Constructor for this class just make a blank object
        public ShapeCreationState()
        {
            _type = null;
            _colorObj = null;
            _sizeObj = null;
            _coordinatesObj = null;
        }

        //Method to test if all the properties are set in this object
        //if it returns true then an actual object can be created based on the properites
        //of this object.


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
        public int Size
        {
            //set the shape type 
            set { _sizeObj = value; }
            //get the shape type  
            get { return (int) _sizeObj; }
        }

        //getter and setter for coordinates
        public Point Coordinates
        {
            //set the shape type 
            set { _coordinatesObj = value; }
            //get the shape type  
            get { return (Point) _coordinatesObj; }
        }

        public bool IsReady()
        {
            return ((_type != null)
                    && (_colorObj != null)
                    && (_sizeObj != null)
                    && (_coordinatesObj != null)
                    && (_objName != null));
        }


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
    }
}