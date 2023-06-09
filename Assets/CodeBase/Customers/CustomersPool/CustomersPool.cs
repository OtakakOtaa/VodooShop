using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Customers.CustomersPool
{
    [Serializable] public sealed class CustomersPool
    {
        [SerializeField] private int _levelScopeMinimum;
        [SerializeField] private int _levelScopeMaximum;
        
        [SerializeField] private string[] _customersKeys;
        
        public CustomersPool(Range levelScope, IEnumerable<string> customers)
        {
            _levelScopeMinimum = levelScope.Start.Value;
            _levelScopeMaximum = levelScope.End.Value;
            _customersKeys = customers.ToArray();
        }

        public Range LevelScope => new (_levelScopeMinimum, _levelScopeMaximum);
        public IEnumerable<string> CustomersKeys => _customersKeys;
    }
}