using CodeBase.GlobalRule.Base.UIRule.MainMenuRule;
using CodeBase.Infrastructure.Runtime.Di;
using VContainer;

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
            builder.Register<MainMenuUIBinder>(Lifetime.Singleton);
        }
    }
}