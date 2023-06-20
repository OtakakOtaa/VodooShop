using System;
using System.Threading.Tasks;
using CodeBase.Runtime.FSM.GameStates.StatesFactory;
using CodeBase.Runtime.Infrastructure.Collections;
using CodeBase.Runtime.Infrastructure.FSM;
using CodeBase.Runtime.Infrastructure.FSM.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.Runtime.FSM.StateMachine
{
    public sealed class GlobalGameStateMachine : IFinalStateMachine
    {
        private readonly Exception _notAvailableStateEx = new("operations with this state are blocked");

        private readonly TypesCollector _statesType;
        private readonly IStateFactory _stateFactory;

        private IState _currentState;

        public GlobalGameStateMachine(TypesCollector statesType, IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _statesType = statesType;
        }
            
        public void Enter<TState>() where TState : IState, new()
        {
            CheckStateForAvailability<TState>();
            
            Exit();
            _currentState = GetState<TState>();
            _currentState.Enter();
        }

        public void Enter<TPayload, TStateWithPayload>(TPayload payload) 
            where TStateWithPayload : IStateWithPayload<TPayload>, new()
        {
            CheckStateForAvailability<TStateWithPayload>();
            Exit();
            _currentState = GetPayloadState<TPayload, TStateWithPayload>(); 
            ((TStateWithPayload)_currentState).Enter(payload);
        }

        private void Exit()
            => (_currentState as IExitableState)?.Exit();

        private IState GetState<TState>() where TState : IState, new() 
            => _stateFactory.Create<TState>();
        
        private IStateWithPayload<TPayload> GetPayloadState<TPayload, TStateWithPayload>() 
            where TStateWithPayload : IStateWithPayload<TPayload>, new()
            => _stateFactory.CreateStateWithPayload<TPayload, TStateWithPayload>();

        private void CheckStateForAvailability<TState>() where TState : IState
        {
            if (_statesType.HasType<TState>() is false) 
                throw _notAvailableStateEx;
        }
    }

}