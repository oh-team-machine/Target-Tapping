using System;

namespace AHS_Kinect_Game
{
    /// <summary>
    /// This class is an extension of the GameLibrary.ddl ScreenManager. It adds global variable support and storage to
    /// the parent.
    /// 
    /// NOTE: Add new global variables for game elements here.
    /// </summary>
    class Manager : GameLibrary.ScreenManager
    {
        /// <summary>
        /// The Patient Name identifier, used for saving to the database.
        /// </summary>
        public String PatientName { get; set; }

        /// <summary>
        /// Number of repetitions.
        /// </summary>
        public int Repetitions { get; set; }

        /// <summary>
        /// Hand used for tracking and gameplay purposes.
        /// </summary>
        public String Hand { get; set; }

        /// <summary>
        /// Time played in game, non-formatted.
        /// </summary>
        public float Time { get; set; }

        /// <summary>
        /// Number of targets missed.
        /// </summary>
        public int Missed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The Kinect object, used for tracking and drawing.
        /// </summary>
        public GameLibrary.KinectTrackedObject kTrackedObj { get; set; }

        /// <summary>
        /// Constructor for the Manager class. Initializes all of the global variables.
        /// 
        /// NOTE: Initialize any added global variables here.
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
            this.Height = 10;
            this.kTrackedObj = null;
        }
    }
}

