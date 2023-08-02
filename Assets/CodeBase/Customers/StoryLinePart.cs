using System;
using CodeBase.Customers.Data;
using CodeBase.Customers.Order;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Customers
{
    [Serializable] public sealed class StoryLinePart
    {
        [SerializeField] private byte _levelNumber;
        [SerializeField] private StoryCustomerOrder _order;

        public StoryLinePart(int levelNumber, StoryCustomerOrder order)
        {
            _levelNumber = (byte)levelNumber;
            _order = order;
        }

        public byte LevelNumber => _levelNumber;
        public StoryCustomerOrder Order => _order;
    }
}