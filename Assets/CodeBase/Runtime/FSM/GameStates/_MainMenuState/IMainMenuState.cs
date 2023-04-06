using CodeBase.Runtime.Infrastructure.FSM.States;

namespace CodeBase.Runtime.FSM.GameStates._MainMenuState
{
    public interface IMainMenuState : IState
    {
        void ForceStartGame();
        void ForceExitGame();
    }
}