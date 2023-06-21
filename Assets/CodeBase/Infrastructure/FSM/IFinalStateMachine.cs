using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.Infrastructure.FSM
{
    public interface IFinalStateMachine
    {
        void Enter<TState>() where TState : IState, new();

        void Enter<TPayload, TStateWithPayload>(TPayload payload)
            where TStateWithPayload : IStateWithPayload<TPayload>, new();
    }
}
