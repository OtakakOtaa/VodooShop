using System;
using CodeBase.Infrastructure.PreProcessDelegateMethods;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace CodeBase.Application
{
    public sealed class ApplicationGates
    {
        private Configuration SceneConfiguration { get; }

        public ApplicationGates(Configuration configuration)
            => SceneConfiguration = configuration;

        public void Exit()
        {
            UnityPreprocessorConditions.InvokeIfUnityEditor(() => EditorApplication.isPlaying = false);
            UnityEngine.Application.Quit();
        }

        public async UniTask LoadMainMenu()
            => await LoadSceneAsync(SceneConfiguration.MainMenuScene).ToUniTask();

        public async UniTask LoadShopHall()
            => await LoadSceneAsync(SceneConfiguration.ShopHallScene).ToUniTask();

        [RuntimeInitializeOnLoadMethod]
        private static void Bootstrap()
        {
            UnityPreprocessorConditions.InvokeIfUnityEditor(() => EditorWindow.focusedWindow.maximized = true);
        }

        [Serializable] public sealed class Configuration
        {
            [SerializeField, ApplicationScene] private string _mainMenuScene;
            [SerializeField, ApplicationScene] private string _shopHallScene;

            public string MainMenuScene => _mainMenuScene;
            public string ShopHallScene => _shopHallScene;
        }
    }
}