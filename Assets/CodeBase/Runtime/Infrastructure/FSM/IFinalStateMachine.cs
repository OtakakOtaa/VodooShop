using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.Infrastructure.FSM
{
    public interface IFinalStateMachine
    {
        void Enter<TState>() where TState : IState, new();

        void Enter<TPayload, TStateWithPayload>(TPayload payload)
            where TStateWithPayload : IStateWithPayload<TPayload>, new();
    }
}
