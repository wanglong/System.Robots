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
    /// Class Composite.
    /// </summary>
    public class Composite : Component
    {
        /// <summary>
        /// The components
        /// </summary>
        private IList<Component> components = new List<Component>();

        /// <summary>
        /// Adds the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public override void Add(Component component)
        {
            components.Add(component);
        }

        /// <summary>
        /// Removes the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public override void Remove(Component component)
        {
            components.Remove(component);
        }

        /// <summary>
        /// Synchronizes the goods information execute.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool TaskExec(JobLog log, string mod, string rem)
        {
            foreach (var item in components)
            {
                item.TaskExec(log, mod, rem);
            }

            return true;
        }
    }
}
