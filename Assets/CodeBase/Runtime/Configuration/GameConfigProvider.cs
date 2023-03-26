using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Runtime.Configuration
{
    public sealed class GameConfigProvider
    {
        public const string MainConfigurationPatch = "Assets/Resources/MainGameConfiguration.asset";
        public async UniTask<GameConfiguration> GetGameConfiguration()
            => (GameConfiguration) await Resources.LoadAsync<GameConfiguration>(MainConfigurationPatch).ToUniTask();
    }
}