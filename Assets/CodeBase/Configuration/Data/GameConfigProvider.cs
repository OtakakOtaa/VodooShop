using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Configuration.Data
{
    public sealed class GameConfigProvider
    {
        public const string MainConfigurationPatch = "Assets/Resources/MainGameConfiguration.asset";
        private GameConfiguration _cachedGameConfiguration;
        
        public async UniTask<GameConfiguration> GetGameConfiguration()
        {
            _cachedGameConfiguration ??=
                (GameConfiguration)await Resources.LoadAsync<GameConfiguration>(MainConfigurationPatch).ToUniTask();
            return _cachedGameConfiguration;
        }
    }
}