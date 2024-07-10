
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid BookInventoryId { get; set; }
        public uint Quantity { get; set; }
    }
}
