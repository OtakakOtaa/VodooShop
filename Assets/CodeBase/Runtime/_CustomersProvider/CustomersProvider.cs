using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;
using JetBrains.Annotations;

// ReSharper disable PossibleMultipleEnumeration

namespace CodeBase.Runtime._CustomersProvider
{
    public sealed class CustomersProvider
    {
        private readonly Customer[] _simpleCustomers;
        private readonly Dictionary<int, PlotCustomer> _storyLine;

        public CustomersProvider(IEnumerable<Customer> simpleCustomers, Dictionary<int, PlotCustomer> storyLine)
        {
            _simpleCustomers = simpleCustomers.ToArray();
            _storyLine = storyLine;
        }
        
        public bool TryGetCustomerById(string name, out Customer customer)
        {
            customer = null;
            var simple = _simpleCustomers.Where(c => c.Name == name);
            var plot = _storyLine
                .Where(p => p.Value.Name == name)
                .Select(p => p.Value);

            if (simple.Any() is false && plot.Any() is false)
                return false;

            customer = simple.Any() is false ? plot.First() : simple.First();
            return true;
        }

        [CanBeNull]
        public PlotCustomer TryGetPlotCustomerThatDay(int day)
            => _storyLine.TryGetValue(day, out var customer) ? customer : null;

        public IEnumerable<Customer> AllCustomers => _simpleCustomers.Union(_storyLine.Values.Distinct());
        public IEnumerable<Customer> SimpleCustomers => _simpleCustomers;
        public IEnumerable<PlotCustomer> PlotCustomers => _storyLine.Values.Distinct();
        
    }
}