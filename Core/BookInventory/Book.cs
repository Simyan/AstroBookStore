using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BookInventory
{

 
    

    public class Book : IEntity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Title;
        public string Description;
        public DateTime ReleaseDate;
        public decimal Price;
        public decimal Stock;
        public IEnumerable<Author> Authors { get; }
        public IEnumerable<Category> Categories { get; }

        public Book(string title, string description, IEnumerable<string> authors, IEnumerable<string> categories)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(title);
            ArgumentNullException.ThrowIfNull(authors);
            ArgumentNullException.ThrowIfNull(categories);

            if (Price <= 0) new ArgumentException();
            if (Stock < 0) new ArgumentException();
            
            Id = new Guid();
            Title = title;
            Description = description ?? string.Empty;
            Authors = authors.Select(name => new Author(name, new Guid()));
            Categories = categories.Select(x => new Category(x));
        }


       

    }
}
