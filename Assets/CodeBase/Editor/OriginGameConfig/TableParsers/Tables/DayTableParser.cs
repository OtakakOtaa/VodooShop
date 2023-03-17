#nullable enable
using System;
using System.Linq;
using CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.Tables
{
    public sealed class DayTableParser : TableParser<DayTableParser.DayInfo, DayTableParser.DayTableTemplate>
    {
        protected override void FillEntity(string field, DayInfo dayInfo, string key)
        {
            switch (key)
            {
                case DayTableTemplate.DayNumber:
                    dayInfo.LevelId = int.Parse(field);
                    break;
                case DayTableTemplate.CustomersAmount:
                    dayInfo.MaxCustomersAmount = int.Parse(field);
                    break;
                case DayTableTemplate.GameTime:
                    dayInfo.LevelTime = ParseToTime(field);
                    break;
                case DayTableTemplate.StoryCustomersId:
                    dayInfo.StoryCustomersId = ParseToIdArray(field).ToArray();
                    break;
                case DayTableTemplate.PoolsId:
                    dayInfo.PoolsId = ParseToIdArray(field).ToArray();
                    break;
            }
        }

        protected override bool IsEntityFilled(DayInfo entity)
            => entity.LevelId is not null &&
               entity.LevelTime is not null &&
               entity.MaxCustomersAmount is not null &&
               entity.PoolsId.Length != 0;


        public sealed class DayTableTemplate : TableTemplate
        {
            public const string DayNumber = "day_number";
            public const string CustomersAmount = "customers_amount";
            public const string GameTime = "game_time";
            public const string StoryCustomersId = "story_customers_id";
            public const string PoolsId = "pools_id";

            public DayTableTemplate()
                => IdKey = DayNumber;

            public override bool HasBeenDetected => Keys.ContainsKey(DayNumber) &&
                                                    Keys.ContainsKey(CustomersAmount) &&
                                                    Keys.ContainsKey(GameTime) &&
                                                    Keys.ContainsKey(StoryCustomersId) &&
                                                    Keys.ContainsKey(PoolsId);

            public override bool ThisReadKey(string comparedField) =>
                comparedField is DayNumber or CustomersAmount or GameTime or StoryCustomersId or PoolsId;
        }

        public sealed class DayInfo
        {
            public int? LevelId;
            public int? MaxCustomersAmount;
            public TimeSpan? LevelTime;
            public string[] StoryCustomersId = Enumerable.Empty<string>().ToArray();
            public string[] PoolsId = Enumerable.Empty<string>().ToArray();
        }
    }
}