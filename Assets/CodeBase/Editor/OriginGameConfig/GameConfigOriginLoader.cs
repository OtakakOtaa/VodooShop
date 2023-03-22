using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Editor.OriginGameConfig.TableParsers;
using CodeBase.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace CodeBase.Editor.OriginGameConfig
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
            
            _configTableParser.Parse(rawTablesData);
            return null;
        }

        private async Task<Dictionary<string, string>> LoadRawData()
            => (await UniTask.WhenAll(_originSettings.Pages
                .Select(async pair => new KeyValuePair<string, string>(pair.Key, await FetchFromTable(pair.Value)))
            )).ToDictionary(pair => pair.Key, pair => pair.Value);


        private async UniTask<string> FetchFromTable(string originPath)
            => (await UnityWebRequest.Get(originPath).SendWebRequest()).downloadHandler.text;

        public class OriginSettings
        {
            private const string RootUrl =
                "https://docs.google.com/spreadsheets/d/18tM5AINSJw7L4NV0i85Un1JLPVA612AVH4bTBlZK6iY/export?format=csv&gid=";

            private const string DayTableId = "81119051";
            private const string CustomerTableId = "972599682";
            private const string CustomerPoolTableId = "909201559";
            private const string OrderTableId = "262418980";
            private const string DialogueTableId = "688924706";

            public Dictionary<string, string> Pages => BuildPages();

            private Dictionary<string, string> BuildPages()
                => new()
                {
                    [DayKey] = RootUrl + DayTableId,
                    [OrderKey] = RootUrl + OrderTableId,
                    [CustomerKey] = RootUrl + CustomerTableId,
                    [DialogueKey] = RootUrl + DialogueTableId,
                    [PoolKey] = RootUrl + CustomerPoolTableId,
                };

            public string DayKey => nameof(DayKey);
            public string OrderKey => nameof(OrderKey);
            public string CustomerKey => nameof(CustomerKey);
            public string PoolKey => nameof(PoolKey);
            
            public string DialogueKey => nameof(DialogueKey);
        }
    }
}