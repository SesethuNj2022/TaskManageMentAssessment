using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;
using TaskStatus = TaskManagementApi.Models.TaskStatus;


namespace TaskManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<TeamMember>().HasData(
                new TeamMember
                {
                    Id = 1,
                    Name = "Alice",
                    Email = "alice@company.com"
                },
                new TeamMember
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@company.com"
                }
            );

            
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = 1,
                    Title = "Setup Project",
                    Description = "Initialize ASP.NET Web API",
                    Status = TaskStatus.Todo,
                    Priority = TaskPriority.High,
                    AssigneeId = 1
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Build Task Controller",
                    Description = "Create CRUD endpoints",
                    Status = TaskStatus.InProgress, 
                    Priority = TaskPriority.Medium,
                    AssigneeId = 2
                }
            );
        }
    }
}