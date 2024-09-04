using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BookInventory
{
    public interface IBookRepository : IRepository<Book>
    {
        Book Add(Book entity);
        Book Update(Book entity);
        Task<Book?> FindByIdAsync(Guid Id);
    }
}
