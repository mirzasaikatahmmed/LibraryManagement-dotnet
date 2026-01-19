using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Data;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.BLL.Services
{
    public class StatisticsService
    {
        private readonly LibraryDbContext _context;

        public StatisticsService(LibraryDbContext context)
        {
            _context = context;
        }

        public LibraryStatisticsDTO GetLibraryStatistics()
        {
            var totalBooks = _context.Books.Count();
            var totalMembers = _context.Members.Count();
            var activeLoans = _context.Loans.Count(l => l.ReturnDate == null);
            var overdueLoans = _context.Loans.Count(l => l.ReturnDate == null && l.DueDate < DateTime.UtcNow);
            var totalFinesCollected = _context.Fines.Where(f => f.IsPaid).Sum(f => (decimal?)f.Amount) ?? 0;
            var pendingFines = _context.Fines.Where(f => !f.IsPaid).Sum(f => (decimal?)f.Amount) ?? 0;

            return new LibraryStatisticsDTO
            {
                TotalBooks = totalBooks,
                TotalMembers = totalMembers,
                ActiveLoans = activeLoans,
                OverdueLoans = overdueLoans,
                TotalFinesCollected = totalFinesCollected,
                PendingFines = pendingFines
            };
        }

        public List<MostBorrowedBookDTO> GetMostBorrowedBooks(int topCount = 10)
        {
            var mostBorrowed = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Loans)
                .Select(b => new MostBorrowedBookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author.Name,
                    BorrowCount = b.Loans.Count
                })
                .OrderByDescending(b => b.BorrowCount)
                .Take(topCount)
                .ToList();

            return mostBorrowed;
        }

        public List<MemberBorrowingHistoryDTO> GetMemberBorrowingStatistics()
        {
            var memberStats = _context.Members
                .Include(m => m.Loans)
                .Include(m => m.Fines)
                .Select(m => new MemberBorrowingHistoryDTO
                {
                    MemberId = m.MemberId,
                    MemberName = m.Name,
                    TotalBorrowedBooks = m.Loans.Count,
                    ActiveLoans = m.Loans.Count(l => l.ReturnDate == null),
                    TotalFines = m.Fines.Sum(f => (decimal?)f.Amount) ?? 0
                })
                .OrderByDescending(m => m.TotalBorrowedBooks)
                .ToList();

            return memberStats;
        }

        public List<LoanDTO> GetOverdueReport()
        {
            var overdueLoans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.ReturnDate == null && l.DueDate < DateTime.UtcNow)
                .Select(l => new LoanDTO
                {
                    LoanId = l.LoanId,
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate,
                    ReturnDate = l.ReturnDate,
                    Status = "Overdue",
                    BookTitle = l.Book.Title,
                    MemberName = l.Member.Name
                })
                .ToList();

            return overdueLoans;
        }
    }
}
