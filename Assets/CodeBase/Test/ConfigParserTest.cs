using System.Collections;
using CodeBase.Editor.OriginGameConfig;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;

namespace CodeBase.Test
{
    public class ConfigParserTest
    {
        [UnityTest] public IEnumerator BootstrapConfigLoader()
        {
            GameConfigOriginLoader gameConfigOriginLoader = new();
            yield return UniTask.WhenAll(gameConfigOriginLoader.FetchConfig()).ToCoroutine();
        }
    }
}