using CodeBase.Runtime.Application;

namespace CodeBase.Runtime.FSM.GameStates._ShopHallState
{
    public class BootstrapShopHallState : IShopHallState
    {
        private readonly SceneSwitcher _sceneSwitcher;

        public BootstrapShopHallState(SceneSwitcher sceneSwitcher)
            => _sceneSwitcher = sceneSwitcher;

        public void Enter()
            => _sceneSwitcher.LoadShopHall();
    }
}