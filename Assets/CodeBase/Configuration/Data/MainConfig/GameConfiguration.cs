using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Customers;
using CodeBase.Customers.CustomersPool;
using CodeBase.Customers.Data;
using CodeBase.Customers.Order;
using UnityEngine;

namespace CodeBase.Configuration.Data.MainConfig
{
    [CreateAssetMenu(menuName = nameof(GameConfiguration), fileName = nameof(GameConfiguration), order = default)]
    public sealed class GameConfiguration : ScriptableObject, IMainConfigProvider
    {
        [SerializeField] private int _levelAmount;
        [SerializeField] private string _duration;
        
        [SerializeField] private StoryLinePart[] _storyLine;
        [SerializeField] private CustomersPool[] _customersPools;
        
        [SerializeField] private Customer[] _simpleCustomers;
        [SerializeField] private CustomerOrderWithoutOwner[] _ordersWithoutOwners;

        [SerializeField] private CustomersViewConfiguration _customersView;

        public void Constructor(int levelAmount, TimeSpan duration, IEnumerable<StoryLinePart> storyLine, IEnumerable<CustomersPool> pools, 
            IEnumerable<Customer> simpleCustomers, IEnumerable<CustomerOrderWithoutOwner> ordersWithoutOwners)
        {
            _levelAmount = levelAmount;
            _duration = duration.ToString();
            
            _customersPools = pools.ToArray();
            _ordersWithoutOwners = ordersWithoutOwners.ToArray();
            _simpleCustomers = simpleCustomers.ToArray();
            _storyLine = storyLine.ToArray();
        }

        public int LevelAmount => _levelAmount;
        public TimeSpan Duration => TimeSpan.Parse(_duration);
        public IEnumerable<CustomersPool> CustomersPools => _customersPools;
        public IEnumerable<CustomerOrder> OrdersWithoutOwners => _ordersWithoutOwners;
        public IEnumerable<Customer> SimpleCustomers => _simpleCustomers;
        public IEnumerable<Customer> PlotCustomers => _storyLine.Select(p => p.Order.Holder);
        public IEnumerable<StoryLinePart> StoryLine => _storyLine;
    }
}