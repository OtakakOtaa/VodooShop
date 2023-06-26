using System;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States
{
    public sealed class MainMenuState : IState
    {
        private readonly GlobalGameStateMachine _globalGameStateMachine;
        private readonly IApplicationGates _applicationGates;

        public MainMenuState(GlobalGameStateMachine globalGameStateMachine, IApplicationGates applicationGates)
        {
            _applicationGates = applicationGates;
            _globalGameStateMachine = globalGameStateMachine;
        }
        
        public void Enter() { }

        public void EnterMainGame()
            => _globalGameStateMachine.Enter<Type, CurtainState>(typeof(MainGameState));

        public void ExitGame()
            => _applicationGates.Exit();
    }
}