using CodeBase.Runtime.FSM.GameStates._MainMenuState;
using UnityEngine;
using Zenject;

namespace CodeBase.Runtime.UI
{
    public class MainMenuUIBinder : MonoBehaviour
    {
        [Inject] private IMainMenuState _mainMenuState;

        public void ExitGame()
            => _mainMenuState.ForceExitGame();
        
        public void StartGame()
            => _mainMenuState.ForceStartGame();
    }
}