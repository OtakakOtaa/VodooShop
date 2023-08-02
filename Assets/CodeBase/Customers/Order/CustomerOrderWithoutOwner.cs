using System;
using CodeBase.Customers.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Customers.Order
{
    [Serializable] public class CustomerOrderWithoutOwner : CustomerOrder
    {
        public CustomerOrderWithoutOwner(float reward, string requestedItem, Dialogue dialogue) 
            : base(null, reward, requestedItem, dialogue) { }

        public void BindTemporaryCustomer(Customer customer) 
            => _holder = customer;

        public void UnBindTemporaryCustomer() 
            => _holder = null;

        public bool HasOwner
            => _holder is not null;

        [CanBeNull] public override Customer Holder => _holder;
    }
}