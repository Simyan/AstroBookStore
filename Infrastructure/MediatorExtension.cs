using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class MediatorExtension
    {
        public static async Task ExecuteDomainEvents(this IMediator mediator, AstroDBContext dbcontext) 
        {
            var DomainEntities = dbcontext.ChangeTracker
                .Entries<DomainEntity>()
                .Where(w => w.Entity.DomainEvents != null && w.Entity.DomainEvents.Any());

            var DomainEvents = DomainEntities
                .SelectMany(s => s.Entity.DomainEvents)
                .ToList();

            DomainEntities
                .ToList()
                .ForEach(e => e.Entity.ClearDomainEvents());

            foreach (var domainEvent in DomainEvents)
                await mediator.Publish(domainEvent);

        }
    }
}
