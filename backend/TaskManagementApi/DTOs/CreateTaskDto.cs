using TaskManagementApi.Models;

namespace TaskManagementApi.DTOs
{
    public class CreateTaskDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public TaskPriority Priority { get; set; }

        public int? AssigneeId { get; set; }
    }
}