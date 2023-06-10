using System.Configuration;

namespace Triggerless.TriggerBot
{
    internal class Shared
    {
        public static bool Paid { get; set; }
        public static void CheckIfPaid()
        {
            string configPaid = ConfigurationManager.AppSettings["appLevel"];
            if (configPaid == "paid" ) { 
                Paid = true;
            }
        }
    }
}
