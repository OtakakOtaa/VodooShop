using System;
using CodeBase.Infrastructure.Collections;
using CodeBase.Infrastructure.Di;
using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.StatesFactory
{
    public class StateFactoryWithDiScopeOrigin : IStateFactory
    {
        private readonly TypesCollector _stateTypes;
        private readonly SceneScopeProvider _scopeProvider;

        public StateFactoryWithDiScopeOrigin(TypesCollector stateTypes, SceneScopeProvider scopeProvider)
        {
            _stateTypes = stateTypes;
            _scopeProvider = scopeProvider;
        }

        public IState Create<TState>() where TState : class, IState
        {
            CheckCreationStateType<TState>();
            return _scopeProvider.GetScope().CreateInstanceFromContainer<TState>();
        }

        public IStateWithPayload<TPayload> CreateStateWithPayload<TPayload, TStateWithPayload>()
            where TStateWithPayload : class, IStateWithPayload<TPayload>
        {
            CheckCreationStateType<IStateWithPayload<TPayload>>();
            return _scopeProvider.GetScope().CreateInstanceFromContainer<IStateWithPayload<TPayload>>();
        }

        private void CheckCreationStateType<TState>() where TState : IState
        {
            if (_stateTypes.HasType<TState>() is false)
                throw _notAvailableStateEx;
        }

        private readonly Exception _notAvailableStateEx = new("operations with this state are blocked");
    }
}