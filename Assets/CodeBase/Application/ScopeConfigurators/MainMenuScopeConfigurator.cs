using CodeBase.GlobalRule.Base.UIRule;
using CodeBase.GlobalRule.ScopeConfigurator;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Application.ScopeConfigurators
{
    public sealed class MainMenuScopeConfigurator : LifetimeScope
    {
        [Header("References")]
        [SerializeField] private MainMenuUIMediator _menuUIMediator;

        protected override void Configure(IContainerBuilder builder)
        {
            new MainMenuRuleConfigurator(_menuUIMediator).Configure(builder);
        }
    }
}