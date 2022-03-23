using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskTracker.Models
{
    public class ProjectModel : BaseModel
    {
        public string Name { get; init; } = "";
        public ProjectStatus Status { get; init; } = ProjectStatus.NotStarted;
        public DateTime? Start { get; init; }
        public DateTime? Finish { get; init; }
        public Priority Priority { get; init; } = Priority.Low;

        [JsonIgnore]
        public IEnumerable<TaskModel> Tasks { get; init; } = Enumerable.Empty<TaskModel>();

        /// <summary>
        /// Adds a task to this project.
        /// </summary>
        /// <param name="task">The task to be added.</param>
        /// <exception cref="ArgumentNullException">Throws if the passed <paramref name="task"/> is null.</exception>
        public void AddTask(TaskModel task)
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            task.AttachToProject(Id);
        }

        /// <summary>
        /// Removes a task from this project.
        /// </summary>
        /// <param name="task">The task to be removed.</param>
        /// <returns>True, if removed successfully, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Throws if the passed <paramref name="task"/> is null.</exception>
        public bool RemoveTask(TaskModel task)
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            return task.DetachFromProject();
        }
    }

    public enum ProjectStatus
    {
        NotStarted,
        Active,
        Completed
    }
}
