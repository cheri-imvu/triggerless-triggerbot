using System;
using System.Threading;
using System.Windows.Forms;
using Triggerless.TriggerBot.Models;

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
            using (SingleProgramInstance spi = new SingleProgramInstance("Triggerless.Triggerbot.0.9.5"))
            {
                if (spi.IsSingleInstance)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //_= Discord.CleanupChannel().Result;
                    int tryCount = 0;
                    int tryMax = 10;
                    int tryWait = 100;
                    while (tryCount < tryMax) 
                    {
                        try
                        {
                            Thread.Sleep(tryWait);
                            MainForm = new TriggerBotMainForm();
                            Application.Run(MainForm);
                            break;
                        }
                        catch (Exception )
                        {
                            tryCount++;
                            tryWait += 200;
                        }
                        string msg = $"Unable to open Triggerbot after {tryMax} tries. Please contact @Triggers in IMVU.";
                        string caption = "Error Starting Triggerbot";
                        _ = Discord.SendMessage(caption, msg).Result;
                        IWin32Window win = null;
                        MessageBox.Show(win, msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
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
