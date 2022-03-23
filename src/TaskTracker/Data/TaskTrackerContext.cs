using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class TaskTrackerContext : DbContext
    {
        public DbSet<ProjectModel> Projects { get; init; } = null!;
        public DbSet<TaskModel> Tasks { get; init; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskTrackerContext"/> class
        /// using the specified options.
        /// </summary>
        /// <param name="options"><inheritdoc cref="DbContext(DbContextOptions)" path="/param[@name='options']"/></param>
        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options)
            : base(options)
        {}

        /// <summary>
        /// Checks whether a project with the given id exists in database or not.
        /// </summary>
        /// <param name="id">Unique identifier of a project.</param>
        /// <returns>A <see cref="Task{TResult}"/> that result containing
        /// true, if a project with the given id exists in the database, otherwise false.</returns>
        public async Task<bool> IsProjectExistsAsync(Guid id) =>
            await Projects.AnyAsync(project => project.Id == id);

        /// <summary>
        /// Checks whether a task with the given id exists in database or not.
        /// </summary>
        /// <param name="id">Unique identifier of a task.</param>
        /// <returns>A <see cref="Task{TResult}"/> that result containing
        /// true, if a task with the given id exists in the database, otherwise false.</returns>
        public async Task<bool> IsTaskExistsAsync(Guid id) =>
            await Tasks.AnyAsync(task => task.Id == id);
    }
}