using System.Linq;
using CodeBase.Customers.Data.View;
using UnityEngine;

namespace CodeBase.Configuration.Data
{
    [CreateAssetMenu(menuName = nameof(CustomersViewConfiguration), fileName = nameof(CustomersViewConfiguration), order = default)]
    public sealed class CustomersViewConfiguration : ScriptableObject
    {
        [SerializeField] private CustomerView[] _customerViews;

        public CustomerView FindById(string id) 
            => _customerViews.FirstOrDefault(c => c.Id == id);
    }
}