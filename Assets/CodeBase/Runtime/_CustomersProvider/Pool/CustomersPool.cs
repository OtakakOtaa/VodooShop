using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Runtime._CustomersProvider.Pool
{
    [Serializable] public sealed class CustomersPool
    {
        [SerializeField] private int _levelScopeMinimum;
        [SerializeField] private int _levelScopeMaximum;
        
        [SerializeField] private string[] _customersKeys;
        
        public CustomersPool(Range levelScope, IEnumerable<string> customers)
        {
            _levelScopeMinimum = levelScope.End.Value;
            _levelScopeMaximum = levelScope.Start.Value;
            _customersKeys = customers.ToArray();
        }

        public Range LevelScope => new (_levelScopeMinimum, _levelScopeMaximum);
        public IEnumerable<string> CustomersKeys => _customersKeys;
    }
}