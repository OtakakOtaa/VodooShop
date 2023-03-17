namespace CodeBase.Runtime.Infrastructure.FSM.States
{
    public abstract class StateWithPayload<TPayload> : State
    {
        public abstract void Enter(TPayload payload);
        public override void Enter() { }
    }    
}

