using System;
using NUnit.Framework;

namespace CodeBase.Test
{
    public class AnyTest
    {
        [Test] public void TimeSpanStaticStringParsing()
        {
            TimeSpan original = new TimeSpan(45, 4, 57);
            TimeSpan actual = TimeSpan.Parse(original.ToString());

            Assert.AreEqual(original, actual);
        }
    }
}