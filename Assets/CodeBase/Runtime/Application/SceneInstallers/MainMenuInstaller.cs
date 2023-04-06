using CodeBase.Runtime.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Runtime.Application.SceneInstallers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuUIBinder _uiBinder;

        public override void InstallBindings()
            => BindUI();

        private void BindUI()
            => Container.Bind<MainMenuUIBinder>().FromInstance(_uiBinder);
    }
}