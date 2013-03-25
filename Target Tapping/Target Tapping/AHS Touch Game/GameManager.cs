using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetTapping.Back_end;

namespace TargetTapping
{
    /// <summary>
    /// 
    /// </summary>
    class GameManager : GameLibrary.ScreenManager
    {
        // Hacky stuff!
        public static GameManager GlobalInstance { get; private set; }

        public String PatientName { get; set; }

        public int Repetitions { get; set; }

        public float Time { get; set; }

        public int Missed { get; set; }

        public int UpTime { get; set; }

        public int HoldTime { get; set; }

        public LevelNames LevelNames { get; private set; }

        //added this to store the level once its been made in the level editor and to
        //access its objects while in the gamescreen.
        public Level activeLevel { get; set; }


        public GraphicsDeviceManager Graphics { get; set; }

        public GameManager(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.PatientName = "";
            this.Repetitions = 10;
            this.Time = 0.0f;
            this.Missed = 0;
            //instantiate the Level we will be playing
            this.activeLevel = new Level();
            LevelNames = new LevelNames();
            GlobalInstance = this;
        }

    }
}

