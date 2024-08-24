using Application.DomainEvents;
using Core.BookInventory;
using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class BookCreationRequestedHandler : INotificationHandler<BookCreationRequested>
    {
        
        public BookCreationRequestedHandler() 
        {
            
        }

        public async Task Handle(BookCreationRequested notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
