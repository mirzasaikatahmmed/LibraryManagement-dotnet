namespace LibraryManagement.Common.DTOs
{
    public class BookSearchDTO
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int? PublicationYear { get; set; }
        public bool? IsAvailable { get; set; }
    }

    public class LibraryStatisticsDTO
    {
        public int TotalBooks { get; set; }
        public int TotalMembers { get; set; }
        public int ActiveLoans { get; set; }
        public int OverdueLoans { get; set; }
        public decimal TotalFinesCollected { get; set; }
        public decimal PendingFines { get; set; }
    }

    public class MostBorrowedBookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int BorrowCount { get; set; }
    }

    public class MemberBorrowingHistoryDTO
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public int TotalBorrowedBooks { get; set; }
        public int ActiveLoans { get; set; }
        public decimal TotalFines { get; set; }
    }

    public class BookRecommendationDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
