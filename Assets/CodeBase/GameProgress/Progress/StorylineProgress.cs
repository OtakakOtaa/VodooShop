
using System;
using CodeBase.Configuration.Data.MainConfig;
using CodeBase.Infrastructure.Runtime.Contracts.GameProgress;
using UnityEngine;

namespace CodeBase.GameProgress.Progress
{
    public sealed class StorylineProgress : IHavePersistantState<StorylineProgress.State>
    {
        private readonly IMainConfigProvider _mainConfigProvider;
        public State PersistantState { private set; get; } = default;

        public event Action StateHasChanged;
        
        public StorylineProgress(IMainConfigProvider mainConfigProvider)
        {
            _mainConfigProvider = mainConfigProvider;
        }

        public void PutState(State state)
            => PersistantState = state;

        public void ForceLevel()
        {
            
            PersistantState = new State(PersistantState.Level + 1, false);
        }

        [Serializable] public sealed class State
        {
            [SerializeField] private int _level;
            [SerializeField] private bool _storyEnded;
            
            public State(int level, bool storyEnded)
            {
                _storyEnded = storyEnded;
                _level = level;
            }

            public int Level => _level;
            public bool StoryEnded => _storyEnded;
        }
    }
}