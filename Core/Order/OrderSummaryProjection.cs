using Marten;
using Marten.Events;
using Marten.Events.Aggregation;
using Marten.Events.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Weasel.Postgresql.Views;

namespace Core.Order
{
    public class OrderSummary
    {
        public Guid Id { get; set; }
        public int OrdersFulfilledCount { get; set; }
        public int OrdersCancelledCount { get; set; }
        public int OrdersReturnedCount { get; set; }
    }

    public class OrderSummaryProjection : MultiStreamProjection<OrderSummary, Guid>
    {
        public OrderSummaryProjection()
        {
            
            //Identity<OrderCompleted>(x => x.OrderId);
            //Identity<OrderCancelled>(x => x.OrderId);
            //Identity<OrderReturned>(x => x.OrderId);
            CustomGrouping(new OrderSummayGrouper());
        }



        public void Apply(OrderCompleted orderCompletedEvent, OrderSummary view)
            => view.OrdersFulfilledCount++;

        public void Apply(OrderCancelled orderCancelledEvent, OrderSummary view)
            => view.OrdersCancelledCount++;

        public void Apply(OrderReturned orderReturnedEvent, OrderSummary view)
             => view.OrdersReturnedCount++;

    }

    public class OrderSummayGrouper : IAggregateGrouper<Guid>
    {

        private readonly Type[] eventTypes =
        [
            typeof(OrderCompleted), 
            typeof(OrderCancelled),
            typeof(OrderReturned)
        ];
        public async Task Group(IQuerySession session, IEnumerable<IEvent> events, ITenantSliceGroup<Guid> grouping)
        {
            //var filteredEvents = events
            //    .OfType<IEvent<OrderCompleted>>()
            //    .OfType<IEvent<OrderCancelled>>()
            //    .OfType<IEvent<OrderReturned>>()
            //    .ToList();

            var filteredEvents = events
                .Where(ev => eventTypes.Contains(ev.EventType))
                .ToList();

            if (!filteredEvents.Any())
            {
                return; 
            }

            var orderIds = filteredEvents.Select(e => e.StreamId).ToList();

            var y = await session.Events.QueryRawEventDataOnly<OrderStarted>()
                .Where(e => orderIds.Contains(e.OrderId))
                .Select(s => new { Id = s.OrderId })
                .ToListAsync();

            grouping.AddEvents(Guid.NewGuid(), filteredEvents);
            //foreach(var group in y.Select(g =>
            //    new { g.Id, Events = filteredEvents.Where(ev => ev.StreamId == g.Id) }))
            //{
            //    grouping.AddEvents(Guid.NewGuid(), group.Events);
            //}
        }
    }
}
