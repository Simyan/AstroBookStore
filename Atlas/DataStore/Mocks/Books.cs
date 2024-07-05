using Atlas.Models;

namespace Atlas.DataStore.Mocks
{
    public class Books
    {
        public static List<Book> listOfBooks = new List<Book>
        {
            new Book()
            {
                Id = 1,
                Author = "Cixn Liu",
                Title = "At Death's End",
                Price = 50,
                Rating = 9,
                Genre = "Sci-fi",
                IsFiction = true
            },
            new Book()
            {
                    Id = 2,
                Author = "Cixn Liu",
                Title = "Three Body Problem",
                Price = 50,
                Rating = 8.5,
                Genre = "Sci-fi",
                IsFiction = true
            },
            new Book()
            {
                    Id = 3,
                Author = "Dr. Fei Fei Li",
                Title = "The Worlds I See",
                Price = 50,
                Rating = 8.4,
                Genre = "Artificial Inteligence",
                IsFiction = false
            }
           };
        }
}
