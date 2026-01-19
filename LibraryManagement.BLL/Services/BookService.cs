using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Entities;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.BLL.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public BookService(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BookDTO> GetAllBooks()
        {
            var books = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .ToList();
            return _mapper.Map<List<BookDTO>>(books);
        }

        public BookDTO? GetBookById(int id)
        {
            var book = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookId == id);
            return book == null ? null : _mapper.Map<BookDTO>(book);
        }

        public BookDTO CreateBook(CreateBookDTO dto)
        {
            var book = _mapper.Map<Book>(dto);
            _context.Books.Add(book);
            _context.SaveChanges();

            return GetBookById(book.BookId)!;
        }

        public bool UpdateBook(int id, UpdateBookDTO dto)
        {
            var book = _context.Books.Find(id);
            if (book == null) return false;

            _mapper.Map(dto, book);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public List<BookDTO> SearchBooks(BookSearchDTO searchDto)
        {
            var query = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Title))
            {
                query = query.Where(b => b.Title.Contains(searchDto.Title));
            }

            if (!string.IsNullOrEmpty(searchDto.Author))
            {
                query = query.Where(b => b.Author.Name.Contains(searchDto.Author));
            }

            if (!string.IsNullOrEmpty(searchDto.Category))
            {
                query = query.Where(b => b.Category.Name.Contains(searchDto.Category));
            }

            if (searchDto.PublicationYear.HasValue)
            {
                query = query.Where(b => b.PublicationYear == searchDto.PublicationYear.Value);
            }

            if (searchDto.IsAvailable.HasValue && searchDto.IsAvailable.Value)
            {
                query = query.Where(b => b.AvailableCopies > 0);
            }

            var books = query.ToList();
            return _mapper.Map<List<BookDTO>>(books);
        }

        public List<BookRecommendationDTO> GetRecommendationsForMember(int memberId)
        {
            var memberLoans = _context.Loans
                .Include(l => l.Book)
                .ThenInclude(b => b.Category)
                .Where(l => l.MemberId == memberId)
                .ToList();

            if (!memberLoans.Any())
            {
                return GetPopularBooks().Select(b => new BookRecommendationDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.AuthorName,
                    Category = b.CategoryName,
                    Reason = "Popular book in library"
                }).Take(5).ToList();
            }

            var preferredCategories = memberLoans
                .GroupBy(l => l.Book.CategoryId)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            var recommendations = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Where(b => preferredCategories.Contains(b.CategoryId) && b.AvailableCopies > 0)
                .OrderByDescending(b => b.Loans.Count)
                .Take(5)
                .Select(b => new BookRecommendationDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author.Name,
                    Category = b.Category.Name,
                    Reason = $"Based on your interest in {b.Category.Name}"
                })
                .ToList();

            return recommendations;
        }

        private List<BookDTO> GetPopularBooks()
        {
            var books = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Include(b => b.Loans)
                .OrderByDescending(b => b.Loans.Count)
                .Take(10)
                .ToList();
            return _mapper.Map<List<BookDTO>>(books);
        }
    }
}
