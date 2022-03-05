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
        /// <summary>
        /// </summary>
        public DbSet<ProjectModel> Projects { get; private set; }
        /// <summary>
        /// </summary>
        public DbSet<TaskModel> Tasks { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options)
            : base(options)
        {}

        /// <summary>
        /// Checks whether a project with the given id exists in database or not.
        /// </summary>
        /// <param name="id">Unique identifier of a project.</param>
        /// <returns>True, if a project with the given id exists, otherwise false.</returns>
        public async Task<bool> IsProjectExists(Guid id) =>
            await Projects.AnyAsync(project => project.Id == id);

        /// <summary>
        /// Checks whether a task with the given id exists in database or not.
        /// </summary>
        /// <param name="id">Unique identifier of a task.</param>
        /// <returns>True, if a task with the given id exists, otherwise false.</returns>
        public async Task<bool> IsTaskExists(Guid id) =>
            await Tasks.AnyAsync(task => task.Id == id);
    }
}