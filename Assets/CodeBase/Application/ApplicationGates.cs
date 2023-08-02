using System;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Runtime;
using CodeBase.Infrastructure.Runtime.Contracts;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static CodeBase.Infrastructure.Runtime.PreProcessDelegateMethods.UnityPreprocessorConditions;
using static UnityEngine.SceneManagement.SceneManager;

namespace CodeBase.Application
{
    public sealed class ApplicationGates : IApplicationGates
    {
        private Configuration SceneConfiguration { get; }

        public ApplicationGates(Configuration configuration)
            => SceneConfiguration = configuration;

        public void Exit()
        {
            InvokeIfUnityEditor(() => EditorApplication.isPlaying = false);
            UnityEngine.Application.Quit();
        }

        public async UniTask LoadMainMenu()
            => await LoadSceneAsync(SceneConfiguration.MainMenuScene).ToUniTask();

        public async UniTask LoadShopHall()
            => await LoadSceneAsync(SceneConfiguration.ShopHallScene).ToUniTask();

        [RuntimeInitializeOnLoadMethod]
        private static void Bootstrap()
        {
            InvokeIfUnityEditor(() => EditorWindow.focusedWindow.maximized = true);
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