using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Wack_A_Mole_V4.Screens
{
    class GameScreen : GameLibrary.Screen
    {
        #region Variables

        private ContentManager content;

        private bool bSoundPlaying;

        private float timeInGame;

        private System.Random generator = new System.Random();

        private Texture2D background;
        private Texture2D mole;
        private Texture2D wackedMole;
        private Texture2D hammer;
        private Texture2D hammerStrike;

        private SoundEffect hammerSound;
        private SoundEffect moleSound;

        private SpriteFont font;

        private uint[] hammerCollisionArray;
        private uint[] moleCollisionArray;

        private Vector2 hammerPosition;
        private Vector2 scorePosition;

        private List<Vector2> molePositions;

        private Rectangle hammerRect;
        private Rectangle backgroundRect;

        private List<Rectangle> moleRectangles;

        private List<bool> moleClicked;
        private List<bool> molePresent;

        private int rand;
        private int previousRand;

        private double moleTime;
        private double currentMoleTime;

        private int score;
        private int repetitions;

        private int molesMissed;
        private int numMolesHit;

        private int numMolesOnScreen;

        private MouseState state;

        #endregion Variables

        public override void Initialize()
        {
            timeInGame = 0.0f;

            moleClicked = new List<bool>();
            molePresent = new List<bool>();

            for (int i = 0; i < 6; i++)
            {
                moleClicked.Add(false);
                molePresent.Add(false);
            }

            rand = -1;
            previousRand = -1;

            //Setup Values
            moleTime = ((Manager)ScreenManager).MoleTimer;
            numMolesOnScreen = ((Manager)ScreenManager).MoleNumber;
            repetitions = ((Manager)ScreenManager).Repetitions;

            //Tracked during the game
            currentMoleTime = 0;
            score = 0;
            numMolesHit = 0;

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundRect = new Rectangle(0, 0, viewport.Width, viewport.Height);

            ScreenManager.Game.IsMouseVisible = false;

            base.Initialize();
        }

        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Game.Services, "Content");

            background = content.Load<Texture2D>("Sprites/WhackAMoleBackground");
            mole = content.Load<Texture2D>("Sprites/Mole");
            wackedMole = content.Load<Texture2D>("Sprites/WackedMole");
            hammer = content.Load<Texture2D>("Sprites/Hammer");
            hammerStrike = content.Load<Texture2D>("Sprites/HammerStrike");

            hammerCollisionArray = new uint[hammer.Width * hammer.Height];
            moleCollisionArray = new uint[mole.Width * mole.Height];

            hammer.GetData(hammerCollisionArray);
            mole.GetData(moleCollisionArray);

            hammerRect = new Rectangle(0, 0, hammer.Width, hammer.Height);

            initializeMolePositions();

            moleRectangles = new List<Rectangle>();

            for (int i = 0; i < 6; i++)
            {
                moleRectangles.Add(new Rectangle((int)molePositions[i].X, (int)molePositions[i].Y, mole.Width, mole.Height));
            }

            font = content.Load<SpriteFont>("Font");

            hammerSound = content.Load<SoundEffect>("Sound/hammerHit");
            moleSound = content.Load<SoundEffect>("Sound/molePop");

            float scoreLength = (font.MeasureString("999/999")).X;
            scorePosition = new Vector2(this.ScreenManager.ScaleXPosition((this.ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth / 2.0f) - (scoreLength / 2.0f)), this.ScreenManager.ScaleYPosition(20.0f));

            nextMoles();
            molesMissed = 0;
        }

        private void initializeMolePositions()
        {
            float xScale = (float)((float)this.ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth / (float)background.Width);
            float yScale = (float)((float)this.ScreenManager.GraphicsDevice.PresentationParameters.BackBufferHeight / (float)background.Height);

            molePositions = new List<Vector2>();
            molePositions.Add(new Vector2(this.ScreenManager.ScaleXPosition((205 - mole.Width/2) * xScale), this.ScreenManager.ScaleYPosition((470 - mole.Height) * yScale)));
            molePositions.Add(new Vector2(this.ScreenManager.ScaleXPosition((820 - mole.Width/2) * xScale), this.ScreenManager.ScaleYPosition((460 - mole.Height) * yScale)));
            molePositions.Add(new Vector2(this.ScreenManager.ScaleXPosition((1180 - mole.Width/2) * xScale), this.ScreenManager.ScaleYPosition((550 - mole.Height) * yScale)));
            molePositions.Add(new Vector2(this.ScreenManager.ScaleXPosition((580 - mole.Width/2) * xScale), this.ScreenManager.ScaleYPosition((580 - mole.Height) * yScale)));
            molePositions.Add(new Vector2(this.ScreenManager.ScaleXPosition((305 - mole.Width/2) * xScale), this.ScreenManager.ScaleYPosition((710 - mole.Height) * yScale)));
            molePositions.Add(new Vector2(this.ScreenManager.ScaleXPosition((980 - mole.Width/2) * xScale), this.ScreenManager.ScaleYPosition((720 - mole.Height) * yScale)));
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //tracks total time
            timeInGame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            currentMoleTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (currentMoleTime >= moleTime)
            {
                nextMoles();
            }

            hammerPosition.X = state.X - hammer.Width/2;
            hammerPosition.Y = state.Y - hammer.Height/3;

            if (state.LeftButton == ButtonState.Pressed)
            {
                hammerRect.X = (int)hammerPosition.X;
                hammerRect.Y = (int)hammerPosition.Y;

                if (!bSoundPlaying)
                {
                    hammerSound.Play();
                    bSoundPlaying = true;
                }

                for (int i = 0; i < 6; i++)
                {
                    if (molePresent[i] && !moleClicked[i])
                    {
                        if (GameLibrary.Collisions.PerPixelCollision(moleRectangles[i], hammerRect, moleCollisionArray, hammerCollisionArray))
                        {
                            moleClicked[i] = true;
                            numMolesHit++;
                            updateScore();

                            if (numMolesHit >= numMolesOnScreen)
                                nextMoles();
                        }
                    }
                }
            }
            else
                bSoundPlaying = false;

            base.Update(gameTime);
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            state = input.MouseState;

            base.HandleInput(gameTime, input);
        }

        /// <summary>
        /// Resets the clicked state and present state of all moles, then
        /// assignes new moles to be present at random, up to the numMolesOnScreen.
        /// </summary>
        private void nextMoles()
        {
            moleSound.Play();

            if (numMolesHit < numMolesOnScreen)
            {
                molesMissed += (numMolesOnScreen - numMolesHit);
            }

            for (int i = 0; i < 6; i++)
            {
                molePresent[i] = false;
                moleClicked[i] = false;
            }

            rand = generator.Next(5);

            for (int i = 0; i < numMolesOnScreen; i++)
            {
                while (molePresent[rand] || previousRand == rand)
                    rand = generator.Next(6);

                switch (rand)
                {
                    case 0: molePresent[0] = true; break;
                    case 1: molePresent[1] = true; break;
                    case 2: molePresent[2] = true; break;
                    case 3: molePresent[3] = true; break;
                    case 4: molePresent[4] = true; break;
                    case 5: molePresent[5] = true; break;
                }

                previousRand = rand;
            }

            currentMoleTime = 0;
            numMolesHit = 0;
        }

        /// <summary>
        /// Updates the score. If the score == repetitions, sets the win game
        /// to true.
        /// </summary>
        private void updateScore()
        {
            score++;

            if (score == repetitions)
            {
                ((Manager)this.ScreenManager).Time = timeInGame;
                ((Manager)this.ScreenManager).Missed = molesMissed;

                EndScreen es = new EndScreen();

                this.ScreenManager.AddScreen(es, false);

                this.ScreenManager.RemoveScreen(this);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch SpriteBatch = ScreenManager.SpriteBatch;

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            SpriteBatch.Draw(background, backgroundRect, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

            for (int i = 0; i < 6; i++)
            {
                if (molePresent[i])
                {
                    if (moleClicked[i])
                        SpriteBatch.Draw(wackedMole, moleRectangles[i], Color.White);
                    else
                        SpriteBatch.Draw(mole, moleRectangles[i], Color.White);
                }
            }

            //TODO
            SpriteBatch.DrawString(font, score.ToString() + "/" + repetitions.ToString(), scorePosition, Color.White);

            if (state.LeftButton == ButtonState.Pressed)
                SpriteBatch.Draw(hammerStrike, hammerPosition, Color.White);
            else
                SpriteBatch.Draw(hammer, hammerPosition, Color.White);

            SpriteBatch.End();
        }
    }
}