using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public DateTime ReleaseDate { get; private set; }
        public IEnumerable<string> Authors { get; private set; }
        public IEnumerable<string> Categories { get; private set; }


        public CreateBookCommand(string title, string description, decimal price, int stock, 
            IEnumerable<string> authors, IEnumerable<string> categories, DateTime releaseDate)
        {
            Title = title;
            Description = description;
            Price = price;
            Stock = stock;
            Authors = authors; 
            Categories = categories;
            ReleaseDate = releaseDate;
        }
    }
}
