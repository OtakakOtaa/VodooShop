using CodeBase.Runtime.Application;
using CodeBase.Runtime.Configuration;
using CodeBase.Runtime.FSM;
using UnityEngine;
using Zenject;

namespace CodeBase.Runtime
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SceneSwitcher.Settings _scenesSettings;

        public override void InstallBindings()
        {
            Container.Bind<SceneSwitcher>().AsSingle().WithArguments(_scenesSettings);
            Container.Bind<GameConfigProvider>().AsSingle();
            Container.Bind<ApplicationGate>().AsSingle();
            
            FsmInstaller.Install(Container);
        }
    }
}