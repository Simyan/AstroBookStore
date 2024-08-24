using Application.Commands;
using Core;
using Core.BookInventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        IRepository<Book> _bookRepository;
        
        public CreateBookCommandHandler(IRepository<Book> BookRepository) 
        {
            _bookRepository = BookRepository;
        }
        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Book book = new(
               request.Title,
               request.Description,
               request.Price,
               request.Stock,
               request.Authors,
               request.Categories);

            _bookRepository.Add(book);
            
            return await _bookRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
