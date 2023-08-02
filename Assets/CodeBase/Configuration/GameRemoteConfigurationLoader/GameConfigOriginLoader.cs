using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Configuration.Data;
using CodeBase.Configuration.Data.MainConfig;
using CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace CodeBase.Configuration.GameRemoteConfigurationLoader
{
    public sealed class GameConfigOriginLoader
    {
        private readonly ConfigTableParser _configTableParser;
        private readonly OriginSettings _originSettings;

        public GameConfigOriginLoader()
        {
            _originSettings = new OriginSettings();
            _configTableParser = new ConfigTableParser(_originSettings);
        }

        public async UniTask<GameConfiguration> FetchConfig()
        {
            var rawTablesData = await LoadRawData();
            return _configTableParser.Parse(rawTablesData);
        }

        private async Task<Dictionary<string, string>> LoadRawData()
            => (await UniTask.WhenAll(_originSettings.Pages
                .Select(async pair => new KeyValuePair<string, string>(pair.Key, await FetchFromTable(pair.Value)))
            )).ToDictionary(pair => pair.Key, pair => pair.Value);

        private async UniTask<string> FetchFromTable(string originPath)
            => (await UnityWebRequest.Get(originPath).SendWebRequest()).downloadHandler.text;

        public class OriginSettings
        {
            public const string RootUrl =
                    "https://docs.google.com/spreadsheets/d/18tM5AINSJw7L4NV0i85Un1JLPVA612AVH4bTBlZK6iY/edit#gid=0";
            
            private const string RootUrlForDownload = "https://docs.google.com/spreadsheets" +
                                           "/d/18tM5AINSJw7L4NV0i85Un1JLPVA612AVH4bTBlZK6iY" +
                                           "/export?format=csv&gid=";

            private const string GeneralTableId = "0";
            private const string DayTableId = "81119051";
            private const string CustomerTableId = "972599682";
            private const string CustomerPoolTableId = "909201559";
            private const string OrderTableId = "262418980";
            private const string DialogueTableId = "688924706";

            public Dictionary<string, string> Pages => BuildPages();

            private Dictionary<string, string> BuildPages()
                => new()
                {
                    [GeneralKey] = RootUrlForDownload + GeneralTableId,
                    [DayKey] = RootUrlForDownload + DayTableId,
                    [OrderKey] = RootUrlForDownload + OrderTableId,
                    [CustomerKey] = RootUrlForDownload + CustomerTableId,
                    [DialogueKey] = RootUrlForDownload + DialogueTableId,
                    [PoolKey] = RootUrlForDownload + CustomerPoolTableId,
                };

            public string GeneralKey => nameof(GeneralKey);
            public string DayKey => nameof(DayKey);
            public string OrderKey => nameof(OrderKey);
            public string CustomerKey => nameof(CustomerKey);
            public string PoolKey => nameof(PoolKey);
            public string DialogueKey => nameof(DialogueKey);
        }
    }
}