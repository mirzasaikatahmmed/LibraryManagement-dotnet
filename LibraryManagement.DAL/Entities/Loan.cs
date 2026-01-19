namespace LibraryManagement.DAL.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty; // Borrowed, Returned, Overdue

        public int BookId { get; set; }
        public int MemberId { get; set; }

        public Book Book { get; set; } = null!;
        public Member Member { get; set; } = null!;
        public ICollection<Fine> Fines { get; set; } = new List<Fine>();
    }
}
