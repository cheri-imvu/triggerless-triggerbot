using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuneTriggerer
{
    public class Collector
    {
        public class Result { }

        public class CollectorEventArgs : EventArgs { }

        public delegate void CollectorEventHandler (object sender, CollectorEventArgs e);

        public event CollectorEventHandler EventOccurred;

        public void FireEvent(CollectorEventArgs e)
        {
            if (EventOccurred != null)
            {
                EventOccurred (this, e);
            }
        }

        private int _numberOfProducts = 0;
        private int _numberProcessed = 0;
        private object _lockObject;

        public static string GetUrlTemplate(int pid) => $"https://userimages-akm.imvu.com/productdata/{pid}/1/{{0}}";

        public static string GetUrl(int pid, string filename) => string.Format(GetUrlTemplate(pid), filename);

        public void Collect() // TODO: figure out a return type.
        {
            
            var sda = new SQLiteDataAccess();
            using (var connProduct = sda.GetProductCacheCxn())
            using (var connApp = sda.GetAppCacheCxn())
            {
                connApp.Open();
                connProduct.Open();

                // Get a list of productCache PIDs that are potential music triggers
                var sqlSearch = $"SELECT id FROM products WHERE {SQLiteDataAccess.AccessoryFilter}";

            }

            // Get a list of appCache PID that have already been scanned

            // Remove the productCache PIDs that are in the appCache PID list

            // Report number of products and number processed

            // Loop over the unscanned PIDs

            // Begin this iteration

            // Set up an HTTP client

            // Grab _contents.json
            // Translate to object, maybe
            // If there are no .ogg files, save the record has_ogg = 0, increment numProcessed, report and continue
            // Grab the image and convert to a BLOB, save the record (with title, creator and image BLOB and has_ogg = 1
            // Grab index.xml and link the trigger names to the ogg file
            // Sort the triggers by name/numerical sequence
            // Loop through the list of triggers
            // Download the ogg FileStream
            // Get the fragment length
            // Save a record to the triggers table
            // increment 
            // report back
            // next iteration

            // I think we're done!


        }



    }
}
