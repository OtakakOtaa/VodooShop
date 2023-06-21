using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.StatesFactory
{
    public interface IStateFactory
    {
        IState Create<TState>() where TState : IState, new();
        IStateWithPayload<TPayload> CreateStateWithPayload<TPayload, TStateWithPayload>()
            where TStateWithPayload : IStateWithPayload<TPayload>, new();
    }
}