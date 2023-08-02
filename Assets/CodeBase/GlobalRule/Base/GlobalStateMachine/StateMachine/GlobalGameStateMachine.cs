using System;
using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.StatesFactory;
using CodeBase.Infrastructure.Runtime.Collections;
using CodeBase.Infrastructure.Runtime.FSM;
using CodeBase.Infrastructure.Runtime.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine
{
    public sealed class GlobalGameStateMachine : IFinalStateMachine, ICurrentGameStateProvider
    {
        private readonly TypesCollector _statesType;
        private readonly IStateFactory _stateFactory;
        
        public IState CurrentState { get; private set; }

        public GlobalGameStateMachine(TypesCollector statesType, IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _statesType = statesType;
        }

        public void Enter<TState>() where TState : class, IState
        {
            CheckStateForAvailability<TState>();
            
            Exit();
            CurrentState = GetState<TState>();
            CurrentState.Enter();
        }

        public void Enter<TPayload, TStateWithPayload>(TPayload payload) 
            where TStateWithPayload : class, IStateWithPayload<TPayload>
        {
            CheckStateForAvailability<TStateWithPayload>();
            Exit();
            CurrentState = GetPayloadState<TPayload, TStateWithPayload>(); 
            ((TStateWithPayload)CurrentState).Enter(payload);
        }

        private void Exit()
            => (CurrentState as IExitableState)?.Exit();

        private IState GetState<TState>() where TState : class, IState 
            => _stateFactory.Create<TState>();

        private IStateWithPayload<TPayload> GetPayloadState<TPayload, TStateWithPayload>() 
            where TStateWithPayload : class, IStateWithPayload<TPayload>
            => _stateFactory.CreateStateWithPayload<TPayload, TStateWithPayload>();

        private void CheckStateForAvailability<TState>() where TState : IState
        {
            if (_statesType.HasType<TState>() is false) 
                throw _notAvailableStateEx;
        }

        private readonly Exception _notAvailableStateEx = new("operations with this state are blocked");
    }
}