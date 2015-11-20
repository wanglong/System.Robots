using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Robots.Helper;
using System.Text;
using System.Threading.Tasks;

namespace System.Robots.Core
{
    /// <summary>
    /// Class JobBase.
    /// </summary>
    [DisallowConcurrentExecutionAttribute]
    public abstract class JobBase : IJob
    {
        public string JobName = "JobBase";
        public string ModuleName = "ModuleName";
        public JobLog Log;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobBase"/> class.
        /// </summary>
        public JobBase()
        {
        }

        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                String jobName = ((JobDetailImpl)context.JobDetail).Name;
                JobName = jobName;
                Log = new JobLog(ModuleName, jobName);
                Log.Info(string.Format("{0}:{1}-{2}", jobName, DateTime.Now, "执行开始"));
                var jobMap = context.JobDetail.JobDataMap;
                var keys = jobMap.GetKeys();
                IDictionary<string, string> keyDic = new Dictionary<string, string>();
                foreach (var key in keys)
                {
                    keyDic.Add(key, jobMap.GetString(key));
                }

                DateTimeOffset dts = context.FireTimeUtc ?? new DateTimeOffset();
                DateTime localTime = dts.LocalDateTime;
                DateTimeOffset nextDts = context.NextFireTimeUtc ?? new DateTimeOffset();
                DateTime nextTime = nextDts.LocalDateTime;
                var second = (nextTime - localTime).TotalSeconds;
                StringBuilder sbLog = new StringBuilder();
                DateTime time = DateTime.Now;
                if (String.IsNullOrEmpty(JobName))
                {
                    Helper.LogHelper<Helper.JobHelper>.Info(this.GetType().Name + ":JobName为空");
                    return;
                }

                var Switch = AppConfigHelper.GetWinServiceAutoSwitch;
                if (!String.IsNullOrEmpty(Switch) && Switch.ToUpper() == "TRUE")
                {
                    var jobId = JobName;
                    var checkBack = CheckServerService.CheckServer(jobId, second);
                    if (!checkBack)
                    {
                        Log.Info(string.Format("{0}:{1}", JobName, "无权执行"));
                        return;
                    }
                }

                var result = Job(keyDic);
                Log.Info(string.Format("{0}:{1}-{2}", jobName, DateTime.Now, "执行结束"));
            }
            catch (Exception ex)
            {
                Log.Error("作业异常", ex, true);
            }
        }

        /// <summary>
        /// 作业子类实现作业接口
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public abstract bool Job(IDictionary<string, string> keys);
    }
}
