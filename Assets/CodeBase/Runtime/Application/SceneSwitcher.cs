using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Runtime.Application
{
    public sealed class SceneSwitcher
    {
        private readonly Settings _settings;

        public SceneSwitcher(Settings settings)
            => _settings = settings;

        public async void LoadMainMenu()
            => await SceneManager.LoadSceneAsync(_settings.MainMenuScene).ToUniTask();
        
        public async void LoadShopHall()
            => await SceneManager.LoadSceneAsync(_settings.ShopHallScene).ToUniTask();

        public async void LoadCraftTableScene()
            => await SceneManager.LoadSceneAsync(_settings.CraftTableScene).ToUniTask();


        [Serializable] public sealed class Settings
        {
            [SerializeField] private string _mainMenuScene;
            [SerializeField] private string _shopHallScene;
            [SerializeField] private string _craftTableScene;

            public string MainMenuScene => _mainMenuScene;
            public string ShopHallScene => _shopHallScene;
            public string CraftTableScene => _craftTableScene;
        }
    }
}