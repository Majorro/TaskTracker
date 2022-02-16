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
        public ProjectStatus Status { get; init; }
        public DateTime Start { get; init; }
        public DateTime Finish { get; init; }
        public int Priority { get; init; }

        [JsonIgnore]
        public IEnumerable<TaskModel> Tasks { get; init; } = null;

        /// <summary>
        /// Adds a task to this project.
        /// </summary>
        /// <param name="task">The task to be added.</param>
        /// <exception cref="NullReferenceException">Throws if passed <paramref name="task"/> is null.</exception>
        public void AddTask(TaskModel task)
        {
            if (task is null)
                throw new NullReferenceException("The parameter {task} cannot be null.");

            task.AttachToProject(Id);
        }

        /// <summary>
        /// Removes a task from this project.
        /// </summary>
        /// <param name="task">The task to be removed.</param>
        /// <returns>True, if removed successfully, otherwise false.</returns>
        /// <exception cref="NullReferenceException">Throws if passed <paramref name="task"/> is null.</exception>
        public bool RemoveTask(TaskModel task)
        {
            if (task is null)
                throw new NullReferenceException("The parameter {task} cannot be null.");

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
