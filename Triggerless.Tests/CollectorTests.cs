using NUnit.Framework;

namespace Triggerless.Tests
{
    [TestFixture]
    public class CollectorTests
    {
        [Test]
        public void TestCollect() 
        {
            new SQLiteDataAccess().DeleteAppCache();
            var collector = new Collector();
            collector.ScanDatabasesSync();
        }

    }
}
