#nullable enable
using System.Linq;
using CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers.ParserTemplate;

namespace CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers.Tables
{
    public sealed class DayTableParser : TableParser<DayTableParser.Day, DayTableParser.DayTableTemplate>
    {
        protected override void FillEntity(string field, Day day, string key)
        {
            switch (key)
            {
                case DayTableTemplate.DayNumber:
                    day.LevelId = int.Parse(field);
                    break;
                case DayTableTemplate.StoryCustomersId:
                    day.StoryCustomersId = ParseToIdArray(field).ToArray();
                    break;
            }
        }

        protected override bool IsEntityFilled(Day entity)
            => entity.LevelId is not null;
        
        public sealed class DayTableTemplate : TableTemplate
        {
            public const string DayNumber = "day_number";
            public const string StoryCustomersId = "story_customers_id";

            public DayTableTemplate()
                => IdKey = DayNumber;

            public override bool HasBeenDetected => Keys.ContainsKey(DayNumber) &&
                                                    Keys.ContainsKey(StoryCustomersId);

            public override bool ThisReadKey(string comparedField) =>
                comparedField is DayNumber or StoryCustomersId;
        }

        public sealed class Day
        {
            public int? LevelId;
            public string[] StoryCustomersId = Enumerable.Empty<string>().ToArray();
        }
    }
}