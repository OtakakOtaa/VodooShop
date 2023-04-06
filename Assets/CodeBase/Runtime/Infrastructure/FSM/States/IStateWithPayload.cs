namespace CodeBase.Runtime.Infrastructure.FSM.States
{
    public interface IStateWithPayload<in TPayload> : IState
    {
        void Enter(TPayload payload);
    }    
}

