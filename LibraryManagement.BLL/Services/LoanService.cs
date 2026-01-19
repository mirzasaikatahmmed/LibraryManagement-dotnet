using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Entities;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.BLL.Services
{
    public class LoanService
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private const decimal DAILY_FINE_AMOUNT = 5.00m;

        public LoanService(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<LoanDTO> GetAllLoans()
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToList();
            return _mapper.Map<List<LoanDTO>>(loans);
        }

        public LoanDTO? GetLoanById(int id)
        {
            var loan = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefault(l => l.LoanId == id);
            return loan == null ? null : _mapper.Map<LoanDTO>(loan);
        }

        public LoanDTO? BorrowBook(CreateLoanDTO dto)
        {
            var book = _context.Books.Find(dto.BookId);
            if (book == null || book.AvailableCopies <= 0)
            {
                return null;
            }

            var loan = new Loan
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                LoanDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(dto.LoanDurationDays),
                Status = "Borrowed"
            };

            book.AvailableCopies--;

            _context.Loans.Add(loan);
            _context.SaveChanges();

            return GetLoanById(loan.LoanId);
        }

        public LoanDTO? ReturnBook(int loanId)
        {
            var loan = _context.Loans
                .Include(l => l.Book)
                .FirstOrDefault(l => l.LoanId == loanId);

            if (loan == null || loan.ReturnDate != null)
            {
                return null;
            }

            loan.ReturnDate = DateTime.UtcNow;
            loan.Status = "Returned";

            loan.Book.AvailableCopies++;

            if (DateTime.UtcNow > loan.DueDate)
            {
                var daysLate = (DateTime.UtcNow - loan.DueDate).Days;
                var fineAmount = daysLate * DAILY_FINE_AMOUNT;

                var fine = new Fine
                {
                    LoanId = loan.LoanId,
                    MemberId = loan.MemberId,
                    Amount = fineAmount,
                    IssueDate = DateTime.UtcNow,
                    IsPaid = false,
                    Reason = $"Late return by {daysLate} days"
                };

                _context.Fines.Add(fine);
                loan.Status = "Returned Late";
            }

            _context.SaveChanges();
            return GetLoanById(loan.LoanId);
        }

        public List<LoanDTO> GetOverdueLoans()
        {
            var overdueLoans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.ReturnDate == null && l.DueDate < DateTime.UtcNow)
                .ToList();

            foreach (var loan in overdueLoans)
            {
                loan.Status = "Overdue";
            }
            _context.SaveChanges();

            return _mapper.Map<List<LoanDTO>>(overdueLoans);
        }

        public int ProcessOverdueFines()
        {
            var overdueLoans = _context.Loans
                .Where(l => l.ReturnDate == null && l.DueDate < DateTime.UtcNow)
                .ToList();

            int finesCreated = 0;

            foreach (var loan in overdueLoans)
            {
                var existingFine = _context.Fines.FirstOrDefault(f => f.LoanId == loan.LoanId && !f.IsPaid);

                var daysLate = (DateTime.UtcNow - loan.DueDate).Days;
                var fineAmount = daysLate * DAILY_FINE_AMOUNT;

                if (existingFine != null)
                {
                    existingFine.Amount = fineAmount;
                    existingFine.Reason = $"Late return by {daysLate} days";
                }
                else
                {
                    var fine = new Fine
                    {
                        LoanId = loan.LoanId,
                        MemberId = loan.MemberId,
                        Amount = fineAmount,
                        IssueDate = DateTime.UtcNow,
                        IsPaid = false,
                        Reason = $"Late return by {daysLate} days"
                    };
                    _context.Fines.Add(fine);
                    finesCreated++;
                }

                loan.Status = "Overdue";
            }

            _context.SaveChanges();
            return finesCreated;
        }
    }
}
