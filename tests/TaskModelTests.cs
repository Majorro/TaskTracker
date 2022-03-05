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
    public class TaskModelTests
    {
        private TaskModel _task;

        [SetUp]
        public void Setup()
        {
            _task = new TaskModel { Id = Guid.NewGuid() };
        }

        [Test, Order(0)]
        public void TestAttachToProjectSuccessfully()
        {
            ProjectModel project = new() { Id = Guid.NewGuid() };

            _task.AttachToProject(project.Id);

            Assert.IsTrue(_task.IsInProject(project.Id));
        }

        [Test, Order(0)]
        public void TestAttachToProjectPassedNull()
        {
            ProjectModel project = null;

            Assert.Throws<NullReferenceException>(() => _task.AttachToProject(project?.Id));
        }

        /// <summary>
        /// Depends on success of <see cref="TestAttachToProjectSuccessfully"/>.
        /// </summary>
        [Test, Order(1)]
        public void TestDetachFromProjectSuccessfully()
        {
            ProjectModel project = new() { Id = Guid.NewGuid() };

            _task.AttachToProject(project.Id);

            Assert.IsTrue(_task.DetachFromProject());
            Assert.IsFalse(_task.IsInProject());
        }
    }
}
