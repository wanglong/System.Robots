using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using System.IO;

namespace System.Robots.Helper
{
    /// <summary>
    /// Class JobHelper.
    /// </summary>
    public class JobHelper
    {
        private string assemblyName = AppConfigHelper.GetWinServiceName;
        private static IScheduler _scheduler = null;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            try
            {
                LogHelper<JobHelper>.Info("作业框架启动中");
                if (null == _scheduler)
                {
                    ISchedulerFactory sf = new StdSchedulerFactory();
                    _scheduler = sf.GetScheduler();
                }
                _scheduler.Start();

                string msg = string.Format(assemblyName + "已经启动{0}。", IsDebug ? "DEBUG" : "RELEASE");

                string mailSubject = "服务启动IP:" + NetUtil.GetHostIp() + " " + msg;



                LogHelper<JobHelper>.Info(mailSubject, true);

            }
            catch (Exception ex)
            {
                string title = assemblyName + "启动失败";
                LogHelper<JobHelper>.Error(title, ex, true);
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            try
            {
                if (null != _scheduler)
                {
                    _scheduler.Shutdown(true);
                }
                string title = assemblyName + "停止";
                LogHelper<JobHelper>.Info(title, true);
            }
            catch (Exception ex)
            {
                string title = assemblyName + "停止失败";
                LogHelper<JobHelper>.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 是否处在开发环境中
        /// </summary>
        /// <returns></returns>
        public static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }

        }
    }
}
