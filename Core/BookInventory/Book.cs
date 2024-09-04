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
        public List<Author> Authors { get; private set; }
        public List<Genre> Genres { get; private set;  }

        //EF Core needs a constructor without the owned entities.
        private Book(){}

        public Book(string title, string description, decimal price, int stock, IEnumerable<string> authors, IEnumerable<string> genres, DateTime releaseDate)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(title);
            ArgumentNullException.ThrowIfNull(authors);
            ArgumentNullException.ThrowIfNull(genres);

            if (price <= 0) new ArgumentException();
            if (stock < 0) new ArgumentException();

            Id = Guid.NewGuid();
            Title = title;
            Description = description ?? string.Empty;
            Authors = authors.Select(name => new Author(name, Guid.NewGuid())).ToList();
            Genres = genres.Select(x => new Genre(x)).ToList();
            Price = price;
            Stock = stock;
            ReleaseDate = releaseDate;

            this.AddDomainEvent(new BookCreationRequested());
        }

            
        

    }
}
