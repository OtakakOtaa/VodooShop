using System.Linq;
using CodeBase.Runtime.Core._Customer.View;
using UnityEngine;

namespace CodeBase.Runtime.Configuration
{
    [CreateAssetMenu(menuName = nameof(CustomersViewConfiguration), fileName = nameof(CustomersViewConfiguration), order = default)]
    public sealed class CustomersViewConfiguration : ScriptableObject
    {
        [SerializeField] private CustomerView[] _customerViews;

        public CustomerView FindById(string id) 
            => _customerViews.FirstOrDefault(c => c.Id == id);
    }
}