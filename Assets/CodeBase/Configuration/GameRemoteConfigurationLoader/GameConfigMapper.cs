#nullable enable
using System.Collections.Generic;
using System.Linq;
using CodeBase.Configuration.Data.MainConfig;
using CodeBase.Configuration.GameRemoteConfigurationLoader.TableParsers.Tables;
using CodeBase.Customers;
using CodeBase.Customers.CustomersPool;
using CodeBase.Customers.Data;
using CodeBase.Customers.Order;
using UnityEngine;

namespace CodeBase.Configuration.GameRemoteConfigurationLoader
{
    public sealed class GameConfigMapper
    {
        private readonly MapperDataContainer _container;

        public GameConfigMapper(MapperDataContainer container)
            => _container = container;

        public GameConfiguration TranslateToConfigFormat()
        {
            var configuration = ScriptableObject.CreateInstance<GameConfiguration>();
            configuration.Constructor
            (
                CastWithoutNullState(_container.General.DaysAmount),
                CastWithoutNullState(_container.General.DayDuration),
                AllStoryLines(),
                CustomersPools(),
                AllSimpleCustomers(),
                AllOrdersWithoutOwner()
            );


            return configuration;
        }


        private IEnumerable<CustomersPool> CustomersPools()
            => _container.Pools.Select(p =>
                new CustomersPool(CastWithoutNullState(p.LevelRange), p.CustomersId));

        private IEnumerable<Customer> AllSimpleCustomers()
            => PlotCustomerMap()
                .Where(c => c.Value is false)
                .Select(c => c.Key);

        private IEnumerable<StoryLinePart> AllStoryLines()
            => _container.Days
                .Select(d => new StoryLinePart
                (
                    CastWithoutNullState(d.LevelId),
                    AllStoryOrders().First(o => o.Holder.Id == d.StoryCustomersId.First())
                ));

        private IEnumerable<CustomerOrderWithoutOwner> AllOrdersWithoutOwner()
            => _container.Orders
                .Where(o => o.CustomerId is null)
                .Select(o => new CustomerOrderWithoutOwner
                (
                    CastWithoutNullState(o.Reward),
                    o.RequestedItem,
                    FindDialogueById(o.DialogueId!)
                ));

        private IEnumerable<StoryCustomerOrder> AllStoryOrders()
            => _container.Orders
                .Where(o => o.CustomerId is not null)
                .Select(o => new StoryCustomerOrder
                (
                    FindCustomerById(o.CustomerId!),
                    CastWithoutNullState(o.Reward),
                    o.RequestedItem,
                    FindDialogueById(o.DialogueId!)
                ))
                .ToArray();

        private Customer FindCustomerById(string id)
            => _container.Customers
                .Where(c => c.Id == id)
                .Select(c => new Customer(c.Id, c.Name))
                .First();

        private Dialogue FindDialogueById(string id)
            => _container.Dialogues
                .Where(d => d.Id == id)
                .Select(d => new Dialogue(d.Speak, d.PromptSpeak))
                .First();
        
        private Dictionary<Customer, bool> PlotCustomerMap()
            => _container.Customers
                .ToDictionary
                (
                    c => new Customer(c.Id, c.Name),
                    c => AllStoryOrders().FirstOrDefault(o => o.Holder.Id == c.Id) != default
                );

        private T CastWithoutNullState<T>(T? target) where T : struct
            => (T)target!;


        public sealed class MapperDataContainer
        {
            public GeneralTableParser.GeneralSettings General = null!;
            public DayTableParser.Day[] Days = null!;
            public OrderTableParser.Order[] Orders = null!;
            public CustomersParser.Customer[] Customers = null!;
            public CustomerPoolsParser.Pool[] Pools = null!;
            public DialogueParser.Dialogue[] Dialogues = null!;
        }

    }
}