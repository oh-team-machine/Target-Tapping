using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Stunt_Plane_V2K
{
    class Plane
    {
        public Texture2D plane;
        public Rectangle planeRect;
        public uint[] planeCollision;

        SoundEffect backgroundSound;
        SoundEffectInstance bg;

        Vector2 location;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_plane">Texture to use for the plane</param>
        public Plane(Texture2D _plane, SoundEffect _background)
        {
            plane = _plane;
            planeRect = new Rectangle(0, 0, plane.Width, plane.Height);

            planeCollision = new uint[plane.Width * plane.Height];

            plane.GetData<uint>(planeCollision);

            backgroundSound = _background;
            bg = backgroundSound.CreateInstance();
            bg.IsLooped = true;
        }

        /// <summary>
        /// Draw Method
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(plane, location, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.9f);
        }

        /// <summary>
        /// Updates plane position and rectangle
        /// </summary>
        /// <param name="state">Mouse stat to update position to</param>
        public void Update(MouseState state)
        {
            location.X = state.X;
            location.Y = state.Y;

            planeRect.X = (int)location.X;
            planeRect.Y = (int)location.Y;
        }


        public void playSound()
        {
            bg.Play();
        }

        public void pauseSound()
        {
            bg.Pause();
        }
    }
}
