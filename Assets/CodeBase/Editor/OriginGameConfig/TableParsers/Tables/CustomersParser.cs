using CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate;
using JetBrains.Annotations;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.Tables
{
    public class CustomersParser : TableParser<CustomersParser.CustomerInfo, CustomersParser.CustomersTableTemplate>
    {
        protected override void FillEntity(string field, CustomerInfo entity, string key)
        {
            switch (key)
            {
                case CustomersTableTemplate.Id:
                    entity.Id = field;
                    break;
                case CustomersTableTemplate.Name:
                    entity.Name = field;
                    break;
            }
        }

        protected override bool IsEntityFilled(CustomerInfo entity)
            => entity.Id is not null && entity.Name is not null; 

        public sealed class CustomersTableTemplate : TableTemplate
        {
            public const string Id = "id";
            public const string Name = "name";

            public CustomersTableTemplate()
                => IdKey = Id;

            public override bool HasBeenDetected 
                => Keys.ContainsKey(Id) && Keys.ContainsKey(Name);
            public override bool ThisReadKey(string comparedField)
                => comparedField is Id or Name;
        }
        
        public sealed class CustomerInfo
        {
            [CanBeNull] public string Id;
            [CanBeNull] public string Name;
        }
    }
}