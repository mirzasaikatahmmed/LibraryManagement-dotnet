using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Common.DTOs
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime MembershipDate { get; set; }
        public string MembershipType { get; set; } = string.Empty;
    }

    public class CreateMemberDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string MembershipType { get; set; } = string.Empty;
    }

    public class UpdateMemberDTO
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? MembershipType { get; set; }
    }
}
