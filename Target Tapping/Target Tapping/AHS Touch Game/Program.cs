using System;
using TargetTapping;

namespace TargetTapping
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TargetTappingGame game = new TargetTappingGame())
            {
                game.Run();
            }
        }
    }
#endif
}

