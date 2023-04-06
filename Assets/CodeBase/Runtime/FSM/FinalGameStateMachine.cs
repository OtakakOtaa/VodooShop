using System.Collections.Generic;
using System.Linq;
using CodeBase.Runtime.FSM.GameStates._MainMenuState;
using CodeBase.Runtime.FSM.GameStates._ShopHallState;
using CodeBase.Runtime.Infrastructure.FSM;
using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.FSM
{
    public sealed class FinalGameStateMachine : IFinalGameStateMachine
    {
        private readonly IEnumerable<IState> _states;
        private IState _currentState;

        public FinalGameStateMachine(IShopHallState shopHallState, IMainMenuState mainMenuState)
        => _states = new IState[] { shopHallState, mainMenuState };


        public void Enter<TState>() where TState : IState
        {
            Exit();
            _currentState = GetState<TState>();
            _currentState.Enter();
        }

        public void Enter<TPayload, TStateWithPayload>(TPayload payload) 
            where TStateWithPayload : class, IStateWithPayload<TPayload>
        {
            Exit();
            _currentState = GetPayloadState<TPayload, TStateWithPayload>();
            ((TStateWithPayload)_currentState).Enter(payload);
        }

        private void Exit()
            => (_currentState as IExitableState)?.Exit();
        
        private IState GetState<TState>() where TState : IState
            => _states.OfType<TState>().First();
    
        private TStateWithPayload GetPayloadState<TPayload, TStateWithPayload>() 
            where TStateWithPayload : IStateWithPayload<TPayload>
            => _states.OfType<TStateWithPayload>().First();
    }
}