using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Order
{
    public class OrderItem : IEntity
    {
        public Guid Id { get; private set; }
        public Guid BookInventoryId {  get; private set; }
        public uint Quantity { get; private set; }

        private OrderItem() { }
        public OrderItem(OrderItemDto dto)
        {
            Id = dto.Id;
            BookInventoryId = dto.BookInventoryId;
            Quantity = dto.Quantity;
        }


    }
}
