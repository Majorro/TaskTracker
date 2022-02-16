using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskTracker.Models
{
    public class TaskModel : BaseModel
    {
        public string Name { get; init; } = "";
        public string Description { get; init; } = "";
        public TaskStatus Status { get; init; }
        public int Priority { get; init; }

        [ForeignKey("Project")]
        public Guid? ProjectId { get; private set; } = null;
        [JsonIgnore]
        public ProjectModel Project { get; private set; } = null;

        /// <summary>
        /// Adds this task to a project.
        /// </summary>
        /// <param name="projectId">A project to which the task will be attached</param>
        /// <exception cref="NullReferenceException">Throws if passed <paramref name="projectId"/> is null.</exception>
        public void AttachToProject(Guid? projectId)
        {
            if (projectId is null)
                throw new NullReferenceException("The parameter {projectId} cannot be null.");
            else
                ProjectId = projectId;
        }

        /// <summary>
        /// Adds this task from a project.
        /// </summary>
        /// <returns>
        /// True if removed successfully,
        /// otherwise false meaning that task is not included in any project.
        /// </returns>
        public bool DetachFromProject()
        {
            if (IsInProject())
            {
                ProjectId = null;
                Project = null;

                return true;
            }
            else return false;
        }

        /// <summary>
        /// Checks whether task is included in any project or not.
        /// </summary>
        /// <returns>True, if task is included in some project, otherwise false.</returns>
        public bool IsInProject()
        {
            return ProjectId != null && ProjectId != Guid.Empty;
        }

        /// <summary>
        /// Checks whether task is included in project with the given id or not.
        /// </summary>
        /// <param name="projectId">Unique identifier of a project to check.</param>
        /// <returns>True, if task is included in the project, otherwise false.</returns>
        public bool IsInProject(Guid projectId)
        {
            return ProjectId == projectId;
        }
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}
