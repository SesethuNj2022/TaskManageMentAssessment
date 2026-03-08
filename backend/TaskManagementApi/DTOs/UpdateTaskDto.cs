using TaskManagementApi.Models;
using TaskStatus = TaskManagementApi.Models.TaskStatus;

namespace TaskManagementApi.DTOs
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; }

        public string?   Description { get; set; }

        public TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public int? AssigneeId { get; set; }
    }
}