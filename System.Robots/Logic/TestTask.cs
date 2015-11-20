using System;
using System.Collections.Generic;
using System.Linq;
using System.Robots.Core;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Logic
{
    /// <summary>
    /// Class TestTask.
    /// </summary>
    public class TestTask : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestTask"/> class.
        /// </summary>
        public TestTask()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestTask"/> class.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        public TestTask(string taskName)
        {
            this.TaskName = taskName;
        }

        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Add(Component component)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tasks the execute.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="mod">The mod.</param>
        /// <param name="rem">The rem.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool TaskExec(JobLog log, string mod, string rem)
        {
            //TODO:Task Exec.
            return true;
        }
    }
}
