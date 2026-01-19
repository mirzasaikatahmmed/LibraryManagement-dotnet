using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Common.DTOs
{
    public class AuthorDTO
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    public class CreateAuthorDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Bio { get; set; } = string.Empty;

        [StringLength(50)]
        public string Country { get; set; } = string.Empty;
    }

    public class UpdateAuthorDTO
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(1000)]
        public string? Bio { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }
    }
}
