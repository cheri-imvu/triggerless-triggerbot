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
        public void TestCollect() 
        {

            var collector = new Collector();
            var x = collector.ScanDatabases();

        }

    }
}
