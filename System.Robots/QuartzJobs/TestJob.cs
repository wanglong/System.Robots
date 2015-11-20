using System;
using System.Collections.Generic;
using System.Linq;
using System.Robots.Helper;
using System.Robots.Logic;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;
using System.Robots.Core;

namespace System.Robots.QuartzJobs
{
    /// <summary>
    /// Class TestJob. This class cannot be inherited.
    /// </summary>
    [DisallowConcurrentExecution]
    public class TestJob : JobBase
    {
        /// <summary>
        /// The _logger
        /// </summary>
        private readonly ILog _logger = LogManager.GetLogger(typeof(TestJob));

        /// <summary>
        /// Initializes a new instance of the <see cref="TestJob"/> class.
        /// </summary>
        public TestJob()
        {
            ModuleName = "TestJob";
        }

        /// <summary>
        /// 作业子类实现作业接口
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool Job(IDictionary<string, string> keys)
        {
            try
            {
                Log.Info(
                    string.Format("{0}-{1}:{2}", JobName + keys["rem"],
                    "开始",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                var runDoWorkLogic = new TestLogic();
                bool bak = runDoWorkLogic.Execution(Log, keys["mod"], keys["rem"]);
                Log.Info(
                    string.Format("{0}-{1}:{2}", JobName + keys["rem"],
                    "结束",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                return bak;

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AuditTemplateWorkFlowJob Error:" + ex.Message + ",StackTrace:" + ex.StackTrace);
            }

            return false;
        }
    }
}
