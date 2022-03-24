using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTrackerTests
{
    /// <summary>
    /// Dummy tests lol
    /// </summary>
    [TestFixture]
    public class ProjectModelTests
    {
        private ProjectModel? _project;

        [SetUp]
        public void Setup()
        {
            _project = new ProjectModel { Id = Guid.NewGuid() };
        }

        [Test, Order(0)]
        public void TestAddTaskSuccessfully()
        {
            TaskModel task = new() { Id = Guid.NewGuid() };

            _project!.AddTask(task);

            Assert.IsTrue(task.IsInProject(_project.Id));
        }

        [Test, Order(0)]
        public void TestAddTaskPassedNull()
        {
            TaskModel? task = null;

            Assert.Throws<ArgumentNullException>(() => _project!.AddTask(task));
        }

        /// <summary>
        /// Depends on success of <see cref="TestAddTaskSuccessfully"/>.
        /// </summary>
        [Test, Order(1)]
        public void TestRemoveTaskSuccessfully()
        {
            TaskModel task = new() { Id = Guid.NewGuid() };

            _project!.AddTask(task);

            Assert.IsTrue(_project.RemoveTask(task));
            Assert.IsFalse(task.IsInProject());
        }

        [Test, Order(0)]
        public void TestRemoveTaskPassedNull()
        {
            TaskModel? task = null;

            Assert.Throws<ArgumentNullException>(() => _project!.RemoveTask(task));
        }
    }
}
