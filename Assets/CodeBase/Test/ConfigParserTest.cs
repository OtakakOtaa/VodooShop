using System.Collections;
using CodeBase.Configuration.GameRemoteConfigurationLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.TestTools;

namespace CodeBase.Test
{
    public class ConfigParserTest
    {
        [UnityTest] public IEnumerator BootstrapConfigLoader()
        {
            GameConfigOriginLoader gameConfigOriginLoader = new();
            yield return gameConfigOriginLoader.FetchConfig().ToCoroutine();
        }
    }
}