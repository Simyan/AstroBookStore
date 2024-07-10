using Core.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.DTO
{
    public class InitiateOrderDto
    {
        public Guid CustomerId { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public AddressDto AddressDto { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
