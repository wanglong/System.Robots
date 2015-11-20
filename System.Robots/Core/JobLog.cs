using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using log4net;
using System.Threading;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository;
using System.Robots.Helper;

namespace System.Robots.Core
{
    /// <summary>
    /// Class JobLog.
    /// </summary>
    public class JobLog : IJobLog
    {
        /// <summary>
        /// The _log
        /// </summary>
        private ILog _log;

        /// <summary>
        /// The _logger
        /// </summary>
        private string _logger = "Logger";

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public string logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        /// <summary>
        /// The _mail logger
        /// </summary>
        private string _mailLogger = "MailLog";

        /// <summary>
        /// Gets or sets the mail logger.
        /// </summary>
        /// <value>The mail logger.</value>
        public string mailLogger
        {
            get { return _mailLogger; }
            set { _mailLogger = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobLog"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="mailLoggerName">Name of the mail logger.</param>
        public JobLog(string moduleName, string jobName, string mailLoggerName = "MailLog")
        {
            if (string.IsNullOrEmpty(jobName))
            {
                _log = LogManager.GetLogger(_logger);
                return;
            }

            var repositoryName = jobName;
            ILoggerRepository repository = null;
            try
            {
                repository = LogManager.GetRepository(repositoryName);
            }
            catch (Exception)
            {
            }

            //找到直接返回ilog
            if (repository != null)
            {
                _log = LogManager.GetLogger(repositoryName, "Defalut");
                return;
            }

            //未找到则创建，多线程下很有可能创建时，就存在了
            try
            {
                repository = LogManager.CreateRepository(repositoryName);
            }
            catch (Exception)
            {
                repository = LogManager.GetRepository(repositoryName);
            }

            var appender = new RollingFileAppender
            {
                Name = jobName + "logAppender",
                File = string.Format("../Log/{0}/{1}/log.txt", moduleName, jobName),
                LockingModel = new FileAppender.MinimalLock(),
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                DatePattern = "yyyyMMdd",
                //相同文件名最大数量
                MaxSizeRollBackups = 20,
                //文件大小
                MaximumFileSize = "5MB",
                //文件名不变
                StaticLogFileName = true,
                Encoding = Encoding.UTF8,
                Layout = new PatternLayout("%date [%-2thread] %-5level %logger - %message%newline")
            };
            appender.ActivateOptions();
            BasicConfigurator.Configure(repository, appender);
            _log = LogManager.GetLogger(repository.Name, "Defalut");
        }

        /// <summary>
        /// Informations the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        /// <exception cref="System.ArgumentNullException">Logger</exception>
        public void Info(string title, string message, bool mail = false)
        {

            ILog Logger = _log;
            if (Logger == null) throw new ArgumentNullException("Logger");
            ILog mailLoggerObj = LogManager.GetLogger(mailLogger);
            var ip = NetUtil.GetHostIp();
            var log = title;
            var mailLog = title + " Ip:" + ip;
            if (!string.IsNullOrEmpty(message))
            {
                log += "\r\n" + message;
                mailLog += "<br>" + message;
            }

            RecordLog(Logger.Info, log);
            if (mail)
            {
                RecordLog(mailLoggerObj.Info, mailLog);
            }

        }

        /// <summary>
        /// Informations the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        /// <exception cref="System.ArgumentNullException">Logger</exception>
        public void Info(string title, bool mail = false)
        {
            if (_log == null) throw new ArgumentNullException("Logger");
            ILog mailLoggerObj = LogManager.GetLogger(mailLogger);
            var ip = NetUtil.GetHostIp();
            var log = title;
            var mailLog = title + " Ip:" + ip;
            RecordLog(_log.Info, log);
            if (mail)
            {
                RecordLog(mailLoggerObj.Info, mailLog);
            }

        }

        /// <summary>
        /// Errors the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="e">The e.</param>
        /// <param name="message">The message.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        /// <exception cref="System.ArgumentNullException">Logger</exception>
        public void Error(string title, Exception e, string message, bool mail = false)
        {
            if (_log == null) throw new ArgumentNullException("Logger");
            ILog mailLoggerObj = LogManager.GetLogger(mailLogger);

            var ip = NetUtil.GetHostIp();
            var log = title;
            var maillog = title + " Ip:" + ip;
            if (!string.IsNullOrEmpty(message))
            {
                log += "\r\n" + message;
                maillog += "<br>" + message;
            }
            RecordLog(_log.Error, log, e);
            if (mail)
            {
                RecordLog(mailLoggerObj.Error, maillog, e);
            }

        }

        /// <summary>
        /// Errors the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="e">The e.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        /// <exception cref="System.ArgumentNullException">Logger</exception>
        public void Error(string title, Exception e, bool mail = false)
        {
            if (_log == null) throw new ArgumentNullException("Logger");
            ILog mailLoggerObj = LogManager.GetLogger(mailLogger);

            var ip = NetUtil.GetHostIp();
            var log = title;
            var maillog = title + " Ip:" + ip;
            RecordLog(_log.Error, log, e);
            if (mail)
            {
                RecordLog(mailLoggerObj.Error, maillog, e);
            }

        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="method">日志记录类型的方法</param>
        /// <param name="message">消息内容</param>
        private void RecordLog(Action<string> method, string message)
        {
            ThreadPool.QueueUserWorkItem(x => method(message));
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="method">日志记录类型的方法</param>
        /// <param name="message">消息内容</param>
        /// <param name="ex">异常</param>
        private void RecordLog(Action<string, Exception> method, string message, Exception ex)
        {
            ThreadPool.QueueUserWorkItem(x => method(message, ex));
        }
    }
}
