using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/team-members")]
    public class TeamMembersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamMembersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _context.TeamMembers.ToListAsync();

            if (members == null || !members.Any())
            {
                return NotFound(new
                {
                    message = "No team members found."
                });
            }

            return Ok(members);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);

            if (member == null)
            {
                return NotFound(new
                {
                    message = $"Team member with ID {id} was not found."
                });
            }

            return Ok(member);
        }
    }
}