using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States;
using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.StatesFactory;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using CodeBase.Infrastructure.Collections;
using CodeBase.Infrastructure.Di;
using VContainer;
using VContainer.Unity;

namespace CodeBase.GlobalRule.ScopeConfigurator
{
    public sealed class ProjectGlobalRuleConfigurator : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureGlobalStateMachine(builder);
        }
        
        private void ConfigureGlobalStateMachine(IContainerBuilder builder)
        {
            TypesCollector finalStatesList = new(new[]
            {
                typeof(MainMenuState),
                typeof(MainGameState),
                typeof(CurtainState),
            });

            builder.Register<GlobalGameGameStateMachine>(Lifetime.Singleton)
                .As<ICurrentGameStateProvider>()
                .AsSelf()
                .WithParameter(finalStatesList);

            builder.Register<StateFactoryWithDiScopeOrigin>(Lifetime.Singleton)
                .As<IStateFactory>()
                .WithParameter(finalStatesList);

            builder.Register<SceneScopeProvider>(Lifetime.Singleton);
        }

    }
}