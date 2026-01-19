namespace LibraryManagement.Common.DTOs
{
    public class FineDTO
    {
        public int FineId { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
    }
}
