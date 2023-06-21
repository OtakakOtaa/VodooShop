using System;
using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.StatesFactory;
using CodeBase.Infrastructure.Collections;
using CodeBase.Infrastructure.FSM;
using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine
{
    public sealed class GlobalGameGameStateMachine : IFinalStateMachine, ICurrentGameStateProvider
    {
        private readonly TypesCollector _statesType;
        private readonly IStateFactory _stateFactory;
        
        public IState CurrentState { get; private set; }

        public GlobalGameGameStateMachine(TypesCollector statesType, IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _statesType = statesType;
        }

        public void Enter<TState>() where TState : IState, new()
        {
            CheckStateForAvailability<TState>();
            
            Exit();
            CurrentState = GetState<TState>();
            CurrentState.Enter();
        }

        public void Enter<TPayload, TStateWithPayload>(TPayload payload) 
            where TStateWithPayload : IStateWithPayload<TPayload>, new()
        {
            CheckStateForAvailability<TStateWithPayload>();
            Exit();
            CurrentState = GetPayloadState<TPayload, TStateWithPayload>(); 
            ((TStateWithPayload)CurrentState).Enter(payload);
        }

        private void Exit()
            => (CurrentState as IExitableState)?.Exit();

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

        private readonly Exception _notAvailableStateEx = new("operations with this state are blocked");
    }
}