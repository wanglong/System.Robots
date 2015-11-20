using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Robots.Core;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Logic
{
    /// <summary>
    /// Class Component.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// The task name
        /// </summary>
        protected string taskName = string.Empty;

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        /// <value>The name of the task.</value>
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public abstract void Add(Component component);

        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public abstract void Remove(Component component);

        /// <summary>
        /// Tasks the execute.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="mod">The mod.</param>
        /// <param name="rem">The rem.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool TaskExec(JobLog log, string mod, string rem);
    }
}
