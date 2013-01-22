using System;
using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stunt_Plane_V2K.Screens
{
    class EndScreen : GameLibrary.Screen
    {
        private ContentManager Content;

        private MouseState state;

        private SpriteFont font;
        private SpriteFont winFont;

        private Vector2 line1Position;
        private Vector2 line2Position;
        private Vector2 line3Position;
        private Vector2 line4Position;
        
        private String line1;
        private String line2;
        private String line3;
        private String line4;

        private GameLibrary.UI.Button replayButton;
        private GameLibrary.UI.Button exitButton;

        public override void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Game.Services, "Content");

            font = Content.Load<SpriteFont>("Font");
            winFont = Content.Load<SpriteFont>("WinFont");

            line1 = "You Win!!!";
            line2 = "Play Time: ";
            line3 = "Number of Rings Flown Through: ";
            line4 = "Number of Rings Missed: ";

            float xCenter = ScreenManager.GraphicsDevice.PresentationParameters.BackBufferWidth / 2.0f;
            float yCenter = ScreenManager.GraphicsDevice.PresentationParameters.BackBufferHeight / 2.5f;

            line1Position = new Vector2((xCenter - font.MeasureString(line1).X), yCenter);
            line2Position = new Vector2((xCenter - font.MeasureString(line2 + "99:99:99.99").X / 2.0f), (line1Position.Y + font.MeasureString(line1).Y*2));
            line3Position = new Vector2((xCenter - font.MeasureString(line3 + "999").X / 2.0f), (line2Position.Y + font.MeasureString(line2).Y));
            line4Position = new Vector2((xCenter - font.MeasureString(line4 + "999").X / 2.0f), (line3Position.Y + font.MeasureString(line3).Y));

            Texture2D exitText = Content.Load<Texture2D>("GUI/ExitButton");
            Texture2D replayText = Content.Load<Texture2D>("GUI/ReplayButton");

            float buttonSpace = this.ScreenManager.ScaleXPosition(50.0f);
            float buttonX = xCenter - (buttonSpace / 2.0f) - (float)exitText.Width;
            float buttonY = ScreenManager.GraphicsDevice.PresentationParameters.BackBufferHeight / 1.25f;

            Rectangle exitRect = new Rectangle((int)buttonX, (int)buttonY, exitText.Width, exitText.Height);
            Rectangle replayRect = new Rectangle((int)(buttonX + exitText.Width + buttonSpace), (int)buttonY, replayText.Width, replayText.Height);

            exitButton = new GameLibrary.UI.Button(exitText, exitRect);
            replayButton = new GameLibrary.UI.Button(replayText, replayRect);
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            exitButton.Update(state);
            replayButton.Update(state);

            if (exitButton.IsClicked())
            {
                SaveResults();
                ScreenManager.Exit();
            }

            if (replayButton.IsClicked())
            {
                SaveResults();
                ScreenManager.RemoveScreen(this);
            }
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            state = input.MouseState;

            base.HandleInput(gameTime, input);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch SpriteBatch = this.ScreenManager.SpriteBatch;

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            SpriteBatch.DrawString(winFont, line1, line1Position, Color.White);
            SpriteBatch.DrawString(font, line2 + GameLibrary.HelpfulMethods.FormatTime(((Manager)this.ScreenManager).Time), line2Position, Color.White);
            SpriteBatch.DrawString(font, line3 + ((Manager)this.ScreenManager).Repetitions.ToString(), line3Position, Color.White);
            SpriteBatch.DrawString(font, line4 + ((Manager)this.ScreenManager).Missed.ToString(), line4Position, Color.White);

            exitButton.Draw(SpriteBatch);
            replayButton.Draw(SpriteBatch);

            SpriteBatch.End();
        }

        private void SaveResults()
        {
            //File.Decrypt("../../Resources/AHS.s3db");

            GameLibrary.Database db = new GameLibrary.Database("../../Resources/AHS.s3db");
            //GameLibrary.Database db = new GameLibrary.Database("AHS.s3db");

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("ID", ((Manager)ScreenManager).PatientName);
            dictionary.Add("Score", ((Manager)ScreenManager).Repetitions.ToString());
            dictionary.Add("Missed", ((Manager)ScreenManager).Missed.ToString());
            dictionary.Add("Time", ((Manager)ScreenManager).Time.ToString());
            dictionary.Add("Hand", ((Manager)ScreenManager).Hand);
            dictionary.Add("Speed", ((Manager)ScreenManager).Speed.ToString());
            dictionary.Add("NumRings", ((Manager)ScreenManager).NumberOfRings.ToString());
                
            db.Insert("Stunt", dictionary);
            //File.Encrypt("../../Resources/AHS.s3db");

        }
    }
}
