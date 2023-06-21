using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine
{
    public interface ICurrentGameStateProvider
    {
        IState CurrentState { get; }
    }
}