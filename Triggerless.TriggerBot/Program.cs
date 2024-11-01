using System;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (SingleProgramInstance spi = new SingleProgramInstance("Triggerless.Triggerbot.0"))
            {
                if (spi.IsSingleInstance)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    MainForm = new TriggerBotMainForm();
                    Application.Run(MainForm);
                }
                else
                {
                    spi.RaiseOtherProcess();
                }
            }
        }

        public static TriggerBotMainForm MainForm { get; set; }
    }
}
