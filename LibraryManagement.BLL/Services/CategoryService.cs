using AutoMapper;
using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Entities;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.BLL.Services
{
    public class CategoryService
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryDTO> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public CategoryDTO? GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            return category == null ? null : _mapper.Map<CategoryDTO>(category);
        }

        public CategoryDTO CreateCategory(CreateCategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            _context.Categories.Add(category);
            _context.SaveChanges();

            return _mapper.Map<CategoryDTO>(category);
        }

        public bool UpdateCategory(int id, UpdateCategoryDTO dto)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return false;

            _mapper.Map(dto, category);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
