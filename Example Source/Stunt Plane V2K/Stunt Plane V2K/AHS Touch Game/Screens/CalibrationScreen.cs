using GameLibrary;
using GameLibrary.UI;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stunt_Plane_V2K.Screens
{
    class CalibrationScreen: GameLibrary.Screen
    {
        #region Variables

        private ContentManager Content;
        private SpriteBatch SpriteBatch;
        
        private MouseState state;

        private KinectView view;

        private GameLibrary.UI.Button buttonUL;
        private GameLibrary.UI.Button buttonUR;
        private GameLibrary.UI.Button buttonLL;
        private GameLibrary.UI.Button buttonLR;

        private GameLibrary.UI.Button selectButton;

        private Vector2 UR;
        private Vector2 UL;
        private Vector2 LR;
        private Vector2 LL;

        private Quadrilateral trackedArea;

        private Texture2D cursor;

        private KinectTrackedObject trackedLimb;

        private Vector2 point;
        private Vector2 calibratedPoint;

        #endregion Variables

        public override void LoadContent()
        {
            point = Vector2.Zero;
            calibratedPoint = Vector2.Zero;

            Vector2 viewLocation = new Vector2((ScreenManager.GraphicsDevice.Viewport.Width / 2.0f) - 320, (ScreenManager.GraphicsDevice.Viewport.Height / 2.0f) - 240);

            view = new KinectView(ScreenManager.Input.Sensor, ScreenManager.GraphicsDevice, viewLocation); 

            UR = UL = LR = LL = Vector2.Zero;

            trackedArea = new Quadrilateral(UL, UR, LL, LR, 3, Color.Red, ScreenManager.GraphicsDevice);

            trackedLimb = new KinectTrackedObject(UL, UR, LL, LR);

            Content = new ContentManager(ScreenManager.Game.Services, "Content");
            SpriteBatch = ScreenManager.SpriteBatch;

            if (((Manager)ScreenManager).Hand == "Left")
            {
                cursor = Content.Load<Texture2D>("Sprites/Left Hand");
            }

            else
            {
                cursor = Content.Load<Texture2D>("Sprites/Right Hand");
            }

            Texture2D textUR = Content.Load<Texture2D>("GUI/URButton");
            Texture2D textUL = Content.Load<Texture2D>("GUI/ULButton");
            Texture2D textLR = Content.Load<Texture2D>("GUI/LRButton");
            Texture2D textLL = Content.Load<Texture2D>("GUI/LLButton");

            Texture2D textSelect = Content.Load<Texture2D>("GUI/SelectButton");

            float xOffset = ScreenManager.ScaleXPosition(ScreenManager.GraphicsDevice.Viewport.Width - textUR.Width);
            float yOffset = ScreenManager.ScaleYPosition(ScreenManager.GraphicsDevice.Viewport.Height - textUR.Height);

            float middle = ScreenManager.ScaleXPosition((ScreenManager.GraphicsDevice.Viewport.Width / 2.0f) - (textUR.Width / 2.0f));

            Rectangle rectUL = new Rectangle(0, 0, textUR.Width, textUL.Height);
            Rectangle rectUR = new Rectangle((int)xOffset, 0, textUR.Width, textUR.Height);
            Rectangle rectLL = new Rectangle(0, (int)yOffset, textLR.Width, textLR.Height);
            Rectangle rectLR = new Rectangle((int)xOffset, (int)yOffset, textLL.Width, textLL.Height);

            Rectangle rectSelect = new Rectangle((int)middle, (int)yOffset, textSelect.Width, textSelect.Height);

            buttonUR = new GameLibrary.UI.Button(textUR, rectUR);
            buttonUL = new GameLibrary.UI.Button(textUL, rectUL);
            buttonLR = new GameLibrary.UI.Button(textLR, rectLR);
            buttonLL = new GameLibrary.UI.Button(textLL, rectLL);

            selectButton = new GameLibrary.UI.Button(textSelect, rectSelect);
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void  Update(GameTime gameTime)
        {
            view.Update();

            buttonUR.Update(state);
            buttonUL.Update(state);
            buttonLR.Update(state);
            buttonLL.Update(state);

            selectButton.Update(state);

            if (ScreenManager.Input.SkeletonData != null)
            {
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

                        calibratedPoint = trackedLimb.TranslatePoint(point);

                        break;
                    }
                }

                if (buttonUR.IsClicked())
                {
                    UR = point;
                    trackedArea = new Quadrilateral(UL, UR, LL, LR, 3, Color.Red, ScreenManager.GraphicsDevice);

                    view.UpdateTrackedArea(trackedArea);

                    trackedLimb = new KinectTrackedObject(UL, UR, LL, LR);
                }

                if (buttonUL.IsClicked())
                {
                    UL = point;
                    trackedArea = new Quadrilateral(UL, UR, LL, LR, 3, Color.Red, ScreenManager.GraphicsDevice);

                    view.UpdateTrackedArea(trackedArea);

                    trackedLimb = new KinectTrackedObject(UL, UR, LL, LR);
                }

                if (buttonLR.IsClicked())
                {
                    LR = point;
                    trackedArea = new Quadrilateral(UL, UR, LL, LR, 3, Color.Red, ScreenManager.GraphicsDevice);

                    view.UpdateTrackedArea(trackedArea);

                    trackedLimb = new KinectTrackedObject(UL, UR, LL, LR);
                }

                if (buttonLL.IsClicked())
                {
                    LL = point;
                    trackedArea = new Quadrilateral(UL, UR, LL, LR, 3, Color.Red, ScreenManager.GraphicsDevice);

                    view.UpdateTrackedArea(trackedArea);

                    trackedLimb = new KinectTrackedObject(UL, UR, LL, LR);
                }

                if (selectButton.IsClicked())
                {
                    ((Manager)ScreenManager).kTrackedObj = trackedLimb;

                    GameScreen gs = new GameScreen();

                    ScreenManager.AddScreen(gs, false);

                    ScreenManager.RemoveScreen(this);
                }
            }
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Matrix transform)
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, transform);

            buttonUR.Draw(SpriteBatch);
            buttonUL.Draw(SpriteBatch);
            buttonLR.Draw(SpriteBatch);
            buttonLL.Draw(SpriteBatch);
            selectButton.Draw(SpriteBatch);

            view.Draw(SpriteBatch);

            SpriteBatch.Draw(cursor, calibratedPoint, Color.White);

            SpriteBatch.End();

        }

        public override void  HandleInput(GameTime gameTime, InputManager input)
        {
            state = input.MouseState;
 	        base.HandleInput(gameTime, input);
        }
    }
}
