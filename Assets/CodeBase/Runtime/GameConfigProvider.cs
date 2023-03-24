using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Runtime
{
    public sealed class GameConfigProvider
    {
        public const string MainConfigurationPatch = "Assets/Resources/MainGameConfiguration.asset";
        public async UniTask<GameConfiguration> GameConfiguration()
            => (GameConfiguration) await Resources.LoadAsync<GameConfiguration>(MainConfigurationPatch).ToUniTask();
    }
}