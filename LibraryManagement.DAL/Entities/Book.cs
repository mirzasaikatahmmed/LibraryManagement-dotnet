namespace LibraryManagement.DAL.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        public Category Category { get; set; } = null!;
        public Author Author { get; set; } = null!;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
