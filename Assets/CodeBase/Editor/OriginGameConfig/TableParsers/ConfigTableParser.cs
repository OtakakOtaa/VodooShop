using System.Collections.Generic;
using CodeBase.Editor.OriginGameConfig.TableParsers.Tables;
using CodeBase.Runtime;

namespace CodeBase.Editor.OriginGameConfig.TableParsers
{
    public sealed class ConfigTableParser
    {
        private readonly GameConfigOriginLoader.OriginSettings _originSettings;

        private readonly DayTableParser _dayTableParser = new ();
        private readonly OrderTableParser _orderTableParser = new ();
        private readonly CustomersParser _customersParser = new ();
        private readonly CustomerPoolsParser _customerPoolsParser = new ();
        private readonly DialogueParser _dialogueParser = new ();
        
        public ConfigTableParser(GameConfigOriginLoader.OriginSettings originSettings) 
            => _originSettings = originSettings;

        public GameConfiguration Parse(Dictionary<string, string> rawTables)
        {
            _dayTableParser.Parse(rawTables[_originSettings.DayKey]);
            _orderTableParser.Parse(rawTables[_originSettings.OrderKey]);
            _customersParser.Parse(rawTables[_originSettings.CustomerKey]);
            _customerPoolsParser.Parse(rawTables[_originSettings.PoolKey]);
            _dialogueParser.Parse(rawTables[_originSettings.DialogueKey]);
            return null;
        }
        
    }
}