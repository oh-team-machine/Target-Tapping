using System;

namespace BackendDevelopment
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public Manager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.PatientName = "";
            this.Repetitions = 10;
            this.Hand = "";
            this.Time = 0.0f;
            this.Missed = 0;
        }
    }
}

