using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TargetTapping.Back_end
{
    class ShapeCreationState
    {

        //Put private class variables here.
        bool _typeSet;
        bool _colorSet;
        bool _sizeSet;
        bool _coordinatesSet;

        private String _type;
        private Color _color;
        private int _size;
        private Rectangle _coordinates;


        //Constructor for this class just make a blank object
        public ShapeCreationState()
        {
            this._typeSet = false;
            this._colorSet = false;
            this._sizeSet = false;
            this._coordinatesSet = false;

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
        //getter and setter for color
        public Color Color
        {
            //set the shape type 
            set { this._color = value; }
            //get the shape type  
            get { return this._color; }
        }

        //getter and setter for size
        public int Size
        {
            //set the shape type 
            set { this._size = value; }
            //get the shape type  
            get { return this._size; }
        }

        //getter and setter for coordinates
        public Rectangle Coordinates
        {
            //set the shape type 
            set { this._coordinates = value; }
            //get the shape type  
            get { return this._coordinates; }
        }

        //The following block is the getter and setters for the state of the value

        //getter and setter for type
        public bool TypeTest
        {
            //set the shape type 
            set { this._typeSet = value; }
            //get the shape type  
            get { return this._typeSet; }
        }
        //getter and setter for color
        public bool ColorTest
        {
            //set the shape type 
            set { this._colorSet = value; }
            //get the shape type  
            get { return this._colorSet; }
        }

        //getter and setter for size
        public bool SizeTest
        {
            //set the shape type 
            set { this._sizeSet = value; }
            //get the shape type  
            get { return this._sizeSet; }
        }

        //getter and setter for coordinates
        public bool CoordinatesTest
        {
            //set the shape type 
            set { this._coordinatesSet = value; }
            //get the shape type  
            get { return this._coordinatesSet; }
        }


    }
}
