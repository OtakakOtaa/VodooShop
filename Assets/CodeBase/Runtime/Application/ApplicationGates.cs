using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace CodeBase.Runtime.Application
{
    public sealed class ApplicationGates
    {
        public Settings SceneSettings { get; private set; }

        public ApplicationGates(Settings settings)
            => SceneSettings = settings;

        
        [RuntimeInitializeOnLoadMethod]
        private static void Bootstrap()
        {
#if UNITY_EDITOR
            EditorWindow.focusedWindow.maximized = true;
#endif
        }
        
        public static void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            UnityEngine.Application.Quit();
        }
        
        
        public async UniTask LoadMainMenu()
            => await LoadSceneAsync(SceneSettings.MainMenuScene).ToUniTask();
        
        public async UniTask LoadShopHall()
            => await LoadSceneAsync(SceneSettings.ShopHallScene).ToUniTask();
        
        [Serializable] public sealed class Settings
        {
            [SerializeField, ApplicationScene] private string _mainMenuScene;
            [SerializeField, ApplicationScene] private string _shopHallScene;
            
            public string MainMenuScene => _mainMenuScene;
            public string ShopHallScene => _shopHallScene;
        }
    }
}