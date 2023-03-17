using System;
using CodeBase.Runtime;
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
    }
}