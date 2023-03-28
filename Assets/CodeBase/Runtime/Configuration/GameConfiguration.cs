using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime._CustomersProvider.Pool;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;
using CodeBase.Runtime.Infrastructure;
using CodeBase.Runtime.Infrastructure.InternalTools;
using UnityEngine;

namespace CodeBase.Runtime.Configuration
{
    [CreateAssetMenu(menuName = nameof(GameConfiguration), fileName = nameof(GameConfiguration), order = default)]
    public sealed class GameConfiguration : ScriptableObject
    {
        [SerializeField, ViewInInspector] private int _levelAmount;
        [SerializeField, ViewInInspector] private string _duration;
        
        [SerializeField] private CustomersPool[] _customersPools;
        [SerializeField] private Customer[] _simpleCustomers;
        [SerializeField] private SyncDictionary<int, PlotCustomer> _customersStoryLine;
        [SerializeField] private CustomerOrder[] _ordersWithoutOwners;
        
        public void Constructor(int levelAmount, TimeSpan duration, 
            IEnumerable<CustomersPool> pools, IEnumerable<Customer> simpleCustomers, 
            SyncDictionary<int, PlotCustomer> customersStoryLine, IEnumerable<CustomerOrder> ordersWithoutOwners)
        {
            _levelAmount = levelAmount;
            _duration = duration.ToString();
            
            _customersPools = pools.ToArray();
            _simpleCustomers = simpleCustomers.ToArray();
            _ordersWithoutOwners = ordersWithoutOwners.ToArray();
            _customersStoryLine = customersStoryLine;
        }
        
        public int LevelAmount => _levelAmount;
        
        public TimeSpan Duration => TimeSpan.Parse(_duration);
        
        public IEnumerable<CustomersPool> CustomersPools => _customersPools;
        
        public IEnumerable<CustomerOrder> OrdersWithoutOwners => _ordersWithoutOwners;
        
        public IEnumerable<Customer> SimpleCustomers => _simpleCustomers;
        
        public IEnumerable<PlotCustomer> PlotCustomers => _customersStoryLine.Values.Distinct();
        
        public Dictionary<int, PlotCustomer> StoryLine => _customersStoryLine.ToDictionary();
        
    }
}