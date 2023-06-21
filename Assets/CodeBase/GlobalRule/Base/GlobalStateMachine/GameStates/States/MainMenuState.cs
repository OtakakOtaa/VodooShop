using System;
using CodeBase.Application;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States
{
    public sealed class MainMenuState : IState
    {
        private readonly GlobalGameGameStateMachine _globalGameStateMachine;
        private readonly ApplicationGates _applicationGates;

        public MainMenuState(GlobalGameGameStateMachine globalGameStateMachine, ApplicationGates applicationGates)
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