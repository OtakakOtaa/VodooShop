using CodeBase.Runtime.Application;
using CodeBase.Runtime.FSM.GameStates._ShopHallState;
using CodeBase.Runtime.Infrastructure.FSM;
using Cysharp.Threading.Tasks;

namespace CodeBase.Runtime.FSM.GameStates._MainMenuState
{
    public class MainMenuState : IMainMenuState
    {
        private readonly IFinalGameStateMachine _stateMachine;
        
        private readonly ApplicationGate _applicationGate;

        private bool _uiActionTriggered;
        private bool _startGameFlag;
        private bool _exitGameFlag;


        public MainMenuState(IFinalGameStateMachine stateMachine, ApplicationGate applicationGate)
        {
            _stateMachine = stateMachine; 
            _applicationGate = applicationGate;
        }
            
        public void Enter()
            => GameStateCycle();

        private async void GameStateCycle()
        {
            await UniTask.WaitUntilValueChanged(this, s => s._uiActionTriggered);
            if (_startGameFlag)
                _stateMachine.Enter<ShopHallState>();
            if (_exitGameFlag)
                _applicationGate.Exit();
        }

        public void ForceStartGame()
        {
            _uiActionTriggered = true;
            _startGameFlag = true;
        }
        
        public void ForceExitGame()
        {
            _uiActionTriggered = true;
            _exitGameFlag = true;
        }
    }
}