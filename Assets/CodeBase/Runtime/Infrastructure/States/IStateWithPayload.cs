namespace CodeBase.Runtime.Infrastructure.States
{
    public abstract class StateWithPayload<TPayload> : State
    {
        public abstract void Enter(TPayload payload);
        public override void Enter() { }
    }    
}

