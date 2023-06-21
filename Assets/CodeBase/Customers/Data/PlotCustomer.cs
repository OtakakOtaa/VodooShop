using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Customers.Data
{
    [Serializable] public sealed class PlotCustomer : Customer
    {
        [SerializeField] private CustomerOrder[] _story;
        [SerializeField] private int _currentStoryPosition;
        
        public PlotCustomer(string id, string name, IEnumerable<CustomerOrder> story) : base(id, name)
            => _story = story.ToArray();

        public CustomerOrder CustomerOrder => _story[_currentStoryPosition];

        public void ForceStoryLine()
            => _currentStoryPosition = Math.Clamp(++_currentStoryPosition, 0, _story.Length - 1);
    }
}