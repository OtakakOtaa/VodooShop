using CodeBase.Runtime._CustomersProvider;
using CodeBase.Runtime._CustomersProvider.Pool;
using CodeBase.Runtime.Configuration;
using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.FSM.GameStates
{
    public sealed class BootstrapState : IState
    {
        private readonly GameConfigProvider _configProvider;

        public BootstrapState(GameConfigProvider configProvider)
            => _configProvider = configProvider;

        public void Enter()
        {
            BootStrap();
        }
        
        private async void BootStrap()
        {
            var config = await _configProvider.GetGameConfiguration();
            CustomersProvider customersProvider = new (config.SimpleCustomers, config.StoryLine);
            CustomersOnDayProvider dayProvider = 
                new(customersProvider, config.CustomersPools, config.OrdersWithoutOwners);
        } 
    }
}