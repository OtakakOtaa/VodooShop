using CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate;
using JetBrains.Annotations;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.Tables
{
    public sealed class OrderTableParser : TableParser<OrderTableParser.OrderInfo, OrderTableParser.OrderTableTemplate>
    {
        protected override void FillEntity(string field, OrderInfo order, string key)
        {
            switch (key)
            {
                case OrderTableTemplate.Id:
                    order.Id = field;
                    break;
                case OrderTableTemplate.RequestedItem:
                    order.RequestedItem = field; 
                    break;
                case OrderTableTemplate.Reward:
                    order.Reward = int.Parse(field); 
                    break;
                case OrderTableTemplate.DialogueId:
                    order.DialogueId = field;
                    break;
                case OrderTableTemplate.Chapter:
                    order.Chapter = int.Parse(field);
                    break;
            }
        }

        protected override bool IsEntityFilled(OrderInfo entity) =>
            entity.Id is not null &&
            entity.RequestedItem is not null &&
            entity.Reward is not null;
        
        public sealed class OrderTableTemplate : TableTemplate
        {
            public const string Id = "id";
            public const string RequestedItem = "requested_item";
            public const string Reward = "reward";
            public const string DialogueId = "dialogue_id";
            public const string Chapter = "chapter";
            
            public OrderTableTemplate() => IdKey = Id;

            public override bool HasBeenDetected => Keys.ContainsKey(Id) &&
                                                    Keys.ContainsKey(RequestedItem) &&
                                                    Keys.ContainsKey(Reward) &&
                                                    Keys.ContainsKey(DialogueId) &&
                                                    Keys.ContainsKey(Chapter);
            public override bool ThisReadKey(string comparedField) 
                => comparedField is Id or RequestedItem or Reward or DialogueId or Chapter;
        }
        
        public sealed class OrderInfo
        {
            [CanBeNull] public string Id;
            [CanBeNull] public string RequestedItem;
            [CanBeNull] public int? Reward;
            [CanBeNull] public string DialogueId;
            [CanBeNull] public int? Chapter;
        }
    }
}