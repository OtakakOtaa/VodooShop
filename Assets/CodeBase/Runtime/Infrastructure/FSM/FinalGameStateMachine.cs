using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.Infrastructure.FSM.States;
using State = CodeBase.Runtime.Infrastructure.FSM.States.State;

namespace CodeBase.Runtime.Infrastructure.FSM
{
    public class FinalGameStateMachine
    {
        private readonly IEnumerable<State> _gameStates;
        private State _currentState;

        public FinalGameStateMachine(IEnumerable<State> states)
            => _gameStates = states;

        public void Enter<TState>() where TState : State
        {
            Exit();
            _currentState = GetState<TState>();
            _currentState.Enter();
        }

        public void Enter<TPayload, TStateWithPayload>(TPayload payload) where TStateWithPayload : StateWithPayload<TPayload>
        {
            Exit();
            _currentState = GetPayloadState<TPayload, TStateWithPayload>();
            ((TStateWithPayload)_currentState).Enter(payload);
        }

        private void Exit()
            => (_currentState as IExitableState)?.Exit();
    
        private State GetState<TState>() where TState : State
            => _gameStates.First(it => it.GetType() == typeof(TState));
    
        private TStateWithPayload GetPayloadState<TPayload, TStateWithPayload>() 
            where TStateWithPayload : StateWithPayload<TPayload>
            => (TStateWithPayload)_gameStates.First(it => (it as TStateWithPayload) is not null);

    }    
}
