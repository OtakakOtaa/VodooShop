using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate
{
    public abstract class TableParser<TEntity, TTable> where TTable : TableTemplate, new() where TEntity : new()
    {
        private const string EmptyFlag = "";
        private const char TimeSeparator = ':';
        private const char EnumerableSeparator = ';';
        private const char Space = ' ';
        private const char RangeSeparator = '-';

        public IEnumerable<TEntity> Parse(string rawData)
        {
            TableTemplate table = new TTable();
            CsvReader reader = new(rawData);
            List<TEntity> tableEntities = new();

            while (reader.TryRead())
            {
                TEntity entity = new();
                for (var i = 0; reader.TryGetField(i, out var field); i++)
                {
                    if (table.HasBeenDetected is false && field is not EmptyFlag)
                    {
                        DetectReadKeyPosition(field, i, table);
                        continue;
                    }

                    if (table.TryGetKey(i, out var key) is false) continue;
                    if (TableEnded(field, key, table)) return tableEntities;
                    if (field is EmptyFlag) continue;

                    FillEntity(field, entity, key);
                }

                if (IsEntityFilled(entity)) tableEntities.Add(entity);
            }

            return tableEntities;
        }

        private bool TableEnded(string field, string key, TableTemplate table)
            => key == table.IdKey && field is EmptyFlag;

        private void DetectReadKeyPosition(string field, int index, TableTemplate table)
        {
            if (table.ThisReadKey(field)) table.BindKeyToColumn(field, index);
        }

        protected abstract void FillEntity(string field, TEntity entity, string key);
        protected abstract bool IsEntityFilled(TEntity entity);

        protected TimeSpan ParseToTime(string field)
        {
            var substring = field
                .Split(TimeSeparator)
                .Select(s => s.First() != '0' ? s : s.Last().ToString())
                .ToArray();
            return new TimeSpan(0, int.Parse(substring[0]), int.Parse(substring[1]));
        }

        protected IEnumerable<string> ParseToIdArray(string field)
            => field.Trim(Space).Split(EnumerableSeparator);

        protected Range ParseToRange(string field)
        {
            var rangeValues = field.Trim(Space).Split(RangeSeparator).Select(int.Parse).ToArray();
            return new Range(rangeValues.First(), rangeValues.Last());
        }
    }
}