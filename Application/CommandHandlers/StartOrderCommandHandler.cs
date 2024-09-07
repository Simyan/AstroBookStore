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
    public class StartOrderCommandHandler(IDocumentStore documentStore) 
        : IRequestHandler<StartOrderCommand, bool>
    {
        private readonly IDocumentStore eventStore = documentStore;

        public async Task<bool> Handle(StartOrderCommand request, CancellationToken cancellationToken)
        {
            await using var session = eventStore.LightweightSession();

            var orderStartedEvent = Order.InitiateOrder(request.InitiateOrderDto);

            session.Events.StartStream(orderStartedEvent.OrderId, orderStartedEvent);
            
            await session.SaveChangesAsync();

            return true;    
        }
    }
}
