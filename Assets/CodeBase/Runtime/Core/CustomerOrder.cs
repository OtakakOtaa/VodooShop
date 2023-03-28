using System;
using UnityEngine;

namespace CodeBase.Runtime.Core
{
    [Serializable] public sealed class CustomerOrder
    {
        [SerializeField] private float _reward;
        [SerializeField] private string _requestedItem;
        [SerializeField] private Dialogue _dialogue;

        public CustomerOrder(float reward, string requestedItem, Dialogue dialogue)
        {
            _dialogue = dialogue;
            _reward = reward;
            _requestedItem = requestedItem;
        }

        public float Reward => _reward;
        public string RequestedItem => _requestedItem;
        public Dialogue Dialogue => _dialogue;
    }
}