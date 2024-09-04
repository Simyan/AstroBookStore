using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        T Add(T entity);
        T Update(T entity);
        Task<T?> FindByIdAsync(Guid Id);
    }
}
