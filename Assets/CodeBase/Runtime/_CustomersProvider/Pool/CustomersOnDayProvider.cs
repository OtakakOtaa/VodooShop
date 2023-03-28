using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Core._Customer;
using CodeBase.Runtime.Infrastructure;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeBase.Runtime._CustomersProvider.Pool
{
    public sealed class CustomersOnDayProvider
    {
        private readonly CustomersProvider _customersProvider;
        private readonly NonRepeatingCollectionElementGiver<CustomerOrder> _ordersProvider;

        private readonly CustomersPool[] _customersPools;

        public CustomersOnDayProvider(CustomersProvider customersProvider,
            IEnumerable<CustomersPool> pools, IEnumerable<CustomerOrder> ordersWithoutOwners)
        {
            _customersProvider = customersProvider;
            _ordersProvider = new NonRepeatingCollectionElementGiver<CustomerOrder>(ordersWithoutOwners);
            _customersPools = pools.ToArray();
        }

        public DayPool GetCustomersOnDay(int dayNumber)
        {
            var simpleCustomersOnThisDay = _customersPools
                .Where(p => IsValueIncludedOnRange(dayNumber, p.LevelScope))
                .SelectMany(p => p.CustomersKeys)
                .Select(GetCustomerById);

            var plotCustomerOnThisDay = _customersProvider.TryGetPlotCustomerThatDay(dayNumber);

            simpleCustomersOnThisDay
                .ToList()
                .ForEach(c => c.PutOrder(GetRandomOrder()));

            return new DayPool(simpleCustomersOnThisDay, plotCustomerOnThisDay);

            bool IsValueIncludedOnRange(int value, Range range)
                => value >= range.Start.Value && value <= range.End.Value;

            Customer GetCustomerById(string id)
            {
                if (_customersProvider.TryGetCustomerById(id, out var customer))
                    return customer;
                throw new Exception();
            }
        }

        private CustomerOrder GetRandomOrder()
        { 
            _ordersProvider.GetNext(out var order, restartContainer: true);
            return order;
        }
    }
}