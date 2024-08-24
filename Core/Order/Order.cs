using Core.Customer;
using Shared.DTO;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Core.Order
{
    


    public class Order : DomainEntity, IAggregateRoot
    {
        
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



        public Order InitiateOrder(InitiateOrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto.OrderItems, nameof(dto.OrderItems));
            ArgumentOutOfRangeException.ThrowIfLessThan(dto.OrderItems.Count(), 1);
            dto.OrderItems.ToList()
                .ForEach(t => ArgumentOutOfRangeException.ThrowIfLessThan<uint>(t.Quantity, 1));

            var processOrderStage = new OrderStage(OrderAction.Process);
            return new Order(new Guid(), dto, processOrderStage);
        }

        public void CompleteOrder() 
        {
            this.OrderStatus = OrderStatus.Completed;
            CurrentStageClosed();
        }

        public void CancelOrder()
        {
            CurrentStageClosed();
            OrderStage stage = new(OrderAction.Cancel);
            this.OrderStages.Append(stage);
        }

       
        public void ReturnOrder()
        {
            CurrentStageClosed();
            OrderStage stage = new(OrderAction.Return);
            this.OrderStages.Append(stage);
        }

        public void CurrentStageClosed()
        {
            var stage = this.OrderStages.Where(w => w.EndDate == null).Single();
            stage.OrderCompleted();
        }
    }


}
