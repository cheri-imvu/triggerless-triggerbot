using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triggerless.TriggerBot;

namespace Triggerless.Tests
{
    [TestFixture]
    public class CollectorTests
    {
        [Test]
        public async Task TestCollectAsync() 
        {
            new SQLiteDataAccess().DeleteAppCache();
            var collector = new Collector();
            collector.ScanDatabases();
        }

    }
}
