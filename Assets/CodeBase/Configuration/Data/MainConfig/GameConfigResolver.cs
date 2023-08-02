using System;
using System.Collections.Generic;
using CodeBase.Customers;
using CodeBase.Customers.CustomersPool;
using CodeBase.Customers.Data;
using CodeBase.Customers.Order;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Configuration.Data.MainConfig
{
    public sealed class GameConfigResolver : IMainConfigProvider
    {
        public const string MainConfigurationPatch = "MainGameConfiguration";

        private GameConfiguration _cachedGameConfiguration;

        public async UniTask<GameConfiguration> LoadGameConfiguration()
        {
            _cachedGameConfiguration ??=
                (GameConfiguration)await Resources.LoadAsync<GameConfiguration>(MainConfigurationPatch).ToUniTask();
            return _cachedGameConfiguration;
        }

        public int LevelAmount => _cachedGameConfiguration.LevelAmount;
        
        public TimeSpan Duration => _cachedGameConfiguration.Duration;
        
        public IEnumerable<CustomersPool> CustomersPools => _cachedGameConfiguration.CustomersPools;
        
        public IEnumerable<CustomerOrder> OrdersWithoutOwners => _cachedGameConfiguration.OrdersWithoutOwners;
        
        public IEnumerable<Customer> SimpleCustomers => _cachedGameConfiguration.SimpleCustomers;
        
        public IEnumerable<Customer> PlotCustomers => _cachedGameConfiguration.PlotCustomers;
        
        public IEnumerable<StoryLinePart> StoryLine => _cachedGameConfiguration.StoryLine;
    }
}