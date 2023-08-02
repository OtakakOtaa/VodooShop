using CodeBase.Configuration.Data.MainConfig;
using CodeBase.GlobalRule.ScopeConfigurator;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Runtime;
using CodeBase.Infrastructure.Runtime.Contracts;
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
            
            builder.Register<GameConfigResolver>(Lifetime.Singleton)
                .As<IMainConfigProvider>();
        }

        private void ConfigureApplicationGate(IContainerBuilder builder)
        {
            builder.Register<ApplicationGates>(Lifetime.Singleton)
                .As<IApplicationGates>()
                .WithParameter(_sceneConfiguration);
        }
    }
}