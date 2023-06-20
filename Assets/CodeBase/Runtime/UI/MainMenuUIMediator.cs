using CodeBase.Runtime.Application;
using UnityEngine;
using VContainer;

namespace CodeBase.Runtime.UI
{
    public class MainMenuUIMediator : MonoBehaviour
    {
        [Inject] private ApplicationGates _applicationGates;
        
        public void ExitGame()
            => ApplicationGates.Exit();
        
        public async void ContinueGame()
            => await _applicationGates.LoadShopHall();
        
        public void OpenShop()
        {
        }
    }
}