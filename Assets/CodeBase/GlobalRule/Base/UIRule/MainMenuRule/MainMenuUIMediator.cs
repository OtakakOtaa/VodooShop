using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using UnityEngine;
using VContainer;

namespace CodeBase.GlobalRule.Base.UIRule.MainMenuRule
{
    public sealed class MainMenuUIMediator : MonoBehaviour
    {
        [Inject] private ICurrentGameStateProvider _gameStateProvider;

        public void EnterToMainGame()
            => MainMenuState.EnterMainGame();

        public void ExitGame()
            => MainMenuState.ExitGame();

        private MainMenuState MainMenuState => (MainMenuState)_gameStateProvider.CurrentState;
    }
}