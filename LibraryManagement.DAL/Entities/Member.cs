namespace LibraryManagement.DAL.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime MembershipDate { get; set; }
        public string MembershipType { get; set; } = string.Empty;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<Fine> Fines { get; set; } = new List<Fine>();
    }
}
