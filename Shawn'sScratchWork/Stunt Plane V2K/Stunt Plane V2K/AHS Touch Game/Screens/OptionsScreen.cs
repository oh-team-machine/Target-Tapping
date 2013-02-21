using System;
using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stunt_Plane_V2K.Screens
{
    class OptionsScreen : GameLibrary.Screen
    {
        private ContentManager content;

        private Texture2D optionsBackground;
        private Rectangle optionsRect;

        private GameLibrary.UI.Button selectButton;
        private GameLibrary.UI.Button cancelButton;

        private GameLibrary.UI.NumericUpDown ringsUpDown;
        private GameLibrary.UI.NumericUpDown speedUpDown;
        private GameLibrary.UI.NumericUpDown repetitionUpDown;

        private GameLibrary.UI.List handList;

        private GameLibrary.UI.Label ringsLabel;
        private GameLibrary.UI.Label speedLabel;
        private GameLibrary.UI.Label repetitionLabel;
        private GameLibrary.UI.Label handLabel;

        public override void LoadContent()
        {
            content = new ContentManager(ScreenManager.Game.Services, "Content");

            optionsBackground = content.Load<Texture2D>("GUI/MenuBackground");

            //Buttons are 300 x 75
            Texture2D selectText = content.Load<Texture2D>("GUI/SelectButton");
            Texture2D cancelText = content.Load<Texture2D>("GUI/CancelButton");

            int menuX = (ScreenManager.GraphicsDevice.Viewport.Width / 2) - (optionsBackground.Width / 2);
            int menuY = (ScreenManager.GraphicsDevice.Viewport.Height / 2) - (optionsBackground.Height / 2);

            optionsRect = new Rectangle(menuX, menuY, optionsBackground.Width, optionsBackground.Height);

            Rectangle selectRect = new Rectangle(menuX, menuY + optionsBackground.Height, selectText.Width, selectText.Height);
            Rectangle cancelRect = new Rectangle(menuX + (optionsBackground.Width - cancelText.Width), menuY + optionsBackground.Height, selectText.Width, selectText.Height);

            selectButton = new GameLibrary.UI.Button(selectText, selectRect);
            cancelButton = new GameLibrary.UI.Button(cancelText, cancelRect);

            Vector2 position = new Vector2(menuX + ((optionsBackground.Width / 2)), menuY + ScreenManager.ScaleYPosition(50));

            Texture2D dummyTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });

            SpriteFont font = content.Load<SpriteFont>("Font");

            ringsUpDown = new GameLibrary.UI.NumericUpDown(dummyTexture, position, GameLibrary.UI.Label.CENTER, 100.0f,font, 1m, 1m, 10m, 1m);
            
            position.Y += ScreenManager.ScaleYPosition(50);

            speedUpDown = new GameLibrary.UI.NumericUpDown(dummyTexture, position, GameLibrary.UI.Label.CENTER, 100.0f, font, 1m, 1m, 10m, 1m);

            position.Y += ScreenManager.ScaleYPosition(50);

            repetitionUpDown = new GameLibrary.UI.NumericUpDown(dummyTexture, position, GameLibrary.UI.Label.CENTER, 100.0f, font, 1m, 1m, 1000m, 1m);
            
            position.Y += ScreenManager.ScaleYPosition(75);

            List<String> hands = new List<String> { "Right", "Left" };

            handList = new GameLibrary.UI.List(hands, 175, 2, 25, position, dummyTexture, font, Color.Black, Color.White);

            position = new Vector2(menuX + ((optionsBackground.Width / 2)) - ScreenManager.ScaleXPosition(300), menuY + ScreenManager.ScaleYPosition(50));

            ringsLabel = new GameLibrary.UI.Label("Number of Rings:", position, GameLibrary.UI.Label.LEFT, 200.0f, font);

            position.Y += ScreenManager.ScaleYPosition(50);

            speedLabel = new GameLibrary.UI.Label("Speed:", position, GameLibrary.UI.Label.LEFT, 200.0f, font);

            position.Y += ScreenManager.ScaleYPosition(50);

            repetitionLabel = new GameLibrary.UI.Label("Number of Reps:", position, GameLibrary.UI.Label.LEFT, 200.0f, font);

            position.Y += ScreenManager.ScaleYPosition(85);

            handLabel = new GameLibrary.UI.Label("Hand Used:", position, GameLibrary.UI.Label.LEFT, 200.0f, font);
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (selectButton.IsClicked())
            {
                ((Manager)ScreenManager).Hand = handList.SelectedElement();

                ((Manager)ScreenManager).Speed = (int)speedUpDown.Value;
                ((Manager)ScreenManager).NumberOfRings = (int)ringsUpDown.Value;
                ((Manager)ScreenManager).Repetitions = (int)repetitionUpDown.Value;

                CalibrationScreen cs = new CalibrationScreen();

                ScreenManager.AddScreen(cs, false);

                this.ScreenState = State.Hidden;
            }

            if (cancelButton.IsClicked())
            {
                ScreenManager.RemoveScreen(this);
            }

            base.Update(gameTime);
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            MouseState mouseState = input.MouseState;

            selectButton.Update(mouseState);
            cancelButton.Update(mouseState);

            ringsUpDown.Update(gameTime, mouseState);
            speedUpDown.Update(gameTime, mouseState);
            repetitionUpDown.Update(gameTime, mouseState);
            
            handList.Update(gameTime, mouseState);

            base.HandleInput(gameTime, input);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch SpriteBatch = ScreenManager.SpriteBatch;

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            SpriteBatch.Draw(optionsBackground, optionsRect, Color.White);

            ringsUpDown.Draw(SpriteBatch);
            speedUpDown.Draw(SpriteBatch);
            repetitionUpDown.Draw(SpriteBatch);

            handList.Draw(SpriteBatch);

            ringsLabel.Draw(SpriteBatch);
            speedLabel.Draw(SpriteBatch);
            repetitionLabel.Draw(SpriteBatch);
            handLabel.Draw(SpriteBatch);

            selectButton.Draw(SpriteBatch);
            cancelButton.Draw(SpriteBatch);

            SpriteBatch.End();
        }
    }
}