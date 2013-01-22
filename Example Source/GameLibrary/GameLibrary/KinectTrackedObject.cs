using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Kinect;

namespace GameLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class KinectTrackedObject
    {
        private Vector3 a;
        private Vector3 b;
        private Vector3 c;
        private Vector3 d;

        private Vector3 e;
        private Vector3 f;
        private Vector3 g;
        private Vector3 h;

        private Vector3 nUpper;
        private Vector3 nLower;

        private float AreaABC;
        private float AreaCDA;

        private float planeA;
        private float planeB;
        private float planeC;
        private float planeD;

        private double denominator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UL"></param>
        /// <param name="UR"></param>
        /// <param name="LL"></param>
        /// <param name="LR"></param>
        public KinectTrackedObject(Vector2 UL, Vector2 UR, Vector2 LL, Vector2 LR)
        {
            a = new Vector3(UL, 1.0f);
            b = new Vector3(UR, 1.0f);
            c = new Vector3(LL, 1.0f);
            d = new Vector3(LR, 1.0f);

            CalibrateScreen();
        }

        private void CalibrateScreen()
        {
            e = new Vector3(0.0f, 0.0f, 1.0f);
            f = new Vector3(1920.0f, 0.0f, 1.0f);
            g = new Vector3(0.0f, 1079.0f, 1.0f);
            h = new Vector3(1920.0f, 1079.0f, 1.0f);

            // Compute the normal of the triangle
            nUpper = Vector3.Normalize(Vector3.Cross((b - a), (c - a)));
            // Compute twice area of triangle ABC
            AreaABC = Vector3.Dot(nUpper, Vector3.Cross((b - a), (c - a)));

            // Compute the normal of the triangle
            nLower = Vector3.Normalize(Vector3.Cross((d - a), (c - a)));
            // Compute twice area of triangle CDA
            AreaCDA = Vector3.Dot(nLower, Vector3.Cross((d - a), (c - a)));

            planeA = a.Y * (b.Z - c.Z) + b.Y * (c.Z - a.Z) + c.Y * (a.Z - b.Z);
            planeB = a.Z * (b.X - c.X) + b.Z * (c.X - a.X) + c.Z * (a.X - b.X);
            planeC = a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y);
            planeD = -(a.X * ((b.Y * c.Z) - (c.Y * b.Z)) + b.X * ((c.Y * a.Z) - (a.Y * c.Z)) + c.X * ((a.Y * b.Z) - (b.Y * a.Z)));

            denominator = Math.Sqrt(((planeA * planeA) + (planeB * planeB) + (planeC * planeC)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2 TranslatePoint(Vector2 point)
        {
            Vector3 p = new Vector3(point, 1.0f);

            // Compute a
            float AreaPBC = Vector3.Dot(nUpper, Vector3.Cross((b - p), (c - p)));
            float A = AreaPBC / AreaABC;
            // Compute b
            float AreaPCA = Vector3.Dot(nUpper, Vector3.Cross((c - p), (a - p)));
            float B = AreaPCA / AreaABC;
            // Compute c
            float C = 1.0f - A - B;

            // Find point Q
            Vector3 q = (e * A) + (f * B) + (g * C);


            if (q.X < 0 || q.Y < 0 || q.Z < 0)
            {
                float AreaPDA = Vector3.Dot(nLower, Vector3.Cross((d - p), (a - p)));
                A = AreaPDA / AreaCDA;
                // Compute b
                AreaPCA = Vector3.Dot(nLower, Vector3.Cross((c - p), (a - p)));
                B = AreaPCA / AreaCDA;
                // Compute c
                C = 1.0f - A - B;

                // Find point Q
                q = (g * A) + (h * B) + (e * C);
            }

            if (q.X > 1920)
            {
                q.X = 1920;
            }
            else if (q.X < 0)
            {
                q.X = 0;
            }

            if (q.Y < 0)
            {
                q.Y = 0;
            }
            else if (q.Y > 1079)
            {
                q.Y = 1079;
            }

            return new Vector2(q.X, q.Y);
        }
    }
}
