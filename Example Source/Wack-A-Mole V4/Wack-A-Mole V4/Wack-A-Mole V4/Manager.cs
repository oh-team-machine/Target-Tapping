using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace Wack_A_Mole_V4
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
        public int MoleNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float MoleTimer { get; set; }

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
            this.MoleTimer = 1.0f;
            this.MoleNumber = 1;
            this.Repetitions = 10;
            this.Hand = "";
            this.Time = 0.0f;
            this.Missed = 0;
        }
    }
}

