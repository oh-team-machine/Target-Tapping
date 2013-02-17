using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

// Anotated by Eddie.

namespace Wack_A_Mole_V4
{
    /// <summary>
    /// A ScreenManager subclass that also doubles as a data class.
    /// The data that would be interesting for the therapist.
    /// Poor separation of concerns but whatever.
    /// </summary>
    class Manager : GameLibrary.ScreenManager
    {

	// NOTE: The following are automatic properties.
	// See example 6 on this page:
        // http://www.dotnetperls.com/property

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

