using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.FSM.GameStates.States
{
    public sealed class CurtainState : IStateWithPayload<string>
    {
        public void Enter(string payload)
        {
        }

        public void Enter()
        {
        }
    }
}