using CodeBase.Runtime._CustomersProvider;
using CodeBase.Runtime._CustomersProvider.Pool;
using CodeBase.Runtime.Configuration;
using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.GameStates
{
    public sealed class BootstrapState : State
    {
        private readonly GameConfigProvider _configProvider;

        public BootstrapState(GameConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        
        public override void Enter()
        { 
            
        }

        private async void BootStrap()
        {
            GameConfiguration config = await _configProvider.GetGameConfiguration();
            
            CustomersProvider customersProvider = new (config.AllCustomers);
            CustomersOnDayProvider customersOnDayProvider = new (customersProvider, config.CustomersPools, 
                config.OrdersWithoutOwners);
            
        } 
    }
}