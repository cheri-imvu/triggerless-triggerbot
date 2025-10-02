using NUnit.Framework;
using System;
using System.Text.RegularExpressions;
using Triggerless.TriggerBot;
using Triggerless.TriggerBot.Models;

namespace Triggerless.Tests
{
    [TestFixture]
    public class CollectorTests
    {
        [Test]
        public void TestCollect() 
        {
            var filterSql = SQLiteDataAccess.AccessoryFilter;
            // C# verbatim string (RegexOptions.None or IgnoreCase if you like)
            var pattern = @"^\s*cPath\s+IN\s*\(\s*'\[\s*\d+(?:\s*,\s*\d+){1,3}\s*\]'\s*(?:,\s*'\[\s*\d+(?:\s*,\s*\d+){1,3}\s*\]'\s*)*\)$";
            var m = Regex.Match(filterSql, pattern, RegexOptions.IgnoreCase);
            Console.WriteLine(filterSql);
            Assert.That(m.Success, Is.True);
        }

        [Test]
        public void TestAvatarId()
        {
            // this is my cid, change it to your own while testing on your own system
            long expectedValue = 83079851;
            long actualValue = AvatarNameReader.GetAvatarId();
            Console.WriteLine($"{expectedValue} was expected; {actualValue} was observed");
            Assert.That(expectedValue == actualValue);
        }

        [Test]
        public void TestSessionId()
        {
            DateTime date = new DateTime(2025, 10, 1, 19, 40, 33, 400);
            string actualValue = date.Ticks.ToBase36();
            Console.WriteLine(actualValue );
        }

    }
}
