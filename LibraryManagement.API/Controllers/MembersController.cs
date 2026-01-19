using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly MemberService _memberService;

        public MembersController(MemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var members = _memberService.GetAllMembers();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var member = _memberService.GetMemberById(id);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateMemberDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var member = _memberService.CreateMember(dto);
            return CreatedAtAction(nameof(GetById), new { id = member.MemberId }, member);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateMemberDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _memberService.UpdateMember(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _memberService.DeleteMember(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
