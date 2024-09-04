using Core;
using Core.BookInventory;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure
{
    

    public class AstroDBContext : DbContext, IUnitOfWork
    {
        readonly IMediator _mediator;
        public AstroDBContext(DbContextOptions<AstroDBContext> dbContextOptions, IMediator mediator) : base(dbContextOptions) 
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

       
        public DbSet<Book> Books { get; set; }
  
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
                modelBuilder.Entity<Book>()
                    .ToTable("Book")
                    .HasKey(e => e.UId);

                modelBuilder.Entity<Book>()
                    .Property(e => e.UId)
                    .UseIdentityColumn();

                modelBuilder.Entity<Book>()
                    .OwnsMany(
                    p => p.Authors,
                    o => o.ToJson()
                    );

                modelBuilder.Entity<Book>()
                    .OwnsMany(
                    p => p.Genres,
                    o => o.ToJson()
                    );

                //modelBuilder.Entity<Book>()
                //    .Property(e => e.Data)
                //    .HasColumnType("json");



                base.OnModelCreating(modelBuilder);  
        }

        public async Task<bool> SaveEntitiesAsync()
        {
            await _mediator.ExecuteDomainEvents(this);

            _ = await SaveChangesAsync();

            return true;
        }


    }
}
