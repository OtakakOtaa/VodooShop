using System;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Runtime.Core._Customer
{
    [Serializable] public class Customer
    {
        [SerializeField] protected string _name;
        [CanBeNull] public CustomerOrder Order { get; private set; }

        public Customer(string name)
            => _name = name;
        
        public string Name => _name;
        
        public void PutOrder(CustomerOrder customerOrder) 
            => Order = customerOrder;
    }
}