using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.Infrastructure.FSM
{
    public interface IFinalStateMachine
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TPayload, TStateWithPayload>(TPayload payload)
            where TStateWithPayload : class, IStateWithPayload<TPayload>;
    }
}
