#nullable enable
using System.Collections.Generic;
using System.Linq;
using CodeBase.Editor.OriginGameConfig.TableParsers.Tables;
using CodeBase.Runtime._CustomersProvider.Pool;
using CodeBase.Runtime.Configuration;
using CodeBase.Runtime.Core;
using CodeBase.Runtime.Core._Customer;
using CodeBase.Runtime.Infrastructure;
using CodeBase.Runtime.Infrastructure.Collections;
using UnityEngine;

namespace CodeBase.Editor.OriginGameConfig
{
    public sealed class GameConfigMapper
    {
        private readonly MapperDataContainer _container;

        public GameConfigMapper(MapperDataContainer container)
            => _container = container;

        public GameConfiguration TranslateToConfigFormat()
        {
            var configuration = ScriptableObject.CreateInstance<GameConfiguration>();

            configuration.Constructor(
                CastWithoutNullState(_container.General.DaysAmount),
                CastWithoutNullState(_container.General.DayDuration),
                MapPools(),
                FindAllSimpleCustomers(),
                FindAllStoryLines(),
                FindOrdersWithoutOwner());

            return configuration;
        }

        private IEnumerable<CustomersPool> MapPools()
            => _container.Pools.Select(p =>
                new CustomersPool(CastWithoutNullState(p.LevelRange), p.CustomersId));

        private IEnumerable<Customer> FindAllSimpleCustomers()
            => MapCustomers().Where(c => c is not PlotCustomer);

        private SyncDictionary<int, PlotCustomer> FindAllStoryLines()
        {
            Dictionary<IEnumerable<int>, PlotCustomer> plotCustomers = MapCustomers()
                .OfType<PlotCustomer>()
                .ToDictionary(c => GetDaysNumbersByPlotId(c.Id), c => c);

            Dictionary<int, PlotCustomer> storyLine = new();
            plotCustomers.ToList()
                .ForEach(p => p.Key.ToList().ForEach(d => storyLine[d] = p.Value));
            
            return storyLine.OrderBy(p => p.Key).ToSyncDictionary(p => p.Key, p => p.Value);
        }

        private IEnumerable<Customer> MapCustomers()
            => _container.Customers
                .Select(c =>
                {
                    var ownedOrders = FindOrdersByCustomerId(c.Id!).ToArray();
                    var isPlot = ownedOrders.Length > 0;
                    return isPlot is false ? new Customer(c.Id, c.Name) : new PlotCustomer(c.Id, c.Name, ownedOrders);
                });

        private IEnumerable<CustomerOrder> FindOrdersWithoutOwner()
            => _container.Orders
                .Where(o => o.CustomerId is null)
                .Select(o => new CustomerOrder(CastWithoutNullState(o.Reward),
                    o.RequestedItem,
                    FindDialogueById(o.DialogueId!)));

        private IEnumerable<CustomerOrder> FindOrdersByCustomerId(string id)
            => _container.Orders
                .Where(o => o.CustomerId == id)
                .OrderBy(o => o.Chapter)
                .Select(o => new CustomerOrder(CastWithoutNullState(o.Reward),
                    o.RequestedItem,
                    FindDialogueById(o.DialogueId!)));

        private Dialogue FindDialogueById(string id)
            => _container.Dialogues
                .Where(d => d.Id == id)
                .Select(d => new Dialogue(d.Speak, d.PromptSpeak))
                .First();

        private IEnumerable<int> GetDaysNumbersByPlotId(string id)
            => _container.Days
                .Where(d => d.StoryCustomersId.Contains(id))
                .Select(d => CastWithoutNullState(d.LevelId));

        private T CastWithoutNullState<T>(T? target) where T : struct
            => (T)target;

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