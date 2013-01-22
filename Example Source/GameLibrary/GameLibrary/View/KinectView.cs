using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.UI;

namespace GameLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class KinectView
    {
        private GraphicsDevice GraphicsDevice;

        private KinectSensor sensor;

        private RenderTarget2D colorTexture;
        private byte[] colorData;
        private byte[] bgraData;

        private Quadrilateral trackedArea;

        private Rectangle positionRect;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="GraphicsDevice"></param>
        /// <param name="position"></param>
        public KinectView(KinectSensor sensor, GraphicsDevice GraphicsDevice, Vector2 position)
        {
            this.GraphicsDevice = GraphicsDevice;
            this.sensor = sensor;

            this.positionRect = new Rectangle((int)position.X, (int)position.Y, 640, 480);

            this.trackedArea = new Quadrilateral(position, position, position, position, 0, Color.Red, this.GraphicsDevice);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update(){
            if (sensor == null || !sensor.IsRunning || sensor.Status !=  KinectStatus.Connected)
                {
                    return;
                }

                using (ColorImageFrame frame = sensor.ColorStream.OpenNextFrame(0))
                {

                    if (frame == null)
                    {
                        return;
                    }

                    if (colorData == null || colorData.Length != frame.PixelDataLength)
                    {
                        colorData = new byte[frame.PixelDataLength];
                        colorTexture = new RenderTarget2D(GraphicsDevice, frame.Width, frame.Height);
                    }

                    frame.CopyPixelDataTo(colorData);
                
                    bgraData = new byte[frame.PixelDataLength];
                
                    for (int i = 0; i < colorData.Length; i+=4)
                    {
                        bgraData[i] = colorData[i + 2];
                        bgraData[i + 1] = colorData[i + 1];
                        bgraData[i + 2] = colorData[i];
                        bgraData[i + 3] = (byte)255;
                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (colorTexture == null)
            {
                return;
            }

            GraphicsDevice.Textures[0] = null;
            colorTexture.SetData<byte>(bgraData);

            spriteBatch.Draw(colorTexture, positionRect, null, Color.White);

            trackedArea.Draw(spriteBatch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackedArea"></param>
        public void UpdateTrackedArea(Quadrilateral trackedArea)
        {
            this.trackedArea.UpperLeft = new Vector2(trackedArea.UpperLeft.X + positionRect.X, trackedArea.UpperLeft.Y + positionRect.Y);
            this.trackedArea.UpperRight = new Vector2(trackedArea.UpperRight.X + positionRect.X, trackedArea.UpperRight.Y + positionRect.Y);
            this.trackedArea.LowerLeft = new Vector2(trackedArea.LowerLeft.X + positionRect.X, trackedArea.LowerLeft.Y + positionRect.Y);
            this.trackedArea.LowerRight = new Vector2(trackedArea.LowerRight.X + positionRect.X, trackedArea.LowerRight.Y + positionRect.Y);

            this.trackedArea.Color = trackedArea.Color;
            this.trackedArea.LineWidth = trackedArea.LineWidth;
        }
    }
}
