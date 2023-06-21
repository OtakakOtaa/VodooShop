using System.Collections.Generic;
using CodeBase.Customers.Data;
using CodeBase.Infrastructure.Collections;

namespace CodeBase.Customers.CustomersPool
{
    public sealed class DayPool
    {
        private readonly NonRepeatingCollectionElementGiver<Customer> _customerProvider;
        public PlotCustomer PlotCustomer { get; }

        public DayPool(IEnumerable<Customer> simpleCustomers, PlotCustomer plotCustomer = null)
        {
            _customerProvider = new NonRepeatingCollectionElementGiver<Customer>(simpleCustomers);
            if(plotCustomer is not null) 
                PlotCustomer = plotCustomer;
        }

        public Customer GetRandomCustomer()
        {
            _customerProvider.GetNext(out var customer, restartContainer: true);
            return customer;
        }
        
        public bool HasDayPlotCustomer => PlotCustomer is not null;
    }
}