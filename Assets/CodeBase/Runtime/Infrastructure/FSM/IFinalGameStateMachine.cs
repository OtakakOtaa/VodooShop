using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.Infrastructure.FSM
{
    public interface IFinalGameStateMachine
    {
        void Enter<TState>() where TState : IState;

        void Enter<TPayload, TStateWithPayload>(TPayload payload)
            where TStateWithPayload : class, IStateWithPayload<TPayload>;
    }    
}
