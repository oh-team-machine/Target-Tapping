using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameLibrary
{
    /// <summary>
    /// This class handles all types of per pixel collisions for 2D objects.
    /// </summary>
    public sealed class Collisions
    {
        private Collisions() { }

        /// <summary>
        /// This is a per pixel collision detection algorithom that tracks collisions between
        /// the mouse and any arbitrary 2D sprite
        /// </summary>
        /// <param name="rectA"> Rectangle the sprite is in.</param>
        /// <param name="colA">Colour array of the sprite</param>
        /// <param name="state">The current mouse state</param>
        /// <returns>bool stating if a collision occured</returns>
        public static bool PerPixelCollisionWithMouse(Rectangle rectA, Color[] colA, MouseState state)
        {
            if(colA == null){

                return false;

            }

            if (!rectA.Contains(state.X, state.Y))
                return false;

            int x = state.X - rectA.Left;
            int y = state.Y - rectA.Top;

            Color colorA = colA[x + (y * rectA.Width)];

            if (colorA.A != 0)
                return true;

            return false;
        }

        /// <summary>
        /// This is a collision detection algorithom that tracks collisions between
        /// the mouse and any arbitrary 2D sprite
        /// </summary>
        /// <param name="rect"> Rectangle the sprite is in.</param>
        /// <param name="state">The current mouse state</param>
        /// <returns>bool stating if a collision occured</returns>
        public static bool CollisionWithMouse(Rectangle rect, MouseState state)
        {
            if (!rect.Contains(state.X, state.Y))
                return false;

            return true;
        }

        /// <summary>
        /// Per pixel collision detection.
        /// </summary>
        /// <param name="rectA">Rectangle of first object</param>
        /// <param name="rectB">Rectangle of second object</param>
        /// <param name="colA">Color array of first object as uint</param>
        /// <param name="colB">Color array of second object as uint</param>
        /// <returns>bool stating if a collision occured</returns>
        public static bool PerPixelCollision(Rectangle rectA, Rectangle rectB, uint[] colA, uint[] colB)
        {
            if (colA == null || colB == null)
            {
                return false;
            }

            if (Rectangle.Intersect(rectA, rectB) == Rectangle.Empty)
                return false;

            int top = Math.Max(rectA.Top, rectB.Top);
            int bottom = Math.Min(rectA.Bottom, rectB.Bottom);
            int left = Math.Max(rectA.Left, rectB.Left);
            int right = Math.Min(rectA.Right, rectB.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    uint colorA = colA[(x - rectA.Left) +
                                (y - rectA.Top) * rectA.Width];
                    uint colorB = colB[(x - rectB.Left) +
                                (y - rectB.Top) * rectB.Width];

                    // If both pixels are not completely transparent,
                    if (((colorA & 0xFF000000) >> 24) > 20 && ((colorB & 0xFF000000) >> 24) > 20)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels between two
        /// sprites regardless of rotation or scale.
        /// </summary>
        /// <param name="transformA">World transform of the first sprite.</param>
        /// <param name="widthA">Width of the first sprite's texture.</param>
        /// <param name="heightA">Height of the first sprite's texture.</param>
        /// <param name="dataA">Pixel color data of the first sprite.</param>
        /// <param name="transformB">World transform of the second sprite.</param>
        /// <param name="widthB">Width of the second sprite's texture.</param>
        /// <param name="heightB">Height of the second sprite's texture.</param>
        /// <param name="dataB">Pixel color data of the second sprite.</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool PerPixelCollisionWithRotationScale(Matrix transformA, int widthA, int heightA, uint[] dataA, Matrix transformB, int widthB, int heightB, uint[] dataB)
        {
            if (dataA == null || dataB == null)
            {
                return false;
            }

            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        uint colorA = dataA[xA + yA * widthA];
                        uint colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (((colorA & 0xFF000000) >> 24) > 20 && ((colorB & 0xFF000000) >> 24) > 20)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }
    }
}
