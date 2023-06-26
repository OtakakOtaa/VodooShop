using System;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States
{
    public sealed class BootstrapState : IState
    {
        private readonly GlobalGameStateMachine _gameState;
        
        public BootstrapState(GlobalGameStateMachine gameStateMachine)
        {
            _gameState = gameStateMachine;
        }
        
        public void Enter()
            => _gameState.Enter<Type,CurtainState>(typeof(MainMenuState));
        
    }
}