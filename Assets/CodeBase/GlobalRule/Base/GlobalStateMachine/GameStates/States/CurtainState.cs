using System;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.FSM.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States
{
    public sealed class CurtainState : IStateWithPayload<Type>
    {
        private readonly GlobalGameStateMachine _gameStateMachine;
        private readonly IApplicationGates _applicationGates;

        public CurtainState(GlobalGameStateMachine gameStateMachine, IApplicationGates applicationGates)
        {
            _gameStateMachine = gameStateMachine;
            _applicationGates = applicationGates;
        }

        public void Enter(Type payload)
            => Play(payload).Forget();

        private async UniTaskVoid Play(Type loadingState)
        {
            if (loadingState == typeof(MainMenuState))
            {
                await _applicationGates.LoadMainMenu();
                _gameStateMachine.Enter<MainMenuState>();
            }
            
            if (loadingState == typeof(MainGameState))
            {
                await _applicationGates.LoadShopHall();
                _gameStateMachine.Enter<MainGameState>();
            }
        }
        
        
        public void Enter() { }
    }
}