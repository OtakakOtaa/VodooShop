using System.Collections.Generic;
using System.Linq;
using CodeBase.Configuration.Data;
using CodeBase.Configuration.Data.MainConfig;
using CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers.Tables;

namespace CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers
{
    public sealed class ConfigTableParser
    {
        private readonly GameConfigOriginLoader.OriginSettings _originSettings;
        
        private readonly GeneralTableParser _generalTableParser = new();
        private readonly DayTableParser _dayTableParser = new ();
        private readonly OrderTableParser _orderTableParser = new ();
        private readonly CustomersParser _customersParser = new ();
        private readonly CustomerPoolsParser _customerPoolsParser = new ();
        private readonly DialogueParser _dialogueParser = new ();
        
        public ConfigTableParser(GameConfigOriginLoader.OriginSettings originSettings) 
            => _originSettings = originSettings;

        public GameConfiguration Parse(Dictionary<string, string> rawTables)
        {
            GameConfigMapper.MapperDataContainer container = new()
            {
                General = _generalTableParser.Parse(rawTables[_originSettings.GeneralKey]).First(),
                Days = _dayTableParser.Parse(rawTables[_originSettings.DayKey]).ToArray(),
                Orders = _orderTableParser.Parse(rawTables[_originSettings.OrderKey]).ToArray(),
                Customers = _customersParser.Parse(rawTables[_originSettings.CustomerKey]).ToArray(),
                Pools = _customerPoolsParser.Parse(rawTables[_originSettings.PoolKey]).ToArray(),
                Dialogues = _dialogueParser.Parse(rawTables[_originSettings.DialogueKey]).ToArray()
            };
            return new GameConfigMapper(container).TranslateToConfigFormat();
        }
        
    }
}