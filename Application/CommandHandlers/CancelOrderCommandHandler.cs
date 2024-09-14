using Application.Commands;
using Core.Order;
using Marten;
using Marten.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CancelOrderCommandHandler(IDocumentStore eventStore)
        : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IDocumentStore _eventStore = eventStore;

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            using var session = _eventStore.LightweightSession();

            var stream = await session.Events.FetchForWriting<Order>(request.OrderId)
                ?? throw new NullReferenceException();
            var order = stream.Aggregate;

            var orderCancelledEvent = Order.CancelOrder(request.OrderId);

            stream.AppendOne(orderCancelledEvent);

            await session.SaveChangesAsync();

            return true;
        }
    }
}
