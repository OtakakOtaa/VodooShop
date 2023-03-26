using System;
using CodeBase.Runtime;
using CodeBase.Runtime.Configuration;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Test
{
    public class AnyTest
    {
        [Test] public void CreationSoTest()
        {
           GameConfiguration gameConfiguration = ScriptableObject.CreateInstance<GameConfiguration>();
           AssetDatabase.CreateAsset(gameConfiguration, "Assets/MainGameConfiguration.asset");
           AssetDatabase.SaveAssets();
        }

        [TestCase(0,1)]
        [TestCase(0,2)]
        [TestCase(0,3)]
        [TestCase(0,4)]
        [Test] public void SystemRangeMaxLimitText(int minLimit, int maxLimit)
        { 
            var range = new Range(minLimit, maxLimit);
            Assert.AreEqual(maxLimit, range.End);
        }

        [Test] public void TimeSpanStaticStringParsing()
        {
            TimeSpan original = new TimeSpan(45, 4, 57);
            TimeSpan actual = TimeSpan.Parse(original.ToString());

            Assert.AreEqual(original, actual);
        }
    }
}