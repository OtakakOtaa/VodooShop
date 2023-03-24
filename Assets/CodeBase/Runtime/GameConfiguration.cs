using System;
using CodeBase.Runtime.Infrastructure.InternalTools;
using UnityEngine;

namespace CodeBase.Runtime
{
    [CreateAssetMenu(menuName = nameof(GameConfiguration), fileName = nameof(GameConfiguration), order = default)]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField, ViewInInspector] private int _levelAmount;
        [SerializeField] private ShopDaySettings[] _shopDays = new []
        {
            new ShopDaySettings(45, TimeSpan.Zero),
            new ShopDaySettings(3, TimeSpan.Zero),
            new ShopDaySettings(71, TimeSpan.Zero),
        };

        public void Constructor(ShopDaySettings[] shopDays)
        {
            _shopDays = shopDays;
            _levelAmount = shopDays.Length;
        }
        
        public int LevelAmount => _levelAmount;
        public ShopDaySettings[] ShopDays => _shopDays;
    }

    [Serializable] public class ShopDaySettings
    {
        [SerializeField, ViewInInspector] private int _number;
        [SerializeField, ViewInInspector] private string _duration;
        
        public ShopDaySettings(int number, TimeSpan duration)
        {
            _number = number;
            _duration = duration.ToString();
        }
        
        public int Number => _number;
        public TimeSpan Duration => TimeSpan.Parse(_duration);
    }
}