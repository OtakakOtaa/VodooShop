using System;
using CodeBase.Customers.Data;
using UnityEngine;

namespace CodeBase.Customers.Order
{
    [Serializable] public abstract class CustomerOrder
    {
        [SerializeField] protected Customer _holder;
        [SerializeField] protected float _reward;
        [SerializeField] protected string _requestedItem;
        [SerializeField] protected Dialogue _dialogue;

        protected CustomerOrder(Customer holder, float reward, string requestedItem, Dialogue dialogue)
        {
            _holder = holder;
            _reward = reward;
            _requestedItem = requestedItem;
            _dialogue = dialogue;
        }

        public float Reward => _reward;
        public string RequestedItem => _requestedItem;
        public Dialogue Dialogue => _dialogue;
        public virtual Customer Holder => _holder;
    }
}