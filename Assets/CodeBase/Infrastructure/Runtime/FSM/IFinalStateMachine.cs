using CodeBase.Infrastructure.Runtime.FSM.States;

namespace CodeBase.Infrastructure.Runtime.FSM
{
    public interface IFinalStateMachine
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TPayload, TStateWithPayload>(TPayload payload)
            where TStateWithPayload : class, IStateWithPayload<TPayload>;
    }
}
