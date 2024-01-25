using BallastLaneApplication.API.Helper;
using BallastLaneApplication.Core.Dtos;
using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BallastLaneApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Tasks/GetAllTasks
        [HttpGet("GetAllTasks")]
        [Authorize("admin")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/Tasks/GetTasks
        [HttpGet("GetTasks")]
        [Authorize("user")]
        public async Task<IActionResult> GetTasksByUserName()
        {
            var tasks = await _taskService.GetAllByUserNameAsync(TokenUserHelper.GetUserFromHttpContext(HttpContext));
            return Ok(tasks);
        }

        // GET: api/Tasks/f0fbff1f-30ad-400a-afda-dc1bbdce74e9
        [HttpGet("{id}")]
        [Authorize("user")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost("CreateTask")]
        [Authorize("user")]
        public async Task<IActionResult> CreateTask([FromBody] TaskRegisterDtos task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTask = await _taskService.CreateTaskAsync(new TaskEntitie
            {
                Title = task.Title,
                Description = task.Description,
                UserName = TokenUserHelper.GetUserFromHttpContext(HttpContext),
                Status = task.Status,
                CreatedAt = DateTime.UtcNow
            });
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        [Authorize("user")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskEntitie task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.Id && task.UserName != TokenUserHelper.GetUserFromHttpContext(HttpContext))
            {
                return BadRequest();
            }

            await _taskService.UpdateTaskAsync(task);

            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        [Authorize("user")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await _taskService.DeleteTaskAsync(id);

            return NoContent();
        }
    }
}
