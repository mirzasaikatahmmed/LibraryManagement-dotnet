using AutoMapper;
using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Entities;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.BLL.Services
{
    public class AuthorService
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public AuthorService(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorDTO> GetAllAuthors()
        {
            var authors = _context.Authors.ToList();
            return _mapper.Map<List<AuthorDTO>>(authors);
        }

        public AuthorDTO? GetAuthorById(int id)
        {
            var author = _context.Authors.Find(id);
            return author == null ? null : _mapper.Map<AuthorDTO>(author);
        }

        public AuthorDTO CreateAuthor(CreateAuthorDTO dto)
        {
            var author = _mapper.Map<Author>(dto);
            _context.Authors.Add(author);
            _context.SaveChanges();

            return _mapper.Map<AuthorDTO>(author);
        }

        public bool UpdateAuthor(int id, UpdateAuthorDTO dto)
        {
            var author = _context.Authors.Find(id);
            if (author == null) return false;

            _mapper.Map(dto, author);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAuthor(int id)
        {
            var author = _context.Authors.Find(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }
    }
}
