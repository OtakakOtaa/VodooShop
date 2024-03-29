using System;

using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Customers.Data
{
    [Serializable] public class Customer
    {
        [SerializeField] protected string _id;
        [SerializeField] protected string _name;
        
        public Customer(string id, string name)
        {
            _id = id;
            _name = name;
        }
        
        public string Name => _name;
        public string Id => _id;
    }
}