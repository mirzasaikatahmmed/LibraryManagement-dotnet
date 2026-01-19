using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("overview")]
        public IActionResult GetLibraryStatistics()
        {
            var stats = _statisticsService.GetLibraryStatistics();
            return Ok(stats);
        }

        [HttpGet("most-borrowed")]
        public IActionResult GetMostBorrowedBooks([FromQuery] int topCount = 10)
        {
            var books = _statisticsService.GetMostBorrowedBooks(topCount);
            return Ok(books);
        }

        [HttpGet("member-statistics")]
        public IActionResult GetMemberBorrowingStatistics()
        {
            var stats = _statisticsService.GetMemberBorrowingStatistics();
            return Ok(stats);
        }

        [HttpGet("overdue-report")]
        public IActionResult GetOverdueReport()
        {
            var report = _statisticsService.GetOverdueReport();
            return Ok(report);
        }
    }
}
