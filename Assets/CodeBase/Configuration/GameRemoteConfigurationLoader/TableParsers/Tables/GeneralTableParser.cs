using System;
using CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers.ParserTemplate;

namespace CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers.Tables
{
    public sealed class GeneralTableParser 
        : TableParser<GeneralTableParser.GeneralSettings, GeneralTableParser.GeneralTableTemplate>
    {
        protected override void FillEntity(string field, GeneralSettings entity, string key)
        {
            switch (key)
            {
                case GeneralTableTemplate.DaysAmount:
                    entity.DaysAmount = int.Parse(field);
                    break;
                case GeneralTableTemplate.DayDuration:
                    entity.DayDuration = ParseToTime(field);
                    break;
            }
        }

        protected override bool IsEntityFilled(GeneralSettings entity)
            => entity.DaysAmount is not null && entity.DayDuration is not null;

        public sealed class GeneralTableTemplate : TableTemplate
        {
            public const string DaysAmount = "days_amount";
            public const string DayDuration = "day_duration";
            public GeneralTableTemplate() 
                => IdKey = DaysAmount;

            public override bool HasBeenDetected => Keys.ContainsKey(DayDuration) && Keys.ContainsKey(DaysAmount);

            public override bool ThisReadKey(string comparedField) =>
                comparedField is DaysAmount or DayDuration;
        }

        public sealed class GeneralSettings
        {
            public int? DaysAmount;
            public TimeSpan? DayDuration;
        }
    }
}