using System;
using System.Linq;
using CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate;
using JetBrains.Annotations;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.Tables
{
    public sealed class CustomerPoolsParser : 
        TableParser<CustomerPoolsParser.Pool, CustomerPoolsParser.PoolsTableTemplate>
    {
        protected override void FillEntity(string field, Pool entity, string key)
        {
            switch (key)
            {
                case PoolsTableTemplate.Id:
                    entity.Id = field;
                    break;
                case PoolsTableTemplate.LevelRange:
                    entity.LevelRange = ParseToRange(field); 
                    break;
                case PoolsTableTemplate.CustomersId:
                    entity.CustomersId = ParseToIdArray(field).ToArray();
                    break;
            }
        }

        protected override bool IsEntityFilled(Pool entity)
            => entity.CustomersId.Length != 0 && entity.Id is not null && entity.LevelRange is not null; 

        public sealed class PoolsTableTemplate : TableTemplate
        {
            public const string Id = "id";
            public const string LevelRange = "lvl_range";
            public const string CustomersId = "customers_id";

            public PoolsTableTemplate()
                => IdKey = Id;

            public override bool HasBeenDetected =>
                Keys.ContainsKey(Id) && Keys.ContainsKey(LevelRange) && Keys.ContainsKey(CustomersId);

            public override bool ThisReadKey(string comparedField)
                => comparedField is Id or LevelRange or CustomersId;
        }

        public sealed class Pool
        {
            [CanBeNull] public string Id;
            [CanBeNull] public Range? LevelRange;
            public string[] CustomersId = Enumerable.Empty<string>().ToArray();
        }
    }
}