namespace TaskManagementApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public int? AssigneeId { get; set; }

        public TeamMember? Assignee { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}