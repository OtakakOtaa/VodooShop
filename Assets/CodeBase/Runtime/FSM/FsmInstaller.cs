using CodeBase.Runtime.FSM.GameStates._MainMenuState;
using CodeBase.Runtime.FSM.GameStates._ShopHallState;
using Zenject;

namespace CodeBase.Runtime.FSM
{
    public sealed class FsmInstaller : Installer<FsmInstaller>
    {
        public override void InstallBindings()
        {
            BindFsm();
            BindBootstrapStates();
        }

        private void BindFsm()
            => Container.BindInterfacesTo<FinalGameStateMachine>().AsSingle();

        private void BindBootstrapStates()
        {
            Container.BindInterfacesTo<BootstrapMainMenuState>().AsSingle();
            Container.BindInterfacesTo<BootstrapShopHallState>().AsSingle();
        }
    }
}