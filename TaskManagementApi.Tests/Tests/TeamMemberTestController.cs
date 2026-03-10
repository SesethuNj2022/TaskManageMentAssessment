using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Controllers;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using Xunit;

namespace TaskManagementApi.Tests
{
    public class TeamMembersControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.TeamMembers.Add(new TeamMember
            {
                Id = 1,
                Name = "Alice",
                Email = "alice@test.com"
            });

            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetMembers_ReturnsMembers()
        {
            var context = GetDbContext();
            var controller = new TeamMembersController(context);

            var result = await controller.GetMembers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var members = Assert.IsAssignableFrom<IEnumerable<TeamMember>>(okResult.Value);

            Assert.Single(members);
        }
    }
}