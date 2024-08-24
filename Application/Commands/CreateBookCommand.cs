using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateBookCommand : IRequest<bool>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public IEnumerable<string> Authors { get; private set; }
        public IEnumerable<string> Categories { get; private set; }
    }
}
