using System;

// Annotated by Eddie.

namespace Wack_A_Mole_V4
{
// I don't know why this is define is here...
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

