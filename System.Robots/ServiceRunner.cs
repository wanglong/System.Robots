using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Topshelf;

namespace System.Robots
{
    /// <summary>
    /// Class ServiceRunner. This class cannot be inherited.
    /// </summary>
    public sealed class ServiceRunner : ServiceControl, ServiceSuspend
    {
        /// <summary>
        /// The scheduler
        /// </summary>
        private readonly IScheduler scheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRunner"/> class.
        /// </summary>
        public ServiceRunner()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        /// <summary>
        /// Starts the specified host control.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Start(HostControl hostControl)
        {
            scheduler.Start();
            return true;
        }

        /// <summary>
        /// Stops the specified host control.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Stop(HostControl hostControl)
        {
            scheduler.Shutdown(false);
            return true;
        }

        /// <summary>
        /// Continues the specified host control.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            return true;
        }

        /// <summary>
        /// Pauses the specified host control.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            return true;
        }
    }
}
