using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Runtime.Di
{
    public sealed class SceneScopeProvider
    {
        public LifetimeScope GetScope()
            => Object.FindObjectOfType<LifetimeScope>();

        public LifetimeScope GetParent()
            => Object.FindObjectOfType<LifetimeScope>().Parent;
    }
}