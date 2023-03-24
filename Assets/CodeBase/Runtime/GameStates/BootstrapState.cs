using CodeBase.Runtime.Infrastructure.FSM.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.Runtime.GameStates
{
    public class BootstrapState : State
    {
        private GameConfigProvider _configProvider;

        public BootstrapState(GameConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        
        public override void Enter()
        { 
            
        }

        private async void BootStrap()
        {
            GameConfiguration config = await _configProvider.GameConfiguration();
            
        } 
    }
}