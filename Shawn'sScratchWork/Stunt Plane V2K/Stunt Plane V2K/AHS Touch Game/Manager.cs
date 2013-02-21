using System;

namespace Stunt_Plane_V2K
{
    /// <summary>
    /// 
    /// </summary>
    class Manager : GameLibrary.ScreenManager
    {
        /// <summary>
        /// 
        /// </summary>
        public String PatientName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NumberOfRings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Repetitions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Hand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Missed { get; set; }

        public GameLibrary.KinectTrackedObject kTrackedObj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public Manager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.PatientName = "";
            this.NumberOfRings = 1;
            this.Speed = 1;
            this.Repetitions = 10;
            this.Hand = "";
            this.Time = 0.0f;
            this.Missed = 0;
            this.kTrackedObj = null;
        }
    }
}

