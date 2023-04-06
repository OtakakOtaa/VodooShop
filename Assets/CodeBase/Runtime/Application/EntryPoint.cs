using CodeBase.Runtime.FSM.GameStates._MainMenuState;
using CodeBase.Runtime.Infrastructure.FSM;
using UnityEngine;
using Zenject;

namespace CodeBase.Runtime.Application
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private IFinalGameStateMachine _gameStateMachine;
        
        public void Start() 
            => _gameStateMachine.Enter<IMainMenuState>();
    }
}