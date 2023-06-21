using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using UnityEngine;

namespace CodeBase.GlobalRule.Base.UIRule
{
    public sealed class MainMenuUIMediator : MonoBehaviour
    {
        private ICurrentGameStateProvider _gameStateProvider;
        
        public void EnterToMainGame()
            => MainMenuState.EnterMainGame();
        
        public void ExitGame()
            => MainMenuState.ExitGame();

        private MainMenuState MainMenuState => (MainMenuState)_gameStateProvider.CurrentState;
    }
}