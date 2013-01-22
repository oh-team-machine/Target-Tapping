using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AHSGame
{
    class ArrowButtons
    {
        #region Variables

        List<Texture2D> arrows;
        List<Texture2D> activeArrows;

        List<Rectangle> arrowRectangles;
        List<uint[]> arrowCollisions;
        List<bool> bArrowActive;

        Random rand;

        int configuration;
        int simultaneous;
        int missed;

        float timer;
        float timeout;

        #endregion Variables

        public ArrowButtons(List<Texture2D> _arrows,List<Texture2D> _activeArrows, int _configuration, float _timeout, int _simultaneous)
        {
            rand = new Random();

            arrows = _arrows;
            activeArrows = _activeArrows;

            configuration = _configuration - 1;
            timeout = _timeout;
            timer = _timeout;
            simultaneous = _simultaneous;

            arrowRectangles = new List<Rectangle>();
            arrowCollisions = new List<uint[]>();
            bArrowActive = new List<bool>();

            foreach (Texture2D texture in arrows)
            {
                uint[] collision = new uint[texture.Width * texture.Height];

                texture.GetData<uint>(collision);

                arrowCollisions.Add(collision);

                bArrowActive.Add(false);
            }

            switch (configuration)
            {
                case 0: config0(); break;
                case 1: config1(); break;
            }
        }

        public void update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timer >= timeout)
            {
                missed += trueCount();
                nextArrows();
                timer = 0.0f;
            }

            if(trueCount() == 0)
            {
                nextArrows();
            }
        }

        private int trueCount()
        {
            int count = 0;

            for (int i = 0; i < 8; i++)
            {
                if (bArrowActive[i])
                    count++;
            }

            return count;
        }

        private void nextArrows()
        {
            int firstRand;
            int secondRand = -1;

            for (int i = 0; i < 8; i++)
            {
                bArrowActive[i] = false;
            }

            firstRand = rand.Next(8);
            bArrowActive[firstRand] = true;

            if (simultaneous == 2)
            {
                secondRand = rand.Next(8);

                while (secondRand == firstRand){
                    secondRand = rand.Next(8);
                }

                bArrowActive[secondRand] = true;

            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 8; i++)
            {
                if (!bArrowActive[i])
                    spriteBatch.Draw(arrows[i], arrowRectangles[i], Color.White);
                else
                    spriteBatch.Draw(activeArrows[i], arrowRectangles[i], Color.White);
            }
        }

        public List<uint[]> getArrowCollisionBounds()
        {
            return arrowCollisions;
        }

        public List<Rectangle> getArrowRectangles()
        {
            return arrowRectangles;
        }

        public bool isArrowActive(int _arrow)
        {
            return bArrowActive[_arrow];
        }

        public void setArrowActive(int _arrow, bool tf)
        {
            bArrowActive[_arrow] = tf;
            timer = 0.0f;
        }

        public int getNumMissed()
        {
            return missed;
        }

        private void config0()
        {
            arrowRectangles.Add(new Rectangle(615, 275, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(685, 305, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(715, 375, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(685, 445, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(615, 475, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(545, 445, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(515, 375, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(545, 305, arrows[0].Width, arrows[0].Height));
        }

        private void config1()
        {
            arrowRectangles.Add(new Rectangle(615, 200, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(765, 250, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(815, 400, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(765, 550, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(615, 600, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(465, 550, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(415, 400, arrows[0].Width, arrows[0].Height));
            arrowRectangles.Add(new Rectangle(465, 250, arrows[0].Width, arrows[0].Height));
        }
    }
}
