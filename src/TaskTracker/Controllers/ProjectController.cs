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
    /// Provides methods to work with <see cref="ProjectModel">projects</see>.
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        /// <inheritdoc cref="TaskTrackerContext"/>
        private readonly TaskTrackerContext _context;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="context"><inheritdoc cref="_context" path="/summary"/></param>
        public ProjectController(TaskTrackerContext context) =>
            _context = context;

        /// <summary>
        /// Gets project by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of a project.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> object wrapping <see cref="ProjectModel"/> object if all good,
        /// an <see cref="ActionResult{T}"/> object with error code and description otherwise.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectModel>> GetProject(Guid id)
        {
            ProjectModel project = await _context.Projects.FindAsync(id);

            if (project is null)
                return NotFound($"There is no project with {{id}} = {id}.");

            return Ok(project);
        }

        /// <summary>
        /// Gets all the existing projects.
        /// </summary>
        /// <returns>An <see cref="ActionResult{T}"/> object wrapping <see cref="IEnumerable{T}"/> of <see cref="ProjectModel"/> object.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProjectModel>> GetProjects() =>
            Ok(_context.Projects);

        /// <summary>
        /// Creates a new project based on passed object.
        /// </summary>
        /// <param name="project"><see cref="ProjectModel">ProjectModel</see> object whose fields will be used in creation.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> object wrapping the created <see cref="ProjectModel"/> object if all good,
        /// an <see cref="ActionResult{T}"/> object with error code and description otherwise. 
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpPost]
        [ProducesResponseType(typeof(ProjectModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProjectModel>> CreateProject([FromBody] ProjectModel project)
        {
            _context.Projects.Add(project);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (await _context.IsProjectExists(project.Id))
                    return BadRequest($"There is already exists project with {{id}} = {project.Id}");
                else
                    throw;
            }
            catch (Exception e)
            {
                throw;
            }

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        /// <summary>
        /// Updates an existing project using fields from the passed object. 
        /// </summary>
        /// <param name="id">Unique identifier of a project.</param>
        /// <param name="project"><see cref="ProjectModel">ProjectModel</see> object with updated fields.</param>
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
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectModel project)
        {
            if (id != project.Id)
                return BadRequest("{id} from the route is not equal to {id} from passed object.");

            if (!await _context.IsProjectExists(id))
                return NotFound($"There is no project with {{id}} = {id}.");

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a project by its unique identifier.
        /// </summary>
        /// <param name="id">Unqiue identifier of a project.</param>
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
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            ProjectModel project = await _context.Projects.FindAsync(id);

            if (project is null)
                return NotFound($"There is no project with {{id}} = {id}.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Gets all tasks of a project with the given id.
        /// </summary>
        /// <param name="id">Unique identifier of a project</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> object wrapping <see cref="IEnumerable{T}"/> of <see cref="TaskModel"/> or empty object if all good,
        /// an <see cref="ActionResult{T}"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("{id}/tasks")]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks(Guid id) // TODO: what faster?
        {
            ProjectModel project = await _context.Projects.Include(project => project.Tasks)
                                                          .Where(project => project.Id == id)
                                                          .FirstOrDefaultAsync();

            if(project is null)
                return NotFound($"There is no project with {{id}} = {id}.");

            return Ok(project.Tasks);

            //return Ok(await _context.Tasks.AsQueryable()
            //                              .Where(task => task.Project.Id == id)
            //                              .ToListAsync());

        }

        /// <summary>
        /// Adds task to a project.
        /// </summary>
        /// <param name="id">Unique identifier of a project.</param>
        /// <param name="taskId">Unique identifier of a task.</param>
        /// <returns>
        /// An empty <see cref="ActionResult"/> object if all good,
        /// an <see cref="ActionResult"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpPost("{id}/add-task/{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTask(Guid id, Guid taskId)
        {
            TaskModel task = await _context.Tasks.FindAsync(taskId);

            if(task is null)
                return NotFound($"There is no task with {{id}} = {taskId}.");

            if(!await _context.IsProjectExists(id))
                return NotFound($"There is no project with {{id}} = {id}.");

            task.AttachToProject(id!);
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Removes a task from a project.
        /// </summary>
        /// <param name="id">Unique identifier of a project.</param>
        /// <param name="taskId">Unique identifier of a task.</param>
        /// <returns>
        /// An empty <see cref="ActionResult"/> object if all good,
        /// an <see cref="ActionResult"/> object with error code and description otherwise.
        /// </returns>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        [HttpPost("{id}/remove-task/{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveTask(Guid id, Guid taskId)
        {
            TaskModel task = await _context.Tasks.FindAsync(taskId);

            if (task is null)
                return NotFound($"There is no task with {{id}} = {taskId}.");

            if (!await _context.IsProjectExists(id))
                return NotFound($"There is no project with {{id}} = {id}.");

            if (task.ProjectId != id)
                return BadRequest($"The task with {{id}} = {taskId} is not a part of the project with {{id}} = {id}.");

            if (task.DetachFromProject())
            {
                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
                return BadRequest($"The task with {{id}} = {taskId} is not a part of any project.");
        }
    }
}
