namespace Atlas.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string? Author { get; set; }
        public double Rating { get; set; }
        public string? Genre { get; set; }
        public bool IsFiction { get; set; }
    }
}
