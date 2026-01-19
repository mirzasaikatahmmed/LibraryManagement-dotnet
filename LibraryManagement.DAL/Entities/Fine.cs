namespace LibraryManagement.DAL.Entities
{
    public class Fine
    {
        public int FineId { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public int LoanId { get; set; }
        public int MemberId { get; set; }

        public Loan Loan { get; set; } = null!;
        public Member Member { get; set; } = null!;
    }
}
