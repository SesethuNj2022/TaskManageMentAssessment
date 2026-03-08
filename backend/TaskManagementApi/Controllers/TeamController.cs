using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetMembers()
        {
            var members = _context.TeamMembers.ToList();

            return Ok(members);
        }
    }
}