using Application.DomainEvents;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Core.BookInventory
{




    public class Book : DomainEntity, IAggregateRoot
    {
        public long UId { get; private set;}
        //public Guid Id { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public IEnumerable<Author> Authors { get; private set; }
        public IEnumerable<Genre> Genres { get; private set;  }

        public Book(string title, string description, decimal price, int stock, IEnumerable<string> authors, IEnumerable<string> genres)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(title);
            ArgumentNullException.ThrowIfNull(authors);
            ArgumentNullException.ThrowIfNull(genres);

            if (price <= 0) new ArgumentException();
            if (stock < 0) new ArgumentException();
            
            Id = new Guid();
            Title = title;
            Description = description ?? string.Empty;
            Authors = authors.Select(name => new Author(name, new Guid()));
            Genres = genres.Select(x => new Genre(x));

            this.AddDomainEvent(new BookCreationRequested());
        }

            
        

    }
}
