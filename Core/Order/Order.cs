using Core.Customer;
using Shared.DTO;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Core.Order
{

    public sealed record OrderStarted(Guid OrderId, InitiateOrderDto InitiateOrderDto, OrderAction orderAction);
    public sealed record OrderCompleted(Guid OrderId, OrderStatus OrderStatus);
    public sealed record OrderCancelled(Guid OrderId, OrderStatus OrderStatus);
    public sealed record OrderReturned(Guid OrderId, OrderStatus OrderStatus);


    public class Order : IAggregateRoot
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; private set; }

        public IEnumerable<OrderItem> OrderItems { get; private set; }

        public DateTime OrderDate { get; private set; }

        public string OrderNumber { get; private set; }

        public Address OrderAddress { get; private set; }

        public OrderStatus OrderStatus { get; private set; }

        public IEnumerable<OrderStage> OrderStages { get; private set; }

        public Order(Guid id,
            InitiateOrderDto initiateOrderDto,
            OrderStage orderStage)
        {
            Id = id;
            CustomerId = initiateOrderDto.CustomerId;
            OrderItems = initiateOrderDto.OrderItems.Select(o => new OrderItem(o));
            OrderNumber = initiateOrderDto.OrderNumber;
            OrderStatus = initiateOrderDto.OrderStatus;
            OrderDate = initiateOrderDto.OrderDate;
            OrderAddress = new Address(initiateOrderDto.AddressDto);
            OrderStages = new List<OrderStage>() { orderStage };
        }

        //[InititateOrder]
        //#Decide:
        //Called by : InititateOrder CH
        //Check if paymentRefNo already exists, ignore if it does
        //Validate fields according to requirement
        //Raise orderStarted event

        //#Create -> orderStarted : create new order object

        //[OrderCompleted]
        //#Decide:
        //Called by : CompleteOrder CH
        //Load order aggregate from ES 
        //Check if it is in valid stage to be completed
        //Raise orderCompleted event

        //#Apply -> orderCompleted : Update order state accordingly

        #region Evolve Methods

        public static Order Create(OrderStarted started)
            => new Order(started.OrderId, started.InitiateOrderDto, new OrderStage(started.orderAction));
        private void Apply(OrderCompleted completed, Order order)
        {
            order.OrderStatus = completed.OrderStatus;
            CurrentStageClosed(order);
        }

        private void Apply(OrderCancelled cancelled, Order order)
        {
            order.OrderStatus = cancelled.OrderStatus;
            CurrentStageClosed(order);
        }

        private void Apply(OrderReturned returned, Order order)
        {
            order.OrderStatus = returned.OrderStatus;
            CurrentStageClosed(order);
        }

        #endregion

        #region Decider Methods
        public static OrderStarted InitiateOrder(InitiateOrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto.OrderItems, nameof(dto.OrderItems));
            ArgumentOutOfRangeException.ThrowIfLessThan(dto.OrderItems.Count(), 1);
            dto.OrderItems.ToList()
                .ForEach(t => ArgumentOutOfRangeException.ThrowIfLessThan<uint>(t.Quantity, 1));

            return new OrderStarted(Guid.NewGuid(), dto, OrderAction.Process);
        }

        public static OrderCompleted CompleteOrder(Guid orderId) => new OrderCompleted(orderId, OrderStatus.Completed);
        public static OrderCancelled CancelOrder(Guid orderId) => new OrderCancelled(orderId, OrderStatus.Cancelled);
        public static OrderReturned ReturnOrder(Guid orderId) => new OrderReturned(orderId, OrderStatus.Returned);

        #endregion

       
        public static void CurrentStageClosed(Order order)
        {
            var stage = order.OrderStages.Where(w => w.EndDate == null).Single();
            stage.OrderCompleted();
        }
    }


}
