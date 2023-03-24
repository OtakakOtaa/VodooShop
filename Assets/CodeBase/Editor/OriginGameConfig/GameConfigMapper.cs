using System;
using System.Linq;
using CodeBase.Editor.OriginGameConfig.TableParsers.Tables;
using CodeBase.Runtime;
using UnityEngine;

namespace CodeBase.Editor.OriginGameConfig
{
    public sealed class GameConfigMapper
    {
        public GameConfiguration TranslateToConfigFormat(MapperDataContainer container)
        {
            var configuration = ScriptableObject.CreateInstance<GameConfiguration>();

            configuration.Constructor(container.Days.Select(MapDay).ToArray());
            
            return configuration;
        }

        private ShopDaySettings MapDay(DayTableParser.Day rawDay)
        {
            if (rawDay.LevelId is null || rawDay.LevelTime is null) 
                throw new Exception(nameof(MapDay));
            return new ShopDaySettings((int)rawDay.LevelId, (TimeSpan)rawDay.LevelTime);
        }

        public sealed class MapperDataContainer
        {
            public DayTableParser.Day[] Days;
            public OrderTableParser.Order[] Orders;
            public CustomersParser.Customer[] Customers;
            public CustomerPoolsParser.Pool[] Pools;
            public DialogueParser.Dialogue[] Dialogues;
        } 
    }
}