using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime._CustomersProvider.Pool;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;
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
        [SerializeField] private Customer[] _customers;
        [SerializeField] private CustomerOrder[] _ordersWithoutOwners;
        
        public void Constructor(int levelAmount, TimeSpan duration, IEnumerable<CustomersPool> pools, 
            IEnumerable<Customer> allCustomers, IEnumerable<CustomerOrder> ordersWithoutOwners)
        {
            _levelAmount = levelAmount;
            _duration = duration.ToString();
            
            _customersPools = pools.ToArray();
            _customers = allCustomers.ToArray();
            _ordersWithoutOwners = ordersWithoutOwners.ToArray();
        }
        
        public int LevelAmount => _levelAmount;
        public TimeSpan Duration => TimeSpan.Parse(_duration);

        public IEnumerable<CustomersPool> CustomersPools => _customersPools;
        public IEnumerable<CustomerOrder> OrdersWithoutOwners => _ordersWithoutOwners;
        public IEnumerable<Customer> AllCustomers => _customers;
        public IEnumerable<Customer> SimpleCustomers => _customers.Where(c => c is not PlotCustomer);
        public IEnumerable<PlotCustomer> PlotCustomers => _customers.OfType<PlotCustomer>();
    }
}