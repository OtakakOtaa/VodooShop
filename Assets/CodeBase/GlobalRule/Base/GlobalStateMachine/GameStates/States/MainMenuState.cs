using System;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using CodeBase.GlobalRule.Base.UIRule.MainMenuRule;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Runtime;
using CodeBase.Infrastructure.Runtime.Contracts;
using CodeBase.Infrastructure.Runtime.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States
{
    public sealed class MainMenuState : IState
    {
        private readonly GlobalGameStateMachine _globalGameStateMachine;
        private readonly IApplicationGates _applicationGates;
        private readonly MainMenuUIBinder _menuUIBinder;

        public MainMenuState(GlobalGameStateMachine globalGameStateMachine, IApplicationGates applicationGates,
            MainMenuUIBinder mainMenuUIBinder)
        {
            _menuUIBinder = mainMenuUIBinder;
            _applicationGates = applicationGates;
            _globalGameStateMachine = globalGameStateMachine;
        }

        public void Enter()
        {
            _menuUIBinder.BindUI();
        }

        public void EnterMainGame()
            => _globalGameStateMachine.Enter<Type, CurtainState>(typeof(MainGameState));

        public void ExitGame()
            => _applicationGates.Exit();
    }
}