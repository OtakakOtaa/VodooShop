using System;
using CodeBase.Runtime.Infrastructure.InternalTools;
using UnityEngine;

namespace CodeBase.Runtime
{
    [CreateAssetMenu(menuName = nameof(GameConfiguration), fileName = nameof(GameConfiguration), order = default)]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField, ViewInInspector] private int _levelAmount;

        public GameConfiguration(int levelAmount)
        {
            _levelAmount = levelAmount;
        }
        
        public int LevelAmount => _levelAmount;

        [Serializable] public class LevelSettings
        {
            [SerializeField] private int _levelId;
            [SerializeField] private int _maxCustomersAmount;
            [SerializeField] private TimeSpan _levelTime;
        }
    }
}