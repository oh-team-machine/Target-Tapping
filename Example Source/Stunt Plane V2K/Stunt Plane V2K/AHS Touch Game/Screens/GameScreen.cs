using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Kinect;

namespace Stunt_Plane_V2K.Screens
{
    class GameScreen : GameLibrary.Screen
    {
        #region Variables

        private ContentManager Content;

        private SpriteBatch SpriteBatch;
        private SpriteBatch backgroundBatch;

        private Plane plane;
        private Rings rings;

        private Texture2D bg;
        private Rectangle fullscreen;

        private MovingBackground mg;
        private MovingBackground fg;

        private Texture2D ringFront;
        private Texture2D ringBack;
        private Texture2D ringFrontCleared;
        private Texture2D ringBackCleared;

        private SoundEffect ringSound;

        private SpriteFont gameFont;

        private MouseState state;

        private float timeInGame;

        private int score;
        private int speed;
        private int numRings;
        private int repetitions;

        private Vector2 scorePosition;

        #endregion Variables

        public override void Initialize()
        {
            timeInGame = 0.0f;

            //Setup Values
            speed = ((Manager)ScreenManager).Speed;
            numRings = ((Manager)ScreenManager).NumberOfRings;
            repetitions = ((Manager)ScreenManager).Repetitions;

            //Tracked during the game
            score = 0;

            fullscreen = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);

            ScreenManager.Game.IsMouseVisible = false;

            base.Initialize();
        }

        public override void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Game.Services, "Content");

            SpriteBatch = ScreenManager.SpriteBatch;
            backgroundBatch = new SpriteBatch(ScreenManager.GraphicsDevice);

            bg = Content.Load<Texture2D>("Sprites/background");
            mg = new MovingBackground(Content.Load<Texture2D>("Sprites/middleground"), ScreenManager.GraphicsDevice.Viewport, speed/2);
            fg = new MovingBackground(Content.Load<Texture2D>("Sprites/foreground"), ScreenManager.GraphicsDevice.Viewport, speed);

            plane = new Plane(Content.Load<Texture2D>("Sprites/plane"), Content.Load<SoundEffect>("Sound/plane"));

            ringFront = Content.Load<Texture2D>("Sprites/ringFront");
            ringBack = Content.Load<Texture2D>("Sprites/ringBack");
            ringFrontCleared = Content.Load<Texture2D>("Sprites/ringFrontCleared");
            ringBackCleared = Content.Load<Texture2D>("Sprites/ringBackCleared");

            ringSound = Content.Load<SoundEffect>("Sound/ringDoDoDo");

            rings = new Rings(ringFront, ringBack, ringFrontCleared, ringBackCleared, numRings, speed, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height, ringSound);

            gameFont = Content.Load<SpriteFont>("Font");

            float scoreLength = (gameFont.MeasureString("999/999")).X;
            scorePosition = new Vector2(this.ScreenManager.ScaleXPosition((this.ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth / 2.0f) - (scoreLength / 2.0f)), this.ScreenManager.ScaleYPosition(20.0f));

            plane.playSound();
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            timeInGame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            plane.Update(state);
            rings.Update(gameTime);

            mg.Update();
            fg.Update();

            for (int i = 0; i < rings.ringRects.Count; i++)
            {
                if (rings.ringCanCollide[i])
                {
                    if (GameLibrary.Collisions.PerPixelCollision(plane.planeRect, rings.ringRects[i], plane.planeCollision, rings.ringCollision))
                    {
                        UpdateScore();
                        rings.ringCanCollide[i] = false;
                        rings.playSound();
                    }
                }
            }

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
                ((Manager)this.ScreenManager).Missed = rings.numMissed;

                EndScreen es = new EndScreen();

                this.ScreenManager.AddScreen(es, false);

                plane.pauseSound();

                this.ScreenManager.RemoveScreen(this);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            backgroundBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transform);

            backgroundBatch.Draw(bg, fullscreen, Color.White);
            mg.Draw(backgroundBatch);
            fg.Draw(backgroundBatch);

            backgroundBatch.End();

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, transform);

            plane.Draw(SpriteBatch);
            rings.Draw(SpriteBatch);

            SpriteBatch.DrawString(gameFont, score.ToString() + "/" + repetitions.ToString(), scorePosition, Color.White);

            SpriteBatch.End();
        }
    }
}