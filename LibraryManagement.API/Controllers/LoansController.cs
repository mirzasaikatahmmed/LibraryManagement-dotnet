using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var loans = _loanService.GetAllLoans();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var loan = _loanService.GetLoanById(id);
            if (loan == null)
                return NotFound();
            return Ok(loan);
        }

        [HttpPost("borrow")]
        public IActionResult BorrowBook([FromBody] CreateLoanDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loan = _loanService.BorrowBook(dto);
            if (loan == null)
                return BadRequest("Book not available");

            return CreatedAtAction(nameof(GetById), new { id = loan.LoanId }, loan);
        }

        [HttpPost("{loanId}/return")]
        public IActionResult ReturnBook(int loanId)
        {
            var loan = _loanService.ReturnBook(loanId);
            if (loan == null)
                return NotFound("Loan not found or already returned");

            return Ok(loan);
        }

        [HttpGet("overdue")]
        public IActionResult GetOverdueLoans()
        {
            var loans = _loanService.GetOverdueLoans();
            return Ok(loans);
        }

        [HttpPost("process-overdue-fines")]
        public IActionResult ProcessOverdueFines()
        {
            var count = _loanService.ProcessOverdueFines();
            return Ok(new { Message = $"Processed {count} overdue fines", Count = count });
        }
    }
}
