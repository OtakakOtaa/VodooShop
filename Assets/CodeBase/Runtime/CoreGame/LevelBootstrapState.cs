using CodeBase.Runtime.Configuration;

namespace CodeBase.Runtime.CoreGame
{
    public sealed class LevelBootstrapState
    {
        private readonly GameConfigProvider _gameConfigProvider;
        
        
        public void Enter()
        {
            _gameConfigProvider.GetGameConfiguration();
        }
    }
}