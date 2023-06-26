using CodeBase.GlobalRule.Base.UIRule;
using CodeBase.Infrastructure.Di;
using VContainer;
using VContainer.Unity;

namespace CodeBase.GlobalRule.ScopeConfigurator
{
    public sealed class MainMenuRuleConfigurator : ContainerRegisterScope
    {
        private readonly MainMenuUIMediator _menuUIMediator;
        
        public MainMenuRuleConfigurator(MainMenuUIMediator menuUIMediator)
        {
            _menuUIMediator = menuUIMediator;
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_menuUIMediator);
        }
    }
}