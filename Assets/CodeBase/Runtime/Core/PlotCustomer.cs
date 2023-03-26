using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Core._Customer;
using UnityEngine;

namespace CodeBase.Runtime.Core
{
    [Serializable] public sealed class PlotCustomer : Customer
    {
        [SerializeField] private CustomerOrder[] _story;
        [SerializeField] private int _currentStoryPosition;
        
        public PlotCustomer(string name, IEnumerable<CustomerOrder> story) : base(name)
            => _story = story.ToArray();

        public bool NextPart(out CustomerOrder storyPart)
        {
            storyPart = null;
            if (_currentStoryPosition == _story.Length) return false;
            storyPart = _story[_currentStoryPosition++];
            return true;
        }
    }
}