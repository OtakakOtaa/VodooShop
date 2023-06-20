using System;
using CodeBase.Runtime.Infrastructure.Collections;
using CodeBase.Runtime.Infrastructure.Di;
using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.FSM.GameStates.StatesFactory
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

        public IState Create<TState>() where TState : IState, new()
        {
            CheckCreationStateType<TState>();
            return _scopeProvider.GetScope().CreateInstanceFromContainer<TState>();
        }

        public IStateWithPayload<TPayload> CreateStateWithPayload<TPayload, TStateWithPayload>()
            where TStateWithPayload : IStateWithPayload<TPayload>, new()
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