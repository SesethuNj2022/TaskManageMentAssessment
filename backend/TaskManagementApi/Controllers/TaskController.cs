using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;
using TaskStatus = TaskManagementApi.Models.TaskStatus;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetTasks(
            string? search,
            TaskStatus? status,
            TaskPriority? priority,
            int? assigneeId)
        {
            var query = _context.Tasks
                .Include(t => t.Assignee)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Title != null && t.Title.Contains(search));

            if (status.HasValue)
                query = query.Where(t => t.Status == status);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority);

            if (assigneeId.HasValue)
                query = query.Where(t => t.AssigneeId == assigneeId);

            var tasks = await query.ToListAsync();

            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Assignee)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound(new
                {
                    message = $"Task with ID {id} was not found."
                });
            }

            return Ok(task);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Invalid request body."
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest(new
                {
                    message = "Title is required."
                });
            }

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                AssigneeId = dto.AssigneeId,
                Status = TaskStatus.Todo
            };

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id },
                task
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Invalid request body."
                });
            }

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new
                {
                    message = $"Task with ID {id} was not found."
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest(new
                {
                    message = "Title is required."
                });
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.Priority = dto.Priority;
            task.AssigneeId = dto.AssigneeId;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new
                {
                    message = $"Task with ID {id} was not found."
                });
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}