using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Robots.Core;

namespace System.Robots.Logic
{
    /// <summary>
    /// Class TestLogic.
    /// </summary>
    public class TestLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestLogic"/> class.
        /// </summary>
        public TestLogic()
        {
        }

        /// <summary>
        /// Executions the specified log.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="mod">The mod.</param>
        /// <param name="rem">The rem.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Execution(JobLog log, string mod, string rem)
        {
            try
            {
                var composite = new Composite();
                composite.TaskName = "TestTask";
                var taskTest = new TestTask("TestTask");
                composite.Add(taskTest);
                composite.TaskExec(log, mod, rem);
            }
            catch (Exception ex)
            {
                log.Error("失败：" + ex.Message + " " + ex.StackTrace, ex, true);
            }

            return true;
        }
    }
}
