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
        public string Id { get; set; }
        public int OrdersFulfilledCount { get; set; }
        public int OrdersCancelledCount { get; set; }
        public int OrdersReturnedCount { get; set; }
        
        //public DateOnly OrderSummaryDate {  get; set; }
    }

    public class OrderSummaryProjection : MultiStreamProjection<OrderSummary, string>
    {
        public OrderSummaryProjection()
        {
            
            //Identity<OrderCompleted>(x => x.OrderId);
            //Identity<OrderCancelled>(x => x.OrderId);
            //Identity<OrderReturned>(x => x.OrderId);
            CustomGrouping(new OrderSummayGrouper());
        }



        public void Apply(IEvent<OrderCompleted> orderCompletedEvent, OrderSummary view)
        {
            
            view.OrdersFulfilledCount++;
        }

        public void Apply(OrderCancelled orderCancelledEvent, OrderSummary view)
            => view.OrdersCancelledCount++;

        public void Apply(OrderReturned orderReturnedEvent, OrderSummary view)
             => view.OrdersReturnedCount++;

    }

    public class OrderSummayGrouper : IAggregateGrouper<string>
    {

        private readonly Type[] eventTypes =
        [
            typeof(OrderCompleted), 
            typeof(OrderCancelled),
            typeof(OrderReturned)
        ];

        public async Task Group(IQuerySession session, IEnumerable<IEvent> events, ITenantSliceGroup<string> grouping)
        {

            var filteredEvents = events
                .Where(ev => eventTypes.Contains(ev.EventType))
                .ToList();

            if (!filteredEvents.Any())
            {
                return;
            }

            var orderIds = filteredEvents.Select(e => e.StreamId).ToList();

            // filter events that we want
            //var filteredPastEvents = await session.Events.QueryAllRawEvents()
            //    .Where(e => e.EventTypesAre(eventTypes))
            //    .ToListAsync();

            //var filteredEventsUnion = filteredEvents.Union(filteredPastEvents);

            var distinctDates = filteredEvents
                .DistinctBy(e => e.Timestamp.Date)
                .ToList();

            foreach (var x in distinctDates.Select(
                    g => new {
                        Id = DateOnly.FromDateTime(g.Timestamp.Date).ToString(),
                        Events = filteredEvents.Where(ev => ev.Timestamp.Date == g.Timestamp.Date)
                    }))
            {
                grouping.AddEvents(x.Id, x.Events);
            }
        }
    }
}
