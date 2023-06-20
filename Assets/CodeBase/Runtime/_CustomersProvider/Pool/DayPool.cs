using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;
using CodeBase.Runtime.Infrastructure;
using CodeBase.Runtime.Infrastructure.Collections;

namespace CodeBase.Runtime._CustomersProvider.Pool
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