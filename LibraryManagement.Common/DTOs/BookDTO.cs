using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Common.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
    }

    public class CreateBookDTO
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Range(1000, 9999)]
        public int PublicationYear { get; set; }

        [Range(1, int.MaxValue)]
        public int TotalCopies { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }

    public class UpdateBookDTO
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(20)]
        public string? ISBN { get; set; }

        [Range(1000, 9999)]
        public int? PublicationYear { get; set; }

        [Range(1, int.MaxValue)]
        public int? TotalCopies { get; set; }

        [StringLength(100)]
        public string? Publisher { get; set; }

        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
    }
}
