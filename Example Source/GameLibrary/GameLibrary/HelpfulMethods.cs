using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GameLibrary
{
    /// <summary>
    /// This allows the program to run in a predefined location, instead
    /// of the primary monitor.
    /// </summary>
    public sealed class User32
    {
        private User32() { }

        /// <summary>
        /// Sets the position of a window.
        /// </summary>
        /// <param name="hWnd">The window Handle</param>
        /// <param name="level">The depth of the window</param>
        /// <param name="X">The x co-ordinate of the window</param>
        /// <param name="Y">The y co-ordinate of the window</param>
        /// <param name="W">The width of the window</param>
        /// <param name="H">The height of the window</param>
        /// <param name="flags">Any flags for the window</param>
        [DllImport("USER32.DLL")]
        public static extern void SetWindowPos(uint hWnd, uint level, int X, int Y, int W, int H, uint flags);

        /// <summary>
        /// This function returns the IntPtr to the window handle for the given window.
        /// </summary>
        /// <param name="lpClassName">The class of window</param>
        /// <param name="lpWindowName">The name of the window</param>
        /// <returns>IntPtr to the window handle</returns>
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Sets the designated window to be the foreground window.
        /// </summary>
        /// <param name="hWnd">IntPtr to the window.</param>
        /// <returns>True if the window is found, false otherwise.</returns>
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }

    /// <summary>
    /// Helpful methods for various tasks.
    /// </summary>
    public sealed class HelpfulMethods
    {
        private HelpfulMethods() { }

        /// <summary>
        /// Sends the exit command to the calling program. Used to
        /// pause motion tracking.
        /// </summary>
        public static void SendExit()
        {
            Process process = new Process();

            process.StartInfo.FileName = "\"C:\\Program Files\\IR Desktop\\Signaller.exe\"";
            process.Start();

            process.Dispose();
        }

        /// <summary>
        /// Takes a millisecond representation of time and returns the
        /// value as minutes and seconds. [0] is minutes, [1] is seconds.
        /// </summary>
        /// <param name="time">Millisecond time value to convert</param>
        /// <returns>A list with two elements [0]minutes, [1]seconds.</returns>
        public static List<int> CalculateTime(float time)
        {
            List<int> totalTime = new List<int>();
            int seconds;
            int minutes;

            seconds = (int)((time / 1000.0f) % 60.0f);
            minutes = (int)((time / 1000.0f) / 60.0f);

            totalTime.Add(minutes);
            totalTime.Add(seconds);

            return totalTime;
        }

        /// <summary>
        /// Function that prints a millisecond time in time format
        /// hh:mm:ss.ss.
        /// </summary>
        /// <param name="timeInMilli">float value representing time</param>
        /// <returns>String formatted as hh:mm:ss.ss</returns>
        public static String FormatTime(float timeInMilli)
        {
            String time = "";

            time += String.Format("{0:00}:", (((timeInMilli / 1000) / 60) / 60));
            time += String.Format("{0:00}:", ((timeInMilli / 1000) / 60));
            time += String.Format("{0:00}.", (timeInMilli / 1000) % 60);
            time += String.Format("{0:00}", (timeInMilli % 100));

            return time;
        }
    }
}
