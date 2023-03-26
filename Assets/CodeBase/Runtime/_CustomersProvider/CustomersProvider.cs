using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Core._Customer;

namespace CodeBase.Runtime._CustomersProvider
{
    public sealed class CustomersProvider
    {
        private readonly Customer[] _allCustomers;

        public CustomersProvider(IEnumerable<Customer> customers)
            => _allCustomers = customers.ToArray();

        public bool TryGetCustomerByName(string name, out Customer customer)
        {
            customer = null;
            try
            {
                customer = _allCustomers.First(c=> c.Name == name);
                return true;
            }
            catch (Exception _) { return false; }
        }
    }
}