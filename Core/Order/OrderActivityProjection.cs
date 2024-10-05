using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marten;
using Marten.Events;
using Marten.Events.Aggregation;
using Marten.Events.Projections;

namespace Core.Order
{
    public class OrderHistoryItem(string status, DateTime? occurredOn)
    {
        public string OrderStatus { get; set; } = status;
        public DateTime? OccurredOn { get; set; } = occurredOn;
    }
    public class OrderActivity
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public List<OrderHistoryItem> OrderHistory { get; set; } = [];
        //public Dictionary<string, DateTime?> OrderStatusChangedOn { get; set; } = [];

    }
    public class OrderActivityProjection : SingleStreamProjection<OrderActivity>
    {
        //public OrderActivityProjection()
        //{
        //    Identity<OrderStarted>(x => x.OrderId);
        //    Identity<OrderCompleted>(x => x.OrderId);
        //    Identity<OrderCancelled>(x => x.OrderId);
        //    Identity<OrderReturned>(x => x.OrderId);
        //}

        public OrderActivityProjection() { }

        public void Apply(OrderStarted @event, OrderActivity view)
        {
            view.CustomerId = @event.InitiateOrderDto.CustomerId;
            view.OrderHistory.Add(
                new(@event.InitiateOrderDto.OrderStatus.ToString(),
                @event.InitiateOrderDto.OrderDate));
        }

        public void Apply(OrderCompleted @event, OrderActivity view)
        {
            view.OrderHistory.Add(new(@event.OrderStatus.ToString(), null));
        }

        public void Apply(OrderCancelled @event, OrderActivity view)
        {
            view.OrderHistory.Add(new(@event.OrderStatus.ToString(), null));
        }

        public void Apply(OrderReturned @event, OrderActivity view)
        {
            view.OrderHistory.Add(new(@event.OrderStatus.ToString(), null));
        }
    }
}
