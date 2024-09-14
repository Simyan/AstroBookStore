using Application.Commands;
using Core.Order;
using Marten;
using Marten.Events;
using MediatR;

namespace Application.CommandHandlers
{
    public class ReturnOrderCommandHandler(IDocumentStore eventStore)
        : IRequestHandler<ReturnOrderCommand, bool>
    {
        private readonly IDocumentStore _eventStore = eventStore;

        public async Task<bool> Handle(ReturnOrderCommand request, CancellationToken cancellationToken)
        {
            using var session = _eventStore.LightweightSession();

            var stream = await session.Events.FetchForWriting<Order>(request.OrderId)
               ?? throw new NullReferenceException();
            var order = stream.Aggregate;

            var orderReturnedEvent = Order.ReturnOrder(request.OrderId);

            stream.AppendOne(orderReturnedEvent);

            await session.SaveChangesAsync();

            return true;
        }
    }
}
