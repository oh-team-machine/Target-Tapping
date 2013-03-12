using System;

namespace TargetTapping
{
    /// <summary>
    /// 
    /// </summary>
    class GameManager : GameLibrary.ScreenManager
    {
        public String PatientName { get; set; }

        public int Repetitions { get; set; }

        public float Time { get; set; }

        public int Missed { get; set; }

        public GameManager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.PatientName = "";
            this.Repetitions = 10;
            this.Time = 0.0f;
            this.Missed = 0;
        }

    }
}

