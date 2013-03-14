using System;
using Microsoft.Xna.Framework;

namespace TargetTapping.Back_end
{
    class ShapeCreationState
    {

        //Put private class variables here.
        //int i = 123;      // a value type 
        //object o = i;     // boxing 
        //int j = (int)o;   // unboxing

        // "Shape", "Letter", "Number"
        private String _type;

        // Colour
        private object _colorObj; // this needs to be unboxed later

        // Size as an int
        private object _sizeObj; // this needs to be unboxed later
        
        // Position as a point
        private object _coordinatesObj; // this needs to be unboxed later

        // "Circle", "A", "1"
        private string _objName;


        //Constructor for this class just make a blank object
        public ShapeCreationState()
        {
            this._type = null;
            this._colorObj = null;
            this._sizeObj = null;
            this._coordinatesObj = null;

        }

        //Method to test if all the properties are set in this object
        //if it returns true then an actual object can be created based on the properites
        //of this object.
        public bool IsReady()
        {

            return ((this._type != null)
                &&  (this._colorObj != null)
                &&  (this._sizeObj != null)
                &&  (this._coordinatesObj != null)
                &&  (this._objName != null));

        }


        //The following block is the getter and setters for the actual values 

        //getter and setter for type
        public string Type
        {
            //set the shape type 
            set { this._type = value; }
            //get the shape type  
            get { return this._type; }
        }

        //getter and setter for name
        public string Name
        {
            set { this._objName = value; }
            get { return this._objName; }
        }

        //getter and setter for color
        public Color Color
        {
            //set the shape type 
            set { this._colorObj = value;
                }
            //get the shape type  
            get { return (Color)this._colorObj; }
        }

        //getter and setter for size
        public int Size
        {
            //set the shape type 
            set { this._sizeObj = value; }
            //get the shape type  
            get { return (int)this._sizeObj; }
        }

        //getter and setter for coordinates
        public Point Coordinates
        {
            //set the shape type 
            set { this._coordinatesObj = value; }
            //get the shape type  
            get { return (Point)this._coordinatesObj; }
        }


    }
}
