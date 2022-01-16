using System;
using System.Threading;
using System.Windows.Forms;

namespace win_capture_audio_installer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static bool result;
        static Mutex mutex = new Mutex(true, "win-capture-audio-installer.DeathlyBower959", out result);

        [STAThread]
        static void Main()
        {
            // Checks if app already open
            if (!result) return;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
