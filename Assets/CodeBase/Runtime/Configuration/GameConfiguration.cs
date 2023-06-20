using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime._CustomersProvider.Pool;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;
using CodeBase.Runtime.Infrastructure;
using CodeBase.Runtime.Infrastructure.Collections;
using UnityEngine;

namespace CodeBase.Runtime.Configuration
{
    [CreateAssetMenu(menuName = nameof(GameConfiguration), fileName = nameof(GameConfiguration), order = default)]
    public sealed class GameConfiguration : ScriptableObject
    {
        [Space] [Header("General Game Settings")] [Space]
        [SerializeField] private int _levelAmount;
        [SerializeField] private string _duration;
        
        [Space] [Header("Customers && Pools && StoryLine")] [Space]
        [SerializeField] private SyncDictionary<int, PlotCustomer> _customersStoryLine;
        [SerializeField] private Customer[] _simpleCustomers;
        [SerializeField] private CustomersPool[] _customersPools;

        [Space] [Header("Free Orders")] [Space]
        [SerializeField] private CustomerOrder[] _ordersWithoutOwners;

        [Space] [Header("Customers View")] [Space] 
        [SerializeField] private CustomersViewConfiguration _customersView;
        
        
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