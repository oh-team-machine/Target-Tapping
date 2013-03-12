using System;

namespace AHS_Touch_Game
{
    /// <summary>
    /// 
    /// </summary>
    class GameManager : GameLibrary.ScreenManager
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
        public GameManager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.PatientName = "";
            this.Repetitions = 10;
            this.Hand = "";
            this.Time = 0.0f;
            this.Missed = 0;
        }

	/// <summary>
	/// Adds a screen to the Screen Manager, uninitialized.
	/// This will initialize it FOR you!
	/// </summary>
	/// <param name="screen"></param>
	public void AddScreen(GameLibrary.Screen screen) {
            base.AddScreen(screen, false);
	}

    }
}

