using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstGame.Back_end;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Back_end
{
    //Pat's test of vs2010 git extension
    /*
    * This class contains all the properties (such as color, size, etc..) for our shapes,
    * letter, numbers that the therapist will put in the level.
     */

    class CreateShape
    {

        //Put private class variables here.
        Color color;
        int size;
        string shape;
        bool multiSelect;
        int[] position = new int[2];
         

        

        //Constructor for this class
        public CreateShape(string shapePassedIn, Color colorPassedIn, int sizePassedIn, int[] positionPassedIn,
                                bool multiSelectPassedIn)
        {
            setColor(colorPassedIn);
            setShape(shapePassedIn);
            setSize(sizePassedIn);
            setPosition(positionPassedIn);
            setMultiSelect(multiSelectPassedIn);

        }


        //Draws the shape on the screen.
        public void drawShape(GraphicsDeviceManager graphics)
        {
            //Texture2D texture;
            //DrawShape draw;
            //texture = draw.drawShape(shape, size, graphics);
        }

        //Methods for this class, such as getters and setters, etc...
        public void setColor(Color colorPassedIn)
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

        public void setPosition(int[] poistionPassedIn)
        {
            position = poistionPassedIn;
        }

        public void setMultiSelect(bool multiSelectPassedIn)
        {
            multiSelect = multiSelectPassedIn;
        }

        public Color getColor()
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

        public int[] getPosition()
        {
            return position;
        }

        public bool getMultiSelect()
        {
            return multiSelect;
        }
    }

}

   


