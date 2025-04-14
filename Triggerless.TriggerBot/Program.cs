using System;
using System.Threading;
using System.Threading.Tasks;
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
        private static void Main()
        {
            bool ranSuccessfully = false;
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
                    bool bSuccess = false;
                    Exception exc = null;
                    while (tryCount < tryMax) 
                    {
                        try
                        {
                            Thread.Sleep(tryWait);
                            MainForm = new TriggerBotMainForm();
                            try 
                            {
                                Application.Run(MainForm);
                            }
                            catch (Exception ex)
                            {
                                Discord.SendMessage("Application Crashed", $"{ex.Message}\n{ex.StackTrace}").Wait(10000);
                                throw ex;
                            }
                            bSuccess = true;
                            ranSuccessfully = true;
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (ranSuccessfully) // meaning this was running before
                            {
                                Discord.SendMessage("Application Crashed", $"{ex.Message}\n{ex.StackTrace}").Wait(10000);
                                throw ex;
                            }
                            tryCount++;
                            tryWait += 200;
                            exc = ex;
                        }
                    }
                    if (!bSuccess)
                    {
                        string msg = $"Unable to open Triggerbot after {tryMax} tries. Please contact @Triggers in IMVU. {exc.Message}\n{exc.StackTrace}";
                        string caption = "Error Starting Triggerbot";
                        Discord.SendMessage(caption, msg).Wait(10000);
                        
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
