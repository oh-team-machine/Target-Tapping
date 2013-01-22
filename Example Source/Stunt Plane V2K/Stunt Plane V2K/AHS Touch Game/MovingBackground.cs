using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Stunt_Plane_V2K
{
    class MovingBackground
    {
        // The image representing the parallaxing background
        private Texture2D texture;

        // An array of positions of the parallaxing background
        private List<Rectangle> positions;

        private Rectangle temp;

        // The speed which the background is moving
        private int speed;

        private Viewport viewport;

        public MovingBackground(Texture2D texture, Viewport viewport, int speed)
        {
            this.texture = texture;
            this.speed = speed;
            this.viewport = viewport;
            // If we divide the screen with the texture width then we can determine the number of tiles need.
            // We add 1 to it so that we won't have a gap in the tiling
            positions = new List<Rectangle>(2);

            // Set the initial positions of the parallaxing background
            for (int i = 0; i < positions.Capacity; i++)
            {
                // We need the tiles to be side by side to create a tiling effect
                positions.Add(new Rectangle(i * viewport.Width, 0, viewport.Width, viewport.Height));
            }
        }

        public void Update()
        {
            // Update the positions of the background
            for (int i = 0; i < positions.Count; i++)
            {
                // Update the position of the screen by adding the speed
                temp = positions[i];
                temp.X -= speed;
                positions[i] = temp;

                // Check the texture is out of view then put that texture at the end of the screen
                if (positions[i].X <= -viewport.Width)
                {
                    temp = positions[i];
                    temp.X = viewport.Width * (positions.Count - 1);
                    positions[i] = temp;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                spriteBatch.Draw(texture, positions[i], null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            }
        }
    }
}
