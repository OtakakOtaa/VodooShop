using CodeBase.Infrastructure.Runtime.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.StatesFactory
{
    public interface IStateFactory
    {
        IState Create<TState>() where TState : class, IState;
        IStateWithPayload<TPayload> CreateStateWithPayload<TPayload, TStateWithPayload>()
            where TStateWithPayload : class, IStateWithPayload<TPayload>;
    }
}