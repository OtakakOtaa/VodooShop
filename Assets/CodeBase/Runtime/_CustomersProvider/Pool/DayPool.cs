using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;

namespace CodeBase.Runtime._CustomersProvider.Pool
{
    public sealed class DayPool
    {
        private readonly Customer[] _simpleCustomers;
        private readonly PlotCustomer[] _plotCustomers;
        
        private int _currentCustomer;

        public DayPool(IEnumerable<Customer> simpleCustomers, IEnumerable<PlotCustomer> plotCustomers)
        {
            _simpleCustomers = simpleCustomers.ToArray();
            _plotCustomers = plotCustomers.ToArray();
        }

        public Customer RandomSimpleCustomer => _simpleCustomers[new Random().Next(0, _simpleCustomers.Length)];

        public bool NextPlotCustomer(out PlotCustomer plotCustomer)
        {
            plotCustomer = null;
            if (_currentCustomer == _simpleCustomers.Length) return false;
            plotCustomer = _plotCustomers[_currentCustomer++];
            return true;
        }
    }
}