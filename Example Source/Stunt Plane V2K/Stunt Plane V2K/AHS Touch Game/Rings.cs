using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Stunt_Plane_V2K
{
    class Rings
    {
        Texture2D ringFront;
        Texture2D ringBack;
        Texture2D ringFrontCleared;
        Texture2D ringBackCleared;

        SoundEffect plingSound;

        public uint[] ringCollision;

        public List<Rectangle> ringRects;
        public List<bool> ringCanCollide;

        List<Vector2> ringLocations;

        Random rand;

        int randY;
        float speed;
        int ringTimer;
        int timer;
        int screenWidth;
        int screenHeight;
        public int numMissed;

        public Rings(Texture2D _ringFront, Texture2D _ringBack, Texture2D _ringFrontCleared, Texture2D _ringBackCleared, int number, float _speed, int _screenWidth, int _screenHeight, SoundEffect _pling)
        {
            plingSound = _pling;

            rand = new Random();

            ringRects = new List<Rectangle>();
            ringLocations = new List<Vector2>();
            ringCanCollide = new List<bool>();

            ringFront = _ringFront;
            ringBack = _ringBack;
            ringFrontCleared = _ringFrontCleared;
            ringBackCleared = _ringBackCleared;
            screenHeight = _screenHeight;
            screenWidth = _screenWidth;

            ringCollision = new uint[ringFront.Width * ringFront.Height];

            ringFront.GetData<uint>(ringCollision);

            randY = rand.Next(0, screenHeight - ringFront.Height);

            ringRects.Add(new Rectangle(screenWidth, randY, ringFront.Width, ringFront.Height));
            ringLocations.Add(new Vector2(screenWidth, randY));
            ringCanCollide.Add(true);

            speed = _speed + 0.5f;
            timer = 0;
            ringTimer = (int)((screenWidth * 16) / (speed * number));
            numMissed = 0;
        }

        public void Draw(SpriteBatch batch)
        {
            for (int i = 0; i < ringLocations.Count; i++)
            {
                if (ringCanCollide[i] == true)
                {
                    batch.Draw(ringFront, new Vector2(ringLocations[i].X + 45, ringLocations[i].Y - 1), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    batch.Draw(ringBack, ringLocations[i], null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.2f);
                }

                else
                {
                    batch.Draw(ringFrontCleared, new Vector2(ringLocations[i].X + 45, ringLocations[i].Y - 1), null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    batch.Draw(ringBackCleared, ringLocations[i], null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.2f);
                }
            }
        }

        public void Update(GameTime time)
        {
            timer += time.ElapsedGameTime.Milliseconds;

            if (timer >= ringTimer)
            {
                timer = 0;
                nextRing();
            }

            for (int i = 0; i < ringLocations.Count; i++)
            {
                Vector2 temp = ringLocations[i];

                temp.X -= speed * (time.ElapsedGameTime.Milliseconds / 10);

                if (temp.X <= -100)
                {
                    if (ringCanCollide[i])
                    {
                        numMissed++;
                    }

                    ringLocations.RemoveAt(i);
                    ringRects.RemoveAt(i);
                    ringCanCollide.RemoveAt(i);
                }

                else
                {
                    ringLocations[i] = temp;
                    ringRects[i] = new Rectangle((int)temp.X, (int)temp.Y, ringFront.Width, ringFront.Height);
                }
            }
        }

        private void nextRing()
        {
            randY = rand.Next(0, screenHeight - ringFront.Height);

            ringRects.Add(new Rectangle(screenWidth, randY, ringFront.Width, ringFront.Height));
            ringLocations.Add(new Vector2(screenWidth, randY));
            ringCanCollide.Add(true);
        }

        public void playSound()
        {
            plingSound.Play();
        }
    }
}
