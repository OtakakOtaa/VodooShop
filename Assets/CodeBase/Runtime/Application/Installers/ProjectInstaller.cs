using CodeBase.Runtime.Configuration;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Runtime.Application.Installers
{
    public class ProjectInstaller : LifetimeScope
    {
        [SerializeField] private ApplicationGates.Settings _scenesSettings;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameConfigProvider>(Lifetime.Singleton);
            builder.Register<ApplicationGates>(Lifetime.Singleton).WithParameter(_scenesSettings);
            builder.Register<GameConfigProvider>(Lifetime.Scoped);
        }
    }
}