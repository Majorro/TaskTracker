using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    /// <summary>
    /// Provides methods to work with <see cref="TaskModel">tasks</see>.
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        /// <inheritdoc cref="TaskTrackerContext"/>
        private readonly TaskTrackerContext _context;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="context"><inheritdoc cref="_context" path="/summary"/></param>
        public TaskController(TaskTrackerContext context) =>
            _context = context;

        /// <summary>
        /// Gets task by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of a task.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> object wrapping <see cref="TaskModel"/> object if all good,
        /// an <see cref="ActionResult{T}"/> object with error code and description otherwise.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskModel>> GetTask(Guid id)
        {
            TaskModel? task = await _context.Tasks.FindAsync(id);

            if (task is null)
                return NotFound($"There is no task with {{id}} = {id}.");

            return Ok(task);
        }

        /// <summary>
        /// Gets all existing tasks.
        /// </summary>
        /// <returns>An <see cref="ActionResult{T}"/> object wrapping <see cref="IEnumerable{T}"/> of <see cref="TaskModel"/> object.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TaskModel>> GetTasks() =>
            Ok(_context.Tasks);

        /// <summary>
        /// Creates a new task based on passed object.
        /// </summary>
        /// <param name="task"><see cref="TaskModel">TaskModel</see> object whose fields will be used in creation.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> object wrapping the created <see cref="TaskModel"/> object if all good,
        /// an <see cref="ActionResult{T}"/> object with error code and description otherwise. 
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpPost]
        [ProducesResponseType(typeof(TaskModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskModel>> CreateTask([FromBody] TaskModel task)
        {
            _context.Tasks.Add(task);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (await _context.IsTaskExistsAsync(task.Id))
                    return BadRequest($"There is already exists task with {{id}} = {task.Id}");
                else
                    throw;
            }

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        /// <summary>
        /// Updates an existing task using fields from the passed object. 
        /// </summary>
        /// <param name="id">Unique identifier of a task.</param>
        /// <param name="task"><see cref="TaskModel">TaskModel</see> object with updated fields.</param>
        /// <returns>
        /// An empty <see cref="ActionResult"/> object if all good,
        /// an <see cref="ActionResult"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskModel task)
        {
            if (id != task.Id)
                return BadRequest("{id} from the route is not equal to {id} from passed object.");

            if (!await _context.IsTaskExistsAsync(id))
                return NotFound($"There is no task with {{id}} = {id}.");

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes task by its unique identifier.
        /// </summary>
        /// <param name="id">Unqiue identifier of a task.</param>
        /// <returns>
        /// An empty <see cref="ActionResult"/> object if all good,
        /// an <see cref="ActionResult"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            TaskModel? task = await _context.Tasks.FindAsync(id);

            if (task is null)
                return NotFound($"There is no task with {{id}} = {id}.");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Gets a project of a task with the given id.
        /// </summary>
        /// <param name="id">Unique identifier of a task.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> object wrapping <see cref="ProjectModel">ProjectModel</see> or empty object if all good,
        /// an <see cref="ActionResult{T}"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("{id}/project")]
        [ProducesResponseType(typeof(ProjectModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProjectModel>> GetProject(Guid id)
        {
            TaskModel? task = await _context.Tasks.Include(task => task.Project)
                                                 .Where(task => task.Id == id)
                                                 .FirstOrDefaultAsync();

            if(task is null)
                return NotFound($"There is no task with {{id}} = {id}.");

            return Ok(task.Project);
        }

        /// <summary>
        /// Adds task to a project.
        /// </summary>
        /// <param name="id">Unique identifier of a task.</param>
        /// <param name="projectId">Unique identifier of a project.</param>
        /// <returns>
        /// An empty <see cref="ActionResult"/> object if all good,
        /// an <see cref="ActionResult"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpPost("{id}/attach-to-project/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AttachToProject(Guid id, Guid projectId)
        {
            TaskModel? task = await _context.Tasks.FindAsync(id);

            if(task is null)
                return NotFound($"There is no task with {{id}} = {id}.");

            if(!await _context.IsProjectExistsAsync(projectId))
                return NotFound($"There is no task with {{id}} = {projectId}.");

            task.AttachToProject(projectId!);
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Removes a task from a project.
        /// </summary>
        /// <param name="id">Unique identifier of a task.</param>
        /// <returns>
        /// An empty <see cref="ActionResult"/> object if all good,
        /// an <see cref="ActionResult"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpPost("{id}/detach-from-project")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DetachFromProject(Guid id)
        {
            TaskModel? task = await _context.Tasks.FindAsync(id);

            if (task is null)
                return NotFound($"There is no task with {{id}} = {id}.");

            if (task.DetachFromProject())
            {
                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
                return BadRequest($"The task with {{id}} = {id} is not included in any project.");
        }
    }
}
