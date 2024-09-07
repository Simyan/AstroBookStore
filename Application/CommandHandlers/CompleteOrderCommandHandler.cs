using Application.Commands;
using Core.Order;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CompleteOrderCommandHandler(IDocumentStore eventStore) 
        : IRequestHandler<CompleteOrderCommand, bool>
    {
        private readonly IDocumentStore _eventStore = eventStore; 

        public async Task<bool> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            using var session = _eventStore.LightweightSession();

            var order = await session.Events.AggregateStreamAsync<Order>(request.OrderId) 
                ?? throw new NullReferenceException();

            var orderCompletedEvent = order.CompleteOrder(request.OrderId);

            session.Events.Append(request.OrderId, orderCompletedEvent);

            await session.SaveChangesAsync();

            return true;

        }
    }
}
