using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States;
using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.StatesFactory;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using CodeBase.Infrastructure.Runtime.Collections;
using CodeBase.Infrastructure.Runtime.Di;
using VContainer;

namespace CodeBase.GlobalRule.ScopeConfigurator
{
    public sealed class GlobalStateMachineConfigurator : ContainerRegisterScope
    {
        private readonly TypesCollector _finalStatesList = new(new[]
        {
            typeof(MainMenuState),
            typeof(MainGameState),
            typeof(CurtainState),
        });

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<GlobalGameStateMachine>(Lifetime.Singleton)
                .As<ICurrentGameStateProvider>()
                .AsSelf()
                .WithParameter(_finalStatesList);

            builder.Register<StateFactoryWithDiScopeOrigin>(Lifetime.Singleton)
                .As<IStateFactory>()
                .WithParameter(_finalStatesList);
            
            builder.Register<SceneScopeProvider>(Lifetime.Singleton);
        }
    }
}