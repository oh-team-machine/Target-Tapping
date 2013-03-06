using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FirstGame.Back_end
{

    /*
    * This class contains all the properties (such as color, size, etc..) for our shapes,
    * letter, numbers that the therapist will put in the level.
     */

    class CreateShape
    {

        //Put private class variables here.
        string color;
        int size;
        string shape;
        bool multiSelect;
        int[][] position;
         

        

        //Constructor for this class
        public CreateShape(string shapePassedIn, string colorPassedIn, int sizePassedIn, int[][] positionPassedIn,
                                bool multiSelectPassedIn)
        {
            setColor(colorPassedIn);
            setShape(shapePassedIn);
            setSize(sizePassedIn);
            setPosition(positionPassedIn);
            setMultiSelect(multiSelectPassedIn);

        }


        //Draws the shape on the screen.
        public void drawShape()
        {

        }

        //Methods for this class, such as getters and setters, etc...
        public void setColor(string colorPassedIn)
        {
            color = colorPassedIn;
        }

        public void setShape(string shapePassedIn)
        {
            shape = shapePassedIn;
        }

        public void setSize(int sizePassedIn)
        {
            size = sizePassedIn;
        }

        public void setPosition(int[][] poistionPassedIn)
        {
            position = poistionPassedIn;
        }

        public void setMultiSelect(bool multiSelectPassedIn)
        {
            multiSelect = multiSelectPassedIn;
        }

        public string getColor()
        {
            return color;
        }

        public string getShape()
        {
            return shape;
        }

        public int getSize()
        {
            return size;
        }

        public int[][] getPosition()
        {
            return position;
        }

        public bool getMultiSelect()
        {
            return multiSelect;
        }
    }

}

   


