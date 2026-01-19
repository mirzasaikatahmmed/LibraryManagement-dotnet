using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Entities;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.BLL.Services
{
    public class MemberService
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public MemberService(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MemberDTO> GetAllMembers()
        {
            var members = _context.Members.ToList();
            return _mapper.Map<List<MemberDTO>>(members);
        }

        public MemberDTO? GetMemberById(int id)
        {
            var member = _context.Members.Find(id);
            return member == null ? null : _mapper.Map<MemberDTO>(member);
        }

        public MemberDTO CreateMember(CreateMemberDTO dto)
        {
            var member = _mapper.Map<Member>(dto);
            _context.Members.Add(member);
            _context.SaveChanges();

            return _mapper.Map<MemberDTO>(member);
        }

        public bool UpdateMember(int id, UpdateMemberDTO dto)
        {
            var member = _context.Members.Find(id);
            if (member == null) return false;

            _mapper.Map(dto, member);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteMember(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return false;

            _context.Members.Remove(member);
            _context.SaveChanges();
            return true;
        }
    }
}
