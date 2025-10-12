using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Triggerless.TriggerBot.Models;
using Triggerless.TriggerBot.Properties;

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
            bool pretendFrench = false;
            if (pretendFrench) 
            {
                var fr = CultureInfo.GetCultureInfo("fr-FR");
                Thread.CurrentThread.CurrentCulture = fr;
                Thread.CurrentThread.CurrentUICulture = fr;
            }


            bool junctionWorked = TriggerbotLinker.EnsureTriggerbotJunction(); 
            // creates a shortcut at the top of IMVU Projects

            SessionId = DateTime.UtcNow.Ticks.ToBase36();
            UniqueId = TriggerBot.UniqueId.ForCurrentUserMachine();
            if (Settings.Default.Cid > 0)
            {
                Cid = Settings.Default.Cid;
            }
            else
            {
                Cid = AvatarNameReader.GetAvatarId();
                Settings.Default.Cid = Cid;
                Settings.Default.Save();
            }
            bool ranSuccessfully = false;
            using (SingleProgramInstance spi = new SingleProgramInstance("Triggerless.Triggerbot.1"))
            {
                if (spi.IsSingleInstance)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    StyledMessageBox.ConfigureTheme(
                        back: Color.FromArgb(51, 29, 0),
                        fore: Color.FromArgb(224,224,224),
                        font: new Font("Liberation Sans", 11f),
                        followSystemDarkTitleBar: true
                    );
                    //_= Discord.CleanupChannel().Result;
                    int tryCount = 0;
                    int tryMax = 2;
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
                                //Discord.SendMessage("Application Crashed", $"{ex.Message}\n{ex.StackTrace}").Wait(10000);
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
                                //Discord.SendMessage("Application Crashed", $"{ex.Message}\n{ex.StackTrace}").Wait(10000);
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
                        StyledMessageBox.Show(win, msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public static string SessionId {  get; private set; }
        public static string UniqueId { get; private set; }
        public static long Cid { get; private set; }    
    }
}
