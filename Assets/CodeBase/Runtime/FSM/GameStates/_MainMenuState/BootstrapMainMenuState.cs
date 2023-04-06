using System.Collections.Generic;
using CodeBase.Runtime.UI;
using Zenject;

namespace CodeBase.Runtime.FSM.GameStates._MainMenuState
{
    public sealed class BootstrapMainMenuState : IMainMenuState
    {
        private readonly DiContainer _diContainer;
        private MainMenuState _mainMenuState;

        public BootstrapMainMenuState(DiContainer diContainer)
            => _diContainer = diContainer;

        public void Enter()
        {
            _mainMenuState ??= _diContainer.Instantiate<MainMenuState>();
            _diContainer.Resolve<MainMenuUIBinder>();
            _diContainer.InjectExplicit(
                _diContainer.Resolve<MainMenuUIBinder>(),
                new List<TypeValuePair> 
                {
                    new() { Type = typeof(IMainMenuState), Value = _mainMenuState }
                });
            _mainMenuState.Enter();
        }

        public void ForceStartGame()
        {
        }

        public void ForceExitGame()
        {
        }
    }
}