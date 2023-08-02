using System;
using System.Collections.Generic;
using CodeBase.Customers;
using CodeBase.Customers.CustomersPool;
using CodeBase.Customers.Data;
using CodeBase.Customers.Order;

namespace CodeBase.Configuration.Data.MainConfig
{
    public interface IMainConfigProvider
    {
        int LevelAmount { get; }

        TimeSpan Duration { get; }

        IEnumerable<CustomersPool> CustomersPools { get; }
        IEnumerable<CustomerOrder> OrdersWithoutOwners { get; }
        IEnumerable<Customer> SimpleCustomers { get; }
        IEnumerable<Customer> PlotCustomers { get; }
        IEnumerable<StoryLinePart> StoryLine { get; }
    }
}