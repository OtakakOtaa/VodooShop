using System;
using CodeBase.Infrastructure.FSM.States;

namespace CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States
{
    public sealed class CurtainState : IStateWithPayload<Type>
    {
        public void Enter(Type payload)
        {
        }

        public void Enter()
        {
        }
    }
}