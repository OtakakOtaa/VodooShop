using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeBase.Runtime._CustomersProvider.Pool
{
    public sealed class CustomersOnDayProvider
    {
        private readonly CustomersProvider _customersProvider;

        private readonly CustomersPool[] _customersPools;
        private readonly CustomerOrder[] _ordersWithoutOwners;

        private readonly List<int> _cachedIdOrders = new ();

        public CustomersOnDayProvider(CustomersProvider customersProvider,
            IEnumerable<CustomersPool> pools, IEnumerable<CustomerOrder> ordersWithoutOwners)
        {
            _customersProvider = customersProvider;

            _ordersWithoutOwners = ordersWithoutOwners?.ToArray();
            _customersPools = pools.ToArray();
        }

        public DayPool GetCustomersOnDay(int dayNumber)
        {
            ClearOrderCache();
            
            var customersOnDay = _customersPools
                .Where(p => IsValueIncludedOnRange(dayNumber, p.LevelScope))
                .SelectMany(p => p.CustomersKeys)
                .Select(k =>
                {
                    if (_customersProvider.TryGetCustomerByName(k, out var customer))
                        return customer;
                    throw new Exception();
                });

            var simpleCustomers = customersOnDay.Where(c => c is not PlotCustomer);
            var plotCustomers = customersOnDay.OfType<PlotCustomer>();
            
            simpleCustomers.ToList().ForEach(c => c.PutOrder(GetNextOrder()));

            return new DayPool(simpleCustomers, plotCustomers);

            bool IsValueIncludedOnRange(int value, Range range)
                => value >= range.Start.Value && value <= range.End.Value;
        }

        private CustomerOrder GetNextOrder()
        {
            var index = new Random().Next(0, _ordersWithoutOwners.Length);
            _cachedIdOrders.Add(index);
            return _ordersWithoutOwners[index];
        }

        private void ClearOrderCache()
            => _cachedIdOrders.Clear();
    }
}