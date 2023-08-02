using System;

namespace CodeBase.Infrastructure.Runtime.Contracts.GameProgress
{
    public interface IHavePersistantState<TState> where TState : class
    {
        event Action StateHasChanged;
        
        TState PersistantState { get; }
        void PutState(TState state);
    }
}