using System;
using System.Windows.Forms;

namespace MadCalc
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /**
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (loginForm.PasswordHash != "1df1854015e31ca286d015345eaff29a6c6073f70984a3a746823d4cac16b075".ToUpper())
                {
                    return;
                }
            }
            /**/

            Application.Run(new MainForm());
        }
    }
}
