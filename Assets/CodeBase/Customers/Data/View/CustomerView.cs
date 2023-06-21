using System;
using UnityEngine;

namespace CodeBase.Customers.Data.View
{
    [Serializable] public sealed class CustomerView
    {
        [SerializeField] private string _customerId;
        [SerializeField] private Sprite _sprite;

        public string Id => _customerId;
        public Sprite Sprite => _sprite;
    }
}