using Core;
using Core.BookInventory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BookRepository : IRepository<Book>
    {
        private readonly AstroDBContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public BookRepository(AstroDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Book Add(Book entity)
        {
            return _context.Add(entity).Entity;
        }

        public async Task<Book?> FindByIdAsync(Guid Id)
        {
            return await _context.Books
                .Where(b => b.Id == Id)
                .SingleOrDefaultAsync();
        }

        public Book Update(Book entity)
        {
            return _context.Update(entity).Entity;
        }
    }
}
