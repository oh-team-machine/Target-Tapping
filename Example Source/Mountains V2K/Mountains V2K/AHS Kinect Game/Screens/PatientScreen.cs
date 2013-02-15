using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AHS_Kinect_Game.Screens
{
    class PatientScreen : GameLibrary.Screen
    {
        private ContentManager Content;

        private GameLibrary.UI.List patientList;

        private GameLibrary.UI.Button selectButton;
        private GameLibrary.UI.Button cancelButton;

        private Texture2D patientBackground;
        private Rectangle patientRect;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your Content.
        /// </summary>
        public override void LoadContent()
        {
            Content = new ContentManager(ScreenManager.Game.Services, "Content");

            patientBackground = Content.Load<Texture2D>("GUI/MenuBackground");

            //Buttons are 300 x 75
            Texture2D selectText = Content.Load<Texture2D>("GUI/SelectButton");
            Texture2D cancelText = Content.Load<Texture2D>("GUI/CancelButton");

            int menuX = (ScreenManager.GraphicsDevice.Viewport.Width / 2) - (patientBackground.Width / 2);
            int menuY = (ScreenManager.GraphicsDevice.Viewport.Height / 2) - (patientBackground.Height / 2);

            patientRect = new Rectangle(menuX, menuY, patientBackground.Width, patientBackground.Height);

            Rectangle selectRect = new Rectangle(menuX, menuY + patientBackground.Height, selectText.Width, selectText.Height);
            Rectangle cancelRect = new Rectangle(menuX + (patientBackground.Width - cancelText.Width), menuY + patientBackground.Height, selectText.Width, selectText.Height);

            selectButton = new GameLibrary.UI.Button(selectText, selectRect);
            cancelButton = new GameLibrary.UI.Button(cancelText, cancelRect);

            Vector2 position = new Vector2(menuX + ((patientBackground.Width / 2) - 100), menuY + 50);

            Texture2D dummyTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });

            SpriteFont font = Content.Load<SpriteFont>("Font");
            patientList = new GameLibrary.UI.List(LoadList(), 200, 15, 28, position, dummyTexture, font);
        }

        /// <summary>
        /// Loads the patient list from an external SQLite database.
        /// </summary>
        /// <returns>A list of patient names from the database.</returns>
        private List<string> LoadList()
        {
            //File.Decrypt("../../Resources/AHS.s3db");

            GameLibrary.Database patientData = new GameLibrary.Database("../../Resources/AHS.s3db");
            //GameLibrary.Database patientData = new GameLibrary.Database("AHS.s3db");

            DataTable ids = patientData.Select("ID", "Users");

            List<String> patientIDS = new List<String>();

            foreach (DataRow dr in ids.Rows)
            {
                patientIDS.Add(dr[0].ToString());
            }

            //File.Encrypt("../../Resources/AHS.s3db");

            Cursor.Current = Cursors.Default;

            return patientIDS;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all Content.
        /// </summary>
        public override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (selectButton.IsClicked())
            {
                ((Manager)ScreenManager).PatientName = patientList.SelectedElement();

                ScreenManager.RemoveScreen(this);
            }

            if (cancelButton.IsClicked())
            {
                ScreenManager.RemoveScreen(this);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the screen to accept and handle input.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="input">The input manager that handles the different hardware inputs.</param>
        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime, GameLibrary.InputManager input)
        {
            MouseState MouseState = input.MouseState;

            selectButton.Update(MouseState);
            cancelButton.Update(MouseState);

            patientList.Update(gameTime, MouseState);

            base.HandleInput(gameTime, input);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="transform">A matrix representing the screen transformation for scaling and drawing purposes.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch SpriteBatch = ScreenManager.SpriteBatch;

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            SpriteBatch.Draw(patientBackground, patientRect, Color.White);

            selectButton.Draw(SpriteBatch);
            cancelButton.Draw(SpriteBatch);
            patientList.Draw(SpriteBatch);

            SpriteBatch.End();
        }
    }
}
