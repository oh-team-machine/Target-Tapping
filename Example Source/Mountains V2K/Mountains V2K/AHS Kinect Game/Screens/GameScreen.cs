using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Kinect;

namespace AHS_Kinect_Game.Screens
{
    class GameScreen : GameLibrary.Screen
    {
        #region Variables

        private ContentManager Content;

        private SpriteBatch SpriteBatch;

        private Texture2D bg;
        private Rectangle fullscreen;

        private SpriteFont gameFont;

        private MouseState state;

        private float timeInGame;

        private int score;
        private int repetitions;

        private Vector2 scorePosition;

        private Texture2D plane;
        private Texture2D mountain;
        private Texture2D airport;
        private Texture2D explosion;
        private Texture2D cursor;

        private Rectangle planeRect;
        private Rectangle mountainRect;
        private Rectangle airportRect;
        private Rectangle cursorRect;

        private uint[] planeCollision;
        private uint[] mountainCollision;
        private uint[] airportCollision;
        private uint[] cursorCollision;

        private SoundEffect planeSound;
        private SoundEffect crashSound;
        private SoundEffectInstance planeSoundInstance;

        private bool bCollision;
        private bool bContinuedCollision;
        private bool bIsCarried;

        private int crashes;

        #endregion Variables

        public override void Initialize()
        {
            timeInGame = 0.0f;

            //Setup Values
            repetitions = ((Manager)ScreenManager).Repetitions;

            //Tracked during the game
            score = 0;

            fullscreen = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);

            ScreenManager.Game.IsMouseVisible = false;

            bCollision = false;
            bContinuedCollision = false;
            bIsCarried = false;

            crashes = 0;

            base.Initialize();
        }

        public override void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Game.Services, "Content");

            SpriteBatch = ScreenManager.SpriteBatch;

            bg = Content.Load<Texture2D>("Sprites/MountainBackground");

            gameFont = Content.Load<SpriteFont>("Font");

            float scoreLength = (gameFont.MeasureString("999/999")).X;
            scorePosition = new Vector2(this.ScreenManager.ScaleXPosition((this.ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth / 2.0f) - (scoreLength / 2.0f)), this.ScreenManager.ScaleYPosition(20.0f));

            switch (((Manager)ScreenManager).Height)
            {
                case 1: mountain = Content.Load<Texture2D>("Sprites/Mountain10%"); break;
                case 2: mountain = Content.Load<Texture2D>("Sprites/Mountain20%"); break;
                case 3: mountain = Content.Load<Texture2D>("Sprites/Mountain30%"); break;
                case 4: mountain = Content.Load<Texture2D>("Sprites/Mountain40%"); break;
                case 5: mountain = Content.Load<Texture2D>("Sprites/Mountain50%"); break;
                case 6: mountain = Content.Load<Texture2D>("Sprites/Mountain60%"); break;
                case 7: mountain = Content.Load<Texture2D>("Sprites/Mountain70%"); break;
                case 8: mountain = Content.Load<Texture2D>("Sprites/Mountain80%"); break;
                case 9: mountain = Content.Load<Texture2D>("Sprites/Mountain90%"); break;
                case 10: mountain = Content.Load<Texture2D>("Sprites/Mountain"); break;
            }

            if (((Manager)ScreenManager).Hand == "Left")
            {
                plane = Content.Load<Texture2D>("Sprites/biplaneRight");
            }
            else
            {
                plane = Content.Load<Texture2D>("Sprites/biplaneLeft");
            }

            airport = Content.Load<Texture2D>("Sprites/airport");

            explosion = Content.Load<Texture2D>("Sprites/Explosion");

            cursor = Content.Load<Texture2D>("Sprites/cursor");

            int x = (ScreenManager.GraphicsDevice.Viewport.Width / 2) - (mountain.Width / 2);
            int y = ScreenManager.GraphicsDevice.Viewport.Height - ((int)ScreenManager.ScaleYPosition(20.0f) + mountain.Height);

            mountainRect = new Rectangle(x, y, mountain.Width, mountain.Height);

            int leftX = (int)ScreenManager.ScaleXPosition(20.0f);
            int rightX = (int)ScreenManager.ScaleXPosition(1900.0f) - airport.Width;
            y = ScreenManager.GraphicsDevice.Viewport.Height - (int)ScreenManager.ScaleYPosition(120.0f);

            if (((Manager)ScreenManager).Hand == "Left")
            {
                planeRect = new Rectangle(leftX, y, plane.Width, plane.Height);
                airportRect = new Rectangle(rightX, y, airport.Width, airport.Height);
            }
            else
            {
                planeRect = new Rectangle(rightX, y, plane.Width, plane.Height);
                airportRect = new Rectangle(leftX, y, airport.Width, airport.Height);
            }

            cursorRect = new Rectangle(0, 0, cursor.Width, cursor.Height);

            mountainCollision = new uint[mountain.Width * mountain.Height];
            planeCollision = new uint[plane.Width * plane.Height];
            airportCollision = new uint[airport.Width * airport.Height];
            cursorCollision = new uint[cursor.Width * cursor.Height];

            mountain.GetData<uint>(mountainCollision);
            plane.GetData<uint>(planeCollision);
            airport.GetData<uint>(airportCollision);
            cursor.GetData<uint>(cursorCollision);

            planeSound = Content.Load<SoundEffect>("Sound/plane");
            crashSound = Content.Load<SoundEffect>("Sound/vehicleExplosion");

            planeSoundInstance = planeSound.CreateInstance();
            planeSoundInstance.IsLooped = true;

            planeSoundInstance.Play();
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            timeInGame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (bIsCarried)
            {
                planeRect.X = state.X - (plane.Width / 2);
                planeRect.Y = state.Y - (plane.Height / 2);
            }

            else
            {
                cursorRect.X = state.X - (cursor.Width / 2);
                cursorRect.Y = state.Y - (cursor.Height / 2);
            }

            bCollision = GameLibrary.Collisions.PerPixelCollision(planeRect, mountainRect, planeCollision, mountainCollision);

            if (GameLibrary.Collisions.PerPixelCollision(planeRect, cursorRect, planeCollision, cursorCollision))
            {
                bIsCarried = true;
            }

            if (GameLibrary.Collisions.PerPixelCollision(planeRect, airportRect, planeCollision, airportCollision))
            {
                UpdateScore();
                bIsCarried = false;

                if (((Manager)ScreenManager).Hand == "Left")
                {
                    planeRect = new Rectangle((int)ScreenManager.ScaleXPosition(20.0f), ScreenManager.GraphicsDevice.Viewport.Height - (int)ScreenManager.ScaleYPosition(120.0f), plane.Width, plane.Height);
                }
                else
                {
                    planeRect = new Rectangle((int)ScreenManager.ScaleXPosition(1900.0f) - airport.Width, ScreenManager.GraphicsDevice.Viewport.Height - (int)ScreenManager.ScaleYPosition(120.0f), plane.Width, plane.Height);
                }
            }

            if (bCollision && !bContinuedCollision)
            {
                crashes++;
                crashSound.Play();
            }

            bContinuedCollision = bCollision;

            base.Update(gameTime);
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            Vector2 point;

            state = input.MouseState;

            foreach (Skeleton skeleton in ScreenManager.Input.SkeletonData)
            {
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    if (((Manager)ScreenManager).Hand == "Left")
                    {
                        point = ScreenManager.Input.SkeletonToColorMap(skeleton.Joints[JointType.HandLeft].Position);
                    }

                    else
                    {
                        point = ScreenManager.Input.SkeletonToColorMap(skeleton.Joints[JointType.HandRight].Position);
                    }

                    Vector2 calibratedPoint = ((Manager)ScreenManager).kTrackedObj.TranslatePoint(point);

                    state = new MouseState((int)calibratedPoint.X, (int)calibratedPoint.Y, state.ScrollWheelValue, state.LeftButton, state.MiddleButton, state.RightButton, state.XButton1, state.XButton2);

                    break;
                }
            }

            base.HandleInput(gameTime, input);
        }

        /// <summary>
        /// Updates the score. If the score == repetitions, sets the win game
        /// to true.
        /// </summary>
        private void UpdateScore()
        {
            score++;

            if (score == repetitions)
            {
                ((Manager)this.ScreenManager).Time = timeInGame;

                //TODO
                ((Manager)this.ScreenManager).Missed = crashes;

                planeSoundInstance.Stop();

                EndScreen es = new EndScreen();

                this.ScreenManager.AddScreen(es, false);

                this.ScreenManager.RemoveScreen(this);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            SpriteBatch.Draw(bg, fullscreen, Color.White);

            SpriteBatch.Draw(mountain, mountainRect, Color.White);

            SpriteBatch.Draw(airport, airportRect, Color.White);

            if (!bIsCarried)
            {
                SpriteBatch.Draw(cursor, cursorRect, Color.White);
            }

            if (bCollision || bContinuedCollision)
            {
                SpriteBatch.Draw(explosion, planeRect, Color.White);
            }
            else
            {
                SpriteBatch.Draw(plane, planeRect, Color.White);
            }

            SpriteBatch.DrawString(gameFont, score.ToString() + "/" + repetitions.ToString(), scorePosition, Color.Red);

            SpriteBatch.End();
        }
    }
}