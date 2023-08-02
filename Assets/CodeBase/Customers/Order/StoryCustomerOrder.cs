using System;
using CodeBase.Customers.Data;

namespace CodeBase.Customers.Order
{
    [Serializable] public class StoryCustomerOrder : CustomerOrder
    {
        public StoryCustomerOrder(Customer holder, float reward, string requestedItem, Dialogue dialogue) 
            : base(holder, reward, requestedItem, dialogue) { }
    }
}