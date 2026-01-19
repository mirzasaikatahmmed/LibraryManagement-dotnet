using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Common.DTOs
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
    }

    public class CreateLoanDTO
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int MemberId { get; set; }

        public int LoanDurationDays { get; set; } = 14;
    }

    public class ReturnBookDTO
    {
        [Required]
        public int LoanId { get; set; }
    }
}
