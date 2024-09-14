using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class ReturnOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; private set; }
        public ReturnOrderCommand(Guid orderId) => OrderId = orderId;
    }
}
