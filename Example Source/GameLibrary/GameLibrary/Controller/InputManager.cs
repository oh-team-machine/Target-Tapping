
using System;
using System.IO;
using GameLibrary.Model;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Windows7.Multitouch;
using Windows7.Multitouch.Win32Helper;
using System.Collections;

namespace GameLibrary
{
    /// <summary>
    /// This class manages Mouse and Keyboard input for XNA games.
    /// </summary>
    public class InputManager
    {        
        private struct TouchPoint
        {
            public int X;
            public int Y;
            public bool pressed;
        }

        /// <summary>
        /// The current MouseState
        /// </summary>
        public MouseState MouseState { get; set; }

        /// <summary>
        /// The current KeyboardState
        /// </summary>
        public KeyboardState KeyState { get; set; }

        private TouchHandler touchHandler;
        private TouchPoint point;
        private int currentPointID;

        private Queue touchQueue;

        /// <summary>
        /// The current state of Touch. Enable if using a touch device.
        /// </summary>
        public bool TouchEnabled { get; set; }

        /// <summary>
        /// The current state of Kinect. Enable if using a Kinect.
        /// </summary>
        public bool KinectEnabled { get; set; }

        /// <summary>
        /// The Kinect Sensor.
        /// </summary>
        public KinectSensor Sensor { get; set; }

        /// <summary>
        /// The array of Skeletons from the skeleton frame data.
        /// </summary>
        public Skeleton[] SkeletonData { get; private set; }

        private KinectStatus lastStatus;
        
        private bool bInverted;

        private Vector2 screen;

        /// <summary>
        /// The constructor for the InputManager class. Initializes a new mouse and keyboard state,
        /// as well as sets up the status of Kinect and Touch input.
        /// </summary>
        public InputManager(Vector2 ScreenResolution)
        {
            MouseState = new MouseState();
            KeyState = new KeyboardState();

            TouchEnabled = false;
            KinectEnabled = false;

            screen = ScreenResolution;

            bInverted = false;

            currentPointID = -1;
        }

        /// <summary>
        /// Updates the mouse and keyboard state with GetState()
        /// </summary>
        public void Update()
        {
            if (TouchEnabled)
            {
                if (touchQueue.Count > 0)
                {
                    TouchPoint temp = (TouchPoint)touchQueue.Dequeue();

                    if (temp.pressed)
                    {
                        MouseState = new MouseState(temp.X, temp.Y, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                    }

                    else
                    {
                        MouseState = new MouseState(temp.X, temp.Y, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                    }
                }
            }

            else
            {
                MouseState = Mouse.GetState();

                if (bInverted)
                {
                    MouseState = new MouseState((int)screen.X - MouseState.X, (int)screen.Y - MouseState.Y, 0, MouseState.LeftButton, MouseState.MiddleButton, MouseState.RightButton, MouseState.XButton1, MouseState.XButton2);
                }
            }

            if (KinectEnabled)
            {
                if (Sensor == null || !Sensor.IsRunning || Sensor.Status != KinectStatus.Connected)
                {
                    return;
                }

                using (SkeletonFrame skeletonFrame = Sensor.SkeletonStream.OpenNextFrame(0))
                {
                    // Sometimes we get a null frame back if no data is ready
                    if (skeletonFrame == null)
                    {
                        return;
                    }

                    // Reallocate if necessary
                    if (SkeletonData == null || SkeletonData.Length != skeletonFrame.SkeletonArrayLength)
                    {
                        SkeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }

                    skeletonFrame.CopySkeletonDataTo(SkeletonData);
                }
            }

            KeyState = Keyboard.GetState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inverted"></param>
        public void InvertScreen(bool inverted)
        {
            bInverted = inverted;
        }

        /// <summary>
        /// Initializes the mouse to be controled by a touch screen.
        /// </summary>
        /// <param name="screenPointer"></param>
        public void InitializeTouch(IntPtr screenPointer)
        {
            TouchEnabled = true;

            touchHandler = Factory.CreateHandler<TouchHandler>(screenPointer);

            touchHandler.TouchDown += TouchDownHandler;
            touchHandler.TouchUp += TouchUpHandler;
            touchHandler.TouchMove += TouchMoveHandler;

            touchQueue = new Queue();
        }

        private void TouchDownHandler(object sender, TouchEventArgs e)
        {
            if (CheckTouchID(e))
            {
                if (!point.pressed)
                {
                    point.X = e.Location.X;
                    point.Y = e.Location.Y;
                    point.pressed = true;

                    touchQueue.Enqueue(point);
                }
            }
        }


        private void TouchUpHandler(object sender, TouchEventArgs e)
        {
            if (CheckTouchID(e))
            {
                point.X = e.Location.X;
                point.Y = e.Location.Y;
                point.pressed = false;

                touchQueue.Enqueue(point);
            }
        }

        private void TouchMoveHandler(object sender, TouchEventArgs e)
        {
            if (CheckTouchID(e))
            {
                if (touchQueue.Count < 5)
                {
                    point.X = e.Location.X;
                    point.Y = e.Location.Y;
                    point.pressed = true;
                    touchQueue.Enqueue(point);
                }

                else
                {
                    if ((Math.Abs(point.X - e.Location.X) + Math.Abs(point.Y - e.Location.Y)) > 45)
                    {
                        point.X = e.Location.X;
                        point.Y = e.Location.Y;
                        point.pressed = true;
                        touchQueue.Enqueue(point);
                    }
                }
            }
        }

        private bool CheckTouchID(TouchEventArgs e)
        {
            if (currentPointID == -1)
            {
                currentPointID = e.Id;
            }

            if (touchQueue.Count == 0)
            {
                currentPointID = e.Id;
            }

            return (currentPointID == e.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeKinect()
        {
            KinectEnabled = true;

            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            DiscoverSensor();
        }

        /// <summary>
        /// This method will use basic logic to try to grab a Sensor.
        /// Once a Sensor is found, it will start the Sensor with the
        /// requested options.
        /// </summary>
        private void DiscoverSensor()
        {
            // Grab any available sensor
            if (KinectSensor.KinectSensors.Count >= 1)
            {
                Sensor = KinectSensor.KinectSensors[0];
            }

            else
            {
                Sensor = null;
            }

            if (Sensor != null)
            {
                lastStatus = Sensor.Status;

                // If this sensor is connected, then enable it
                if (lastStatus == KinectStatus.Connected)
                {
                    Sensor.SkeletonStream.Enable();
                    Sensor.ColorStream.Enable();

                    try
                    {
                        Sensor.Start();
                    }
                    catch (IOException)
                    {
                        // sensor is in use by another application
                        // will treat as disconnected for display purposes
                        Sensor = null;
                    }
                }
            }
            else
            {
                lastStatus = KinectStatus.Disconnected;
            }
        }

        /// <summary>
        /// This wires up the status changed event to monitor for 
        /// Kinect state changes.  It automatically stops the sensor
        /// if the device is no longer available.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The event args.</param>
        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // If the status is not connected, try to stop it
            if (e.Status != KinectStatus.Connected)
            {
                e.Sensor.Stop();
            }

            lastStatus = e.Status;
            DiscoverSensor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2 SkeletonToColorMap(SkeletonPoint point)
        {
            if ((Sensor != null) && (Sensor.ColorStream != null))
            {
                ColorImagePoint colorPt = Sensor.CoordinateMapper.MapSkeletonPointToColorPoint(point, Sensor.ColorStream.Format);

                return (new Vector2(colorPt.X, colorPt.Y));
            }

            return Vector2.Zero;
        }
    }
}
