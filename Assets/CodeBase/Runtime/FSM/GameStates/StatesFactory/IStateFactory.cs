using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.FSM.GameStates.StatesFactory
{
    public interface IStateFactory
    {
        IState Create<TState>() where TState : IState, new();
        IStateWithPayload<TPayload> CreateStateWithPayload<TPayload, TStateWithPayload>()
            where TStateWithPayload : IStateWithPayload<TPayload>, new();
    }
}