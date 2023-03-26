using CodeBase.Editor.OriginGameConfig.TableParsers.Tables;
using CodeBase.Runtime.Configuration;
using UnityEngine;

namespace CodeBase.Editor.OriginGameConfig
{
    public sealed class GameConfigMapper
    {
        public GameConfiguration TranslateToConfigFormat(MapperDataContainer container)
        {
            var configuration = ScriptableObject.CreateInstance<GameConfiguration>();

            // configuration.Constructor();
              
            return configuration;
        }

        public sealed class MapperDataContainer
        {
            public GeneralTableParser.GeneralSettings General;
            public DayTableParser.Day[] Days;
            public OrderTableParser.Order[] Orders;
            public CustomersParser.Customer[] Customers;
            public CustomerPoolsParser.Pool[] Pools;
            public DialogueParser.Dialogue[] Dialogues;
        } 
    }
}