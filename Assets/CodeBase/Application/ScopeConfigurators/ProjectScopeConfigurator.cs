using CodeBase.GlobalRule.ScopeConfigurator;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Application.ScopeConfigurators
{
    public sealed class ProjectScopeConfigurator : LifetimeScope
    {
        [Header("Configuration")]
        [SerializeField] private ApplicationGates.Configuration _sceneConfiguration;

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureApplicationGate(builder);
            new GlobalStateMachineConfigurator().Configure(builder);
        }

        private void ConfigureApplicationGate(IContainerBuilder builder)
        {
            builder.Register<ApplicationGates>(Lifetime.Singleton).
                WithParameter(_sceneConfiguration);
        }
    }
}