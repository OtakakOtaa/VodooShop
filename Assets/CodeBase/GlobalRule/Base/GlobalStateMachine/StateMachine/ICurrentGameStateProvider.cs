using CodeBase.Infrastructure.Runtime.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine
{
    public interface ICurrentGameStateProvider
    {
        IState CurrentState { get; }
    }
}